using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ElectricityAccounting accounting = new(@"D:\Workspace\SigmaTasks\Task3\Task3\data.txt", 5);
            int apartmentNumber = accounting.FindApartmentWithoutUsing();
            if (apartmentNumber < 0)
            {
                Console.WriteLine("There is no apartment where electricity wasn't used.");
            }
            else
            {
                Console.WriteLine($"Found apartment where electricity wasn't used: {apartmentNumber}");
            }
            try
            {
                Console.WriteLine(accounting.GetApartmentInfo(110));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Exception was caught!");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine(accounting.PrintAllApartments());
            Console.WriteLine(accounting.GetOwnerWithMostArrears());
            MagicSquareOdd magicSquare = new(15);
            Console.WriteLine(magicSquare.ToString());
            magicSquare.Size = 11;
            Console.WriteLine(magicSquare.ToString());
        }
    }
}
