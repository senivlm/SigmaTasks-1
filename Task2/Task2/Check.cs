using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Check //: Storage // Cannot derive from sealed type 'Storage'
    {
        private Check() { }
        public static void PrintProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            Console.WriteLine(product.ToString());
        }
        public static void PrintBuy(Buy buy)
        {
            if (buy == null)
            {
                throw new ArgumentNullException(nameof(buy));
            }
            Console.WriteLine("Information about product:");
            PrintProduct(buy.Product);
            Console.WriteLine("Information about buy:");
            Console.WriteLine($"Count: {buy.Count}");
            Console.WriteLine($"Overall cost: ${buy.OverallCost}");
            Console.WriteLine($"Overall weight: {buy.OverallWeight} kg");
        }
    }
}
