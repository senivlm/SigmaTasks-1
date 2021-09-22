using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new("NVidia GeForce RTX 3090", 799, 0.8);
            Buy buy = new(product, 10);
            Check.PrintBuy(buy);
        }
    }
}
