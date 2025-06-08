using System.Reflection;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Exchanger
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            bool exitProgram_toggle = false;
            bool mainMenu_toggle = true;
            bool availableCurrencyMenu_toggle = false;
            bool currenciesListMenu_toggle = false;
            bool currenciesListExchangerMenu_toggle = false;
            string chosenCurrency;
            var dictionary = new CurrencyDictionary();
            CurrencyDictionaryItem chosenCurrencyList;
            CurrencyData data;

            while (!exitProgram_toggle)
            {



                while (mainMenu_toggle)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Welcome to our small console exchanger");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1.Available currencies\n2.Choose a currency\n3.Exit");
                    Console.WriteLine("----------------------------------------------------------");
                    string chosenOption = Console.ReadLine();
                    switch (chosenOption)

                    {
                        case "1":
                            mainMenu_toggle = false;
                            availableCurrencyMenu_toggle = true;
                            break;
                        case "2":
                            mainMenu_toggle = false;
                            currenciesListMenu_toggle = true;
                            break;
                        case "3":
                            mainMenu_toggle = false;
                            exitProgram_toggle = true;
                            break;
                        default:
                            break;
                    }

                    if (!availableCurrencyMenu_toggle && !mainMenu_toggle && !currenciesListMenu_toggle)
                    {
                        Console.WriteLine("See ya!");
                    }
                }



                while (availableCurrencyMenu_toggle)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine($"Available currencies: \n{dictionary.getMyAvailableListToString()}");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1.Choose a currency\n2.Back to main menu\n3.Exit");
                    Console.WriteLine("----------------------------------------------------------");
                    string chosenOption = Console.ReadLine();
                    switch (chosenOption)

                    {
                        case "1":
                            availableCurrencyMenu_toggle = false;
                            currenciesListMenu_toggle = true;
                            break;
                        case "2":
                            availableCurrencyMenu_toggle = false;
                            mainMenu_toggle = true;
                            break;
                        case "3":
                            availableCurrencyMenu_toggle = false;
                            exitProgram_toggle = true;
                            break;
                        default:
                            break;
                    }

                    if (!availableCurrencyMenu_toggle && !mainMenu_toggle && !currenciesListMenu_toggle)
                    {
                        Console.WriteLine("See ya!");
                    }

                }


                while (currenciesListMenu_toggle)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine($"Available currencies: \n{dictionary.getMyAvailableListToString()}");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.Write("Please choose a currency: ");
                    chosenCurrency = Console.ReadLine();
                    chosenCurrencyList = await dictionary.findItem(chosenCurrency);
                    if (chosenCurrencyList == null)
                    {
                        Console.WriteLine("Something has gone wrong. Probably you wrote an incorrect currency!");
                        Console.ReadKey();
                        continue;
                    }
                    dictionary.UnpackMyData(chosenCurrencyList);
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine($"1.Exchange {chosenCurrency}\n2.Choose a currency\n3.Back to main menu\n4.Exit");
                    Console.WriteLine("----------------------------------------------------------");
                    string chosenOption = Console.ReadLine();
                    switch (chosenOption)

                    {
                        case "1":
                            currenciesListExchangerMenu_toggle = true;
                            break;
                        case "2":
                            availableCurrencyMenu_toggle = false;
                            currenciesListMenu_toggle = true;
                            break;
                        case "3":
                            currenciesListMenu_toggle = false;
                            mainMenu_toggle = true;
                            break;
                        case "4":
                            currenciesListMenu_toggle = false;
                            exitProgram_toggle = true;
                            break;
                        default:
                            break;
                    }


                    while (currenciesListExchangerMenu_toggle)
                    {
                        Console.WriteLine("----------------------------------------------------------");
                        Console.Write("Please choose a currency to exchange: ");
                        string chosenCurrencyToExchange = Console.ReadLine();
                        if (dictionary.availableCurrencies.Contains(chosenCurrencyToExchange))
                        {
                            float? valueOfChosenCurrencyToExchange = dictionary.getCurrencyValue(chosenCurrency, chosenCurrencyToExchange);
                            if (valueOfChosenCurrencyToExchange == null) {
                                Console.WriteLine("Couldn't find a needed currency to exchange ");
                                Console.WriteLine("Please press any key to exit from Exchange menu");
                                Console.ReadKey();

                                continue;
                            };
                            Console.WriteLine("----------------------------------------------------------");
                            Console.Write($"How many {chosenCurrency} do you want to exchange to {chosenCurrencyToExchange}: ");
                            float valueToExchange = float.Parse(Console.ReadLine());
                            Console.WriteLine("----------------------------------------------------------");
                            Console.WriteLine($"You've successfully exchanged your {chosenCurrency} to {chosenCurrencyToExchange}");
                            Console.WriteLine($"You've got {valueToExchange * valueOfChosenCurrencyToExchange} {chosenCurrencyToExchange}");
                            Console.WriteLine("----------------------------------------------------------");
                            Console.WriteLine("Please press any key to exit from Exchange menu");

                            Console.ReadKey();

                            currenciesListExchangerMenu_toggle = false;
                            currenciesListMenu_toggle = false;
                            mainMenu_toggle = true;
                        }
                        else
                        {
                            Console.WriteLine("Something has gone wrong. Probably you wrote an incorrect currency!");
                            Console.ReadKey();
                            continue;
                        }


                    }



                    if (!availableCurrencyMenu_toggle && !mainMenu_toggle && !currenciesListMenu_toggle)
                    {
                        Console.WriteLine("See ya!");
                    }

                }


            }
        }
    }
}
