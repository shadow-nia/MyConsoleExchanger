using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Exchanger;

namespace Exchanger
{

    static public class MyClient
    {
        static private string LINK = "https://api.currencyapi.com/v3/latest";
        static private string API_KEY = "?apikey=cur_live_5AOkVYBxjllggyUEG0eNkWmNqM9iOhyLvjlR8udm";
        static private string AVAILABLE_CURRENCIES = "&currencies=AED%2CAUD%2CCAD%2CCHF%2C" +
            "CNY%2CCZK%2CDKK%2CEUR%2CGBP%2CILS%2CINR%2C" +
            "JPY%2CKRW%2CKZT%2CMXN%2CPLN%2CSGD%2CTRY%2CUSD%2CUAH";

        
       static public HttpClient Client {get; private set;}

       static MyClient()
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri(LINK)
            };          
        }

        static public async Task<CurrencyData> getMyClientData(string base_currency)
        {
            try
            {
                CurrencyData? result = await Client.GetFromJsonAsync<CurrencyData>($"{API_KEY}{AVAILABLE_CURRENCIES}&base_currency={base_currency}");
                if (result != null)
                {
                    //Console.WriteLine($"Last updated at: {result.meta.last_updated_at}");

                    return result;
                }
                else
                {
                    throw new [global::System.Serializable]
public class MyException : Exception
        {
            public MyException() { }
            public MyException(string message) : base(message) { }
            public MyException(string message, Exception inner) : base(message, inner) { }
            protected MyException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
        ($"Failed to get {base_currency}"); };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }



        }

        static public void UnpackMyClientData(Dictionary<string, CurrencyInfo> data) {
            foreach (var currency in data)
            {
                Console.WriteLine($"Currency: {currency.Value.Code}, value: {currency.Value.Value}");
            }

        }

    }
}



