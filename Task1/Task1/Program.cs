using System;

namespace Task1
{ 
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateMethods();
        }
        static void DemonstrateMethods()
        {
            Product product = new("NVidia GeForce RTX 3090", 799, 0.8);
            Buy buy = new(product, new Product("Intel Core i9-11900K", 500, 0.5));
            Console.WriteLine(Check.PrintBuy(buy));
            Console.WriteLine(product.ToString());
        }
    }

}
