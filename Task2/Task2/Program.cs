using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage(3);
            for (int i = 0; i < storage.Length; ++i)
            {
                Console.WriteLine($"Product #{i + 1}");
                Check.PrintProduct(storage[i]);
            }

        }
    }
}
