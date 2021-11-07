using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using GiftsFromNicholas.Models;

namespace GiftsFromNicholas
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DemonstrateNicholas();  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has occurred while demonstrating Nicholas.");
                Console.WriteLine(ex);
            }
            
        }

        private static void DemonstrateNicholas()
        {
            Nicholas nicholas = Nicholas.Instance;
            int count;
            do
            {
                Console.Write("Enter count of children: ");
            } while (Int32.TryParse(Console.ReadLine(), out count) && count < 0);

            bool ignoreBad;
            do
            {
                Console.Write("Ignore bad actions (true|false): ");
            } while (!bool.TryParse(Console.ReadLine(), out ignoreBad));
            List<Gift> gifts = new();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Child #{i + 1}");
                int goodActions, badActions;
                string name;
                do
                {
                    Console.Write("Enter child's name: ");
                } while (!Regex.IsMatch(name = Console.ReadLine(), @"^\w+$"));

                do
                {
                    Console.Write("Enter child's good actions count: ");
                } while (!Int32.TryParse(Console.ReadLine(), out goodActions) && goodActions < 0);

                do
                {
                    Console.Write("Enter child's bad actions count: ");
                } while (Int32.TryParse(Console.ReadLine(), out badActions) && badActions < 0);

                IGiftFactory factory;
                string gender;
                do
                {
                    Console.Write("Enter child's gender: ");
                } while (!Regex.IsMatch(gender = Console.ReadLine(), @"^(male)|(female)$", RegexOptions.IgnoreCase));

                if (String.Compare(gender, "male", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    factory = new BoyGiftFactory();
                }
                else
                {
                    factory = new GirlGiftFactory();
                }

                ChildStats stats = new(goodActions, badActions, name);
                Gift gift = nicholas.GiveGift(stats, factory, ignoreBad);
                gifts.Add(gift);
            }

            Console.WriteLine("Gifts:");
            foreach (Gift gift in gifts)
            {
                Console.WriteLine(gift);
            }
        }
    }
}
