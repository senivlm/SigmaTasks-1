using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Task9.Enums;
using Task9.Models;

namespace Task9
{
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateStorage();
        }

        private static void DemonstrateStorage()
        {
            Storage storage = new();
            storage.OnExpiredProductsShow += ShowExpiredProducts;
            storage.OnExpiredProductsShow += DealExpiredProducts;
            storage.OnInvalidProductDetected += SaveMessageToFile;
            storage.OnInvalidProductDetected += FixInvalidProduct;
            try
            {
                storage.ReadProducts(@"D:\Workspace\SigmaTasks\Task9\Task9\products.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Exception was caught.");
                Console.WriteLine(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Exception was caught.");
                Console.WriteLine(ex);
            }
            
            Console.WriteLine(storage.PrintProducts());
            storage.ShowExpiredProducts();
            Console.WriteLine(storage.PrintProducts());
        }

        private static void FixInvalidProduct(Storage sender, string message, string line)
        {
            Console.WriteLine($"Invalid product in line: {line}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine("What do you want to do?\n1. Manually enter data.\n2. Skip.");
            string answer;
            while (!Regex.IsMatch(answer = Console.ReadLine(), @"^[1-2]$"))
            {
                Console.WriteLine("Wrong input.");
            }

            switch (answer)
            {
                case "1":
                    Console.WriteLine("Select type of product:\n1. Product.\n2. Meat.\n3. Dairy.");
                    string type;
                    while (!Regex.IsMatch(type = Console.ReadLine(), @"^[1-3]$"))
                    {
                        Console.WriteLine("Wrong input.");
                    }
                    
                    string name;
                    Console.Write("Enter name: ");
                    while (!Regex.IsMatch(name = Console.ReadLine(), @"^(\w+\s*)+"))
                    {
                        Console.WriteLine("Wrong input.");
                    }

                    string input;
                    double cost, weight;
                    int days;
                    DateTime madeDate;
                    Console.Write("Enter cost: ");
                    while (!Double.TryParse(Console.ReadLine(), out cost))
                    {
                        Console.WriteLine("Wrong input.");
                    }
                    Console.Write("Enter weight: ");
                    while (!Double.TryParse(Console.ReadLine(), out weight))
                    {
                        Console.WriteLine("Wrong input.");
                    }
                    Console.Write("Days to expiration: ");
                    while (!Int32.TryParse(Console.ReadLine(), out days) || days < 0)
                    {
                        Console.WriteLine("Wrong input.");
                    }
                    Console.Write("Enter made date (Format: dd.MM.yyyy): ");
                    while (!DateTime.TryParseExact(
                        Console.ReadLine(), 
                        "dd.MM.yyyy", 
                        null, 
                        DateTimeStyles.None,
                        out madeDate))
                    {
                        Console.WriteLine("Wrong input.");
                    }

                    if (type == "1")
                    {
                        sender.AddProduct(new Product(name, cost, weight, days, madeDate));
                    }
                    else if (type == "2")
                    {
                        sender.AddProduct(new DairyProducts(name, cost, weight, days, madeDate));
                    }
                    else
                    {
                        Console.Write("Category: ");
                        object category, kind;
                        while (!Enum.TryParse(typeof(Category), Console.ReadLine(), true, out category))
                        {
                            Console.WriteLine("Wrong input.");
                        }

                        Console.Write("Kind: ");
                        while (!Enum.TryParse(typeof(Kind), Console.ReadLine(), true, out kind))
                        {
                            Console.WriteLine("Wrong input.");
                        }

                        sender.AddProduct(
                            new Meat(name, cost, weight, days, madeDate, (Category) category, (Kind) kind)
                            );
                    }
                    break;
                case "2":
                    return;
            }
        }

        private static void SaveMessageToFile(Storage sender, string message, string line)
        {
            if (!File.Exists(@"D:\Workspace\SigmaTasks\Task9\Task9\log.txt"))
            {
                _ = File.Create(@"D:\Workspace\SigmaTasks\Task9\Task9\log.txt");
            }

            using StreamWriter writer = new(@"D:\Workspace\SigmaTasks\Task9\Task9\log.txt", true);
            writer.WriteLine($"Invalid line: {line}");
            writer.WriteLine($"Message: {message}");
            writer.WriteLine($"Date: {DateTime.Now}\n");
        }

        private static void ShowExpiredProducts(Storage sender, List<Product> expired)
        {
            Console.WriteLine("Expired products: ");
            if (!expired.Any())
            {
                Console.WriteLine("None.");
            }
            foreach (Product expiredProduct in expired)
            {
                Console.WriteLine(expiredProduct);
            }
        }

        private static void DealExpiredProducts(Storage sender, List<Product> expired)
        {
            Console.WriteLine("What do you want to do with expired products?\n1. Remove.\n2. Leave them in storage.");
            string answer;
            while (!Regex.IsMatch(answer = Console.ReadLine(), @"^[1-2]$"))
            {
                Console.WriteLine("Wrong input.");
            }

            if (answer == "1")
            {
                expired.ForEach(sender.RemoveProduct);
            }
        }
    }
}
