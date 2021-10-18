using System;
using System.Diagnostics;
using System.IO;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
           // DemonstratePolynomials();
            DemonstrateSiteVisiting();
            DemonstrateMatrix();
        }

        private static void DemonstrateMatrix()
        {
            double[,] arr2d = new double[5, 5];
            Matrix matrix = new Matrix(arr2d);
            matrix.Fill();
            Console.WriteLine(matrix.ToString());
            foreach (var num in matrix)
            {
                Console.Write("{0, -7:F3}", num);
            }
            Console.WriteLine();
        }

        private static void DemonstrateSiteVisiting()
        {
            try
            {
                SiteVisitingHelper.FillFileWithRandomIPs(@"D:\Workspace\SigmaTasks\Task6\Task6\ip.txt", 10, 10);
                SiteVisiting visiting = new(@"D:\Workspace\SigmaTasks\Task6\Task6\ip.txt");
                Console.WriteLine(visiting.ToString());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        private static void DemonstratePolynomials()
        {
            try
            {
                Polynomial p1 = Polynomial.Parse("0.56 + -4.53*x^1 + 4.34*x^4");
                var p2 = -p1;
                Console.WriteLine(p2);
                Console.WriteLine(p2 - p1);
                Console.WriteLine(p2 * p1);
                Polynomial p3 = 4.56;
                Console.WriteLine(p3);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }
    }
}
