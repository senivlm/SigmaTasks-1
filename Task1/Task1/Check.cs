using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Check
    {
        private Check() { }
        public static void PrintProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Cost: ${product.Cost}");
            Console.WriteLine($"Weight: {product.Weight} kg");
        }
        public static void PrintBuy(Buy buy)
        {
            Console.WriteLine("Information about product:");
            PrintProduct(buy.Product);
            Console.WriteLine("Information about buy:");
            Console.WriteLine($"Count: {buy.Count}");
            Console.WriteLine($"Overall cost: ${buy.OverallCost}");
            Console.WriteLine($"Overall weight: {buy.OverallWeight} kg");
        }
    }
}
