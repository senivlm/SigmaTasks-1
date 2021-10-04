using System;
using System.Collections.Generic;
namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            // DemonstratePolynomial();
            //DemonstrateProduct();
            DemonstrateStorage();
        }

        static void DemonstrateStorage()
        {
            Storage storage = new Storage(@"D:\Workspace\SigmaTasks\Task4\Task4\products.txt");
            Console.WriteLine(storage.PrintProducts());
            storage.RemoveExpiredProducts(@"D:\Workspace\SigmaTasks\Task4\Task4\expired.txt");
        }
        static void DemonstrateProduct()
        {
            Product pr = new Product("NVidia RTX 3090, 45.6, 40.8, 1000, 12.11.2020");
            Console.WriteLine(pr);
        }
        static void DemonstratePolynomial()
        {
            Polynomial p1 = new Polynomial(new List<double>()
            {
                5.0, 0.0, 4.23, 5.78
            });
            Polynomial p2 = new Polynomial(new List<double>()
            {
                0.0, 4.56, 3.24, 7.89, 1.23, 0.0, 5.67
            });
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine(p1.Add(p2));
            var p3 = Polynomial.Parse("2*x^ 1 + 0*x^2 + 4.5*x^3 + -5,5*x^4 + 6*x^5 + 7*x^6 + 8*x^7");
            Console.WriteLine(p3);
            var p4 = new Polynomial().Multiply(4);
            Console.WriteLine(p1.Subtract(p2));
            Console.WriteLine(p4);
            Console.WriteLine(p1.Calculate(5.6));
            Console.WriteLine(p1.Multiply(p2));
        }
    }
}
