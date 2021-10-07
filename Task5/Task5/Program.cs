using System;
using System.IO;
using System.Text;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                string filePath = @"D:\Workspace\SigmaTasks\Task5\Task5\text.txt";
                Replacer replacer = new(filePath);
                Console.WriteLine(replacer.ToString());
                FilePathHandler handler = new(filePath);
                Console.WriteLine(handler.GetFileName());
                Console.WriteLine(handler.GetRootDirectory());
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Unsupported file extensions.");
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid text.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
