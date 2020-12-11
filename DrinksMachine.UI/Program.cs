using DrinksMachine.Logic;
using DrinksMachine.Logic.Executors;
using DrinksMachine.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace DrinksMachine.UI
{
    class Program
    {
        static DrinkExecutor DrinkExecutor = new DrinkExecutor();
        static Dictionary<string, int> Ingredients = new Dictionary<string, int>()
        {
            { "Lemon Tea", 2 },
            { "Lemon Slice", 2 },
            { "Coffee", 2 },
            { "Hot Chocolate", 2 },
            { "Milk", 4 },
            { "Sugar", 4 }
        };
        static DrinkMachineSensorMonitor DrinkMachineSensorMonitor = new DrinkMachineSensorMonitor(90, 100, 100, Ingredients);

        static void Main(string[] args)
        {
            Console.WriteLine("Drinks Machine!");

            // Get the Configuration.
            var config = ConfigurationManager.AppSettings;
            var templateFileName = config["DrinksTemplateFile"];

            if (!File.Exists(templateFileName))
            {
                Console.WriteLine($"No configuration file found at '{templateFileName}'.");
                Console.ReadLine();
                Environment.Exit(-1);
            }

            String JSONtxt = File.ReadAllText(templateFileName);
            var templates = JsonConvert.DeserializeObject<IList<Drink>>(JSONtxt);
            Console.WriteLine("Drinks templates loaded.");

            SetCommands(templates);

            var drinksAvailable = new Drink[]
            {
                templates[0],
                templates[1],
                templates[2]
            };

            string option = "0";

            do
            {
                PrintMenu();
                option = Console.ReadLine().ToUpper();

                if (option != "E")
                {
                    var index = int.Parse(option);

                    if (drinksAvailable[index - 1].Name == "Coffee")
                    {
                        var sugar = AskForSugar();
                        var milk = AskForMilk();

                        drinksAvailable[index - 1].Ingredients["Sugar"] = sugar;
                        drinksAvailable[index - 1].Ingredients["Milk"] = milk;
                    }


                    GetDrink(drinksAvailable[index-1]);

                    if (drinksAvailable[index - 1].Name == "Coffee")
                    {
                        drinksAvailable[index - 1].Ingredients["Sugar"] = 0;
                        drinksAvailable[index - 1].Ingredients["Milk"] = 0;
                    }
                }

            } while (option != "E");


            //END
            Console.ReadLine();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("Please select a beverage:");
            Console.WriteLine("1) Lemon Tea");
            Console.WriteLine("2) Coffee");
            Console.WriteLine("3) Hot Chocolate");
            Console.WriteLine("e) Exit");
        }

        private static int AskForSugar()
        {
            Console.WriteLine("Please select sugar:");
            Console.WriteLine("0) 0");
            Console.WriteLine("1) 1");
            Console.WriteLine("2) 2");

            return int.Parse(Console.ReadLine());
        }

        private static int AskForMilk()
        {
            Console.WriteLine("Please select milk:");
            Console.WriteLine("0) 0");
            Console.WriteLine("1) 1");
            Console.WriteLine("2) 2");

            return int.Parse(Console.ReadLine());
        }

        private static void GetDrink(Drink template)
        {
            var result = DrinkExecutor.ExecuteCommands(template);

            result.Messages.ForEach(x => Console.WriteLine(x));
        }

        private static void SetCommands(IList<Drink> templates)
        {
            templates[0].ServingActions.Add(new WaterCommand(DrinkMachineSensorMonitor));
            templates[0].ServingActions.Add(new AddIngredientsCommand(DrinkMachineSensorMonitor));
            templates[0].ServingActions.Add(new ServeDrinkCommand((cr, s) =>
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, "Pour tea into cup"));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, "Pour tea into cup"));
                    return false;
                }
            }));
            templates[0].ServingActions.Add(new ServeDrinkCommand((cr, s) =>
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, "Serve"));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, "Serve"));
                    return false;
                }
            }));

            templates[1].ServingActions.Add(new WaterCommand(DrinkMachineSensorMonitor));
            templates[1].ServingActions.Add(new AddIngredientsCommand(DrinkMachineSensorMonitor));
            templates[1].ServingActions.Add(new ServeDrinkCommand((cr, s) =>
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, "Pour coffee into cup"));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, "Pour coffee into cup"));
                    return false;
                }
            }));
            templates[1].ServingActions.Add(new ServeDrinkCommand((cr, s) =>
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, "Serve"));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, "Serve"));
                    return false;
                }
            }));

            templates[2].ServingActions.Add(new WaterCommand(DrinkMachineSensorMonitor));
            templates[2].ServingActions.Add(new AddIngredientsCommand(DrinkMachineSensorMonitor));
            templates[2].ServingActions.Add(new ServeDrinkCommand((cr, s) =>
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, "Pour hot choclate into cup"));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, "Pour hot chocolate into cup"));
                    return false;
                }
            }));
            templates[2].ServingActions.Add(new ServeDrinkCommand((cr, s) =>
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, "Serve"));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, "Serve"));
                    return false;
                }
            }));
        }
    }
}
