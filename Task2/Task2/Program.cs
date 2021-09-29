using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateMethods();
        }
        static void DemonstrateMethods()
        {
            Storage storage = new(3);
            for (int i = 0; i < storage.Length; ++i)
            {
                storage.ChangeCostForAll(10);
                Console.WriteLine($"Product #{i + 1}");
                Console.WriteLine(Check.PrintProduct(storage[i]));
            }
            Storage storage1 = new(
                new Meat("Chicken leg", 43.5, 6, Category.First, Kind.Chicken),
                new Product("NVidia RTX 3090", 799, 0.8),
                new DairyProducts("Bread", 1.0, 0.8, 2)
            );
            Console.WriteLine(storage1.PrintProducts());
            Meat[] meats = storage1.GetAllMeatProducts();
            foreach (Meat meat in meats)
            {
                Console.WriteLine(Check.PrintProduct(meat));
            }
        }
    }
}
