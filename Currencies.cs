using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.Design;
using Exchanger;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace Exchanger
{
    public class CurrencyInfo
    {
        public string Code { get; set; }
        public float? Value { get; set; }

    }

    public class Meta
    {
        public DateTime last_updated_at { get; set; }
    }

    public class CurrencyData
    {
        public Meta Meta { get; set; }
        public Dictionary<string, CurrencyInfo> Data { get; set; }
    }
 
    public class CurrencyDictionary
    {
        public Dictionary<string, CurrencyDictionaryItem> Data { get;  set; }
        public List<string> availableCurrencies { get; private set; } 
        public CurrencyDictionary()
        {
            Data = new Dictionary<string, CurrencyDictionaryItem>();
            availableCurrencies = new List<string> { "AED", "AUD", "CAD", "CHF", "CNY", "CZK", "DKK", "EUR", "GBP", "ILS", "INR", "JPY", "KRW", "KZT", "MXN", "PLN", "SGD", "TRY", "USD", "UAH" };
        }


        public async Task<CurrencyDictionaryItem> findItem(string currency)
        {
            if (availableCurrencies.Contains(currency)) { 

            if (Data.ContainsKey(currency)) {
                checkActualDataAndReplace(currency);
                return Data[currency];
            }

            var data = await MyClient.getMyClientData(currency);

            Data.Add(currency, new CurrencyDictionaryItem { Meta = data.Meta }); 
            foreach (var item in data.Data)
            {
                Data[currency].Data.Add(item.Key, new CurrencyDictionaryItemCONTAIN { Data = new CurrencyInfo { Code = item.Key, Value = item.Value.Value } });
            }
            return Data[currency];
            }

            Console.WriteLine("I can't find anything, probably you wrote unavailable currency");
            return null;

        }

        public async Task checkActualDataAndReplace(string currency)
        {
            DateTime dateTimeRequest = Data[currency].Meta.last_updated_at;
            DateTime actualData = DateTime.Now;
            TimeSpan difference = dateTimeRequest - actualData;
            double minutesPassed = difference.TotalMinutes;
            if (minutesPassed < -1)
            {
                var data = await MyClient.getMyClientData(currency);

                Data[currency] = new CurrencyDictionaryItem { Meta = data.Meta };
                foreach (var item in data.Data)
                {
                    Data[currency].Data.Add(item.Key, new CurrencyDictionaryItemCONTAIN
                    {
                        Data = new CurrencyInfo
                        {
                            Code = item.Key,
                            Value = item.Value.Value
                        }
                    });
                }
            }
        }


        public string getMyAvailableListToString() {
            return string.Join(", ", availableCurrencies);
        }



        public void UnpackMyData(CurrencyDictionaryItem items)
        {
            foreach (var item in items.Data) {
                Console.WriteLine($"Currency: {item.Value.Data.Code}, value: {item.Value.Data.Value}");
            }
        }

        public float? getCurrencyValue(string CurrencyFromExchange, string CurrencyToExchange) {
            foreach (var item in Data[CurrencyFromExchange].Data)
            {
                if (item.Value.Data.Code == CurrencyToExchange)
                {
                    return item.Value.Data.Value;
                }
            }
            return null;
        }

    }
    public class CurrencyDictionaryItem
    {

        public Dictionary<string, CurrencyDictionaryItemCONTAIN> Data { get;  set; }
        public Meta Meta { get; set; }

        public CurrencyDictionaryItem()
        {
            Data = new Dictionary<string, CurrencyDictionaryItemCONTAIN>();
        }

    }

    public class CurrencyDictionaryItemCONTAIN
    {

        public CurrencyInfo Data { get; set; }

    }

}