using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task8
{
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateSorting();
            DemonstrateStorageSearch();
            DemonstrateSentenceHandler();
        }

        private static void DemonstrateSentenceHandler()
        {
            SentenceHandler handler = new(@"D:\Workspace\SigmaTasks\Task8\Task8\text.txt");
            handler.SortSentencesByLength();
            foreach (string sentence in handler.Sentences)
            {
                Console.WriteLine(sentence);
            }

            Console.WriteLine();
            Console.WriteLine(handler.GetSentenceWithMaxNestedDepth());
        }

        private static void DemonstrateStorageSearch()
        {
            try
            {
                StorageSearch stSearch = new(
                    new Storage(@"D:\Workspace\SigmaTasks\Task8\Task8\products1.txt"),
                    new Storage(@"D:\Workspace\SigmaTasks\Task8\Task8\products2.txt")
                );
                List<Product> commonProducts = stSearch.GetCommonProducts();
                Console.WriteLine("Common products: \n");
                foreach (Product product in commonProducts)
                {
                    Console.WriteLine(product);
                }

                List<Product> productsSt1 = stSearch.GetProductsOnlyInFirstStorage();
                Console.WriteLine("Products which only 1st storage contains: \n");
                foreach (Product product in productsSt1)
                {
                    Console.WriteLine(product);
                }

                List<Product> productsSt2 = stSearch.GetProductsOnlyInSecondStorage();
                Console.WriteLine("Products which only 2nd storage contains: \n");
                foreach (Product product in productsSt2)
                {
                    Console.WriteLine(product);
                }

                List<Product> nonCommonProducts = stSearch.GetNonCommonProducts();
                Console.WriteLine("Non-Common products: \n");
                foreach (Product product in nonCommonProducts)
                {
                    Console.WriteLine(product);
                }
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
            catch (FormatException ex)
            {
                Console.WriteLine("Exception was caught.");
                Console.WriteLine(ex);
            }
        }

        private static void DemonstrateSorting()
        {
            List<Product> productList = new()
            {
                new("NVidia GeForce RTX 3090", 3099, 2.5, 999, DateTime.Now),
                new("Intel Core i9-11900K", 599, 0.2, 999, DateTime.Now.AddDays(-4)),
                new("Bulka", 4, 0.5, 2, DateTime.Now.AddHours(-4))
            };
            Product[] productArray = productList.ToArray();
            Comparer comparerByCost = (obj1, obj2) =>
            {
                Product prod1 = obj1 as Product;
                Product prod2 = obj2 as Product;
                return (int)(prod1.Cost - prod2.Cost);
            };
            Sorting.Sort(productArray, comparerByCost);
            Console.WriteLine("Products sorted by cost: ");
            Console.WriteLine();
            foreach (Product product in productArray)
            {
                Console.WriteLine(product);
            }
            Sorting.Sort(productArray, (obj1, obj2) =>
            {
                Product prod1 = obj1 as Product;
                Product prod2 = obj2 as Product;
                return prod1.DaysToExpiration - prod2.DaysToExpiration;
            });
            Console.WriteLine("Products sorted by days to expiration: ");
            Console.WriteLine();
            foreach (Product product in productArray)
            {
                Console.WriteLine(product);
            }
        }
    }
}
