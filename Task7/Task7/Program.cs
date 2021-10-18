using System;
using System.IO;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            DemonstrateReplacer();
            DemonstrateMenu();
        }

        private static void DemonstrateMenu()
        {
            try
            {
                Menu menu = new(@"D:\Workspace\SigmaTasks\Task7\Task7\Menu.txt",
                    @"D:\Workspace\SigmaTasks\Task7\Task7\Prices.txt");
                Console.WriteLine(menu);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Exception was caught.");
                Console.WriteLine(ex);
            }
            catch (FileNotFoundException ex)
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

        private static void DemonstrateReplacer()
        {
            try
            {
                Replacer replacer = new(@"D:\Workspace\SigmaTasks\Task7\Task7\Dictionary.txt");
                Console.WriteLine(replacer.TranslateTextFromFile(@"D:\Workspace\SigmaTasks\Task7\Task7\Text.txt"));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Exception was caught.");
                Console.WriteLine(ex);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Exception was caught.");
                Console.WriteLine(ex);
            }
        }
    }
}
