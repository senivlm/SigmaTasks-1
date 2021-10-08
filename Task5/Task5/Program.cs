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
            DemonstrateReplacer();
            DemonstrateFilePathHandler();
            DemonstratePicture3D();
        }

        static void DemonstrateReplacer()
        {
            try
            {
                string filePath = @"D:\Workspace\SigmaTasks\Task5\Task5\text.txt";
                Replacer replacer = new(filePath);
                Console.WriteLine(replacer.ToString());
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

        static void DemonstratePicture3D()
        {
            byte[,,] figure = new byte[3, 3, 3];
            figure[1, 0, 1] = 1;
            figure[1, 1, 1] = 1;
            figure[1, 2, 1] = 1;
            figure[1, 0, 2] = 1;
            figure[0, 1, 1] = 1;
            figure[1, 1, 0] = 1;
            Picture3D picture3D = new(figure);
            var projections = picture3D.GetProjections();
            foreach (var projection in projections)
            {
                Console.WriteLine(projection.ToMatrixView());
            }
        }

        static void DemonstrateFilePathHandler()
        {
            string filePath = @"D:\Workspace\SigmaTasks\Task5\Task5\text.txt";
            FilePathHandler handler = new(filePath);
            try
            {
                Console.WriteLine(handler.GetFileName());
                Console.WriteLine(handler.GetRootDirectory());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Caught an exception.");
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Caught an exception.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
