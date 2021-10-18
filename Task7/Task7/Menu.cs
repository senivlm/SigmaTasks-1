using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task7
{
    public class Menu
    {Чому відкрите поле? назва поля з маленької літери
        public  Dictionary<string, DishInfo> Dishes;

        public Menu()
        {
            Dishes = new();
        }

        public Menu(string pathToDishesWeight, string pathToDishesPrice)
        {
            Dishes = new();
            ReadDishesWeight(pathToDishesWeight);
            ReadDishesPrice(pathToDishesPrice);
        }

        public void ReadDishesWeight(string pathToFile)
        {
            if (String.IsNullOrWhiteSpace(pathToFile))
            {
                throw new ArgumentException("Argument is either null or whitespace.", nameof(pathToFile));
            }
            if (!File.Exists(pathToFile))
            {
                throw new FileNotFoundException("File not found.", pathToFile);
            }

            if (String.Compare(new FileInfo(pathToFile).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pathToFile), "Only text (.txt) files are supported.");
            }

            using (StreamReader reader = new(pathToFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {Регулярні вирази - це добре!
                    if (!Regex.IsMatch(line, @"^\w+\s+\d+\.?\d*"))
                    {
                        throw new FormatException("File does not suit the format.");
                    }

                    string[] split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    string name = split[0];
                    if (!Double.TryParse(split[1], out double weight))
                    {
                        throw new FormatException("Failed to parse weight.");
                    }

                    if (Dishes.ContainsKey(name))
                    {
                        Dishes[name].Weight += weight;
                    }
                    else
                    {
                        Dishes.Add(name, new DishInfo(weight, 0.0));
                    }
                }
            }
        }

        public void ReadDishesPrice(string pathToFile)
        {
            if (String.IsNullOrWhiteSpace(pathToFile))
            {
                throw new ArgumentException("Argument is either null or whitespace.", nameof(pathToFile));
            }
            if (!File.Exists(pathToFile))
            {
                throw new FileNotFoundException("File not found.", pathToFile);
            }

            if (String.Compare(new FileInfo(pathToFile).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pathToFile), "Only text (.txt) files are supported.");
            }
            using (StreamReader reader = new(pathToFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!Regex.IsMatch(line, @"^\w+\s+\d+\.?\d*"))
                    {
                        throw new FormatException("File does not suit the format.");
                    }

                    string[] split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    string name = split[0];
                    if (!Double.TryParse(split[1], out double price))
                    {
                        throw new FormatException("Failed to parse price.");
                    }

                    if (Dishes.ContainsKey(name))
                    {
                        if (Dishes[name].Price != 0.0)
                        {
                            throw new InvalidOperationException($"{name} already has set price.");
                        }

                        Dishes[name].Price = price * Dishes[name].Weight;
                    }
                    else
                    {
                        Dishes.Add(name, new DishInfo(0.0, price));
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (KeyValuePair<string, DishInfo> dish in Dishes)
            {
                sb.AppendLine($"{dish.Key,-25}|{dish.Value.Weight,-8:F3}|{dish.Value.Price,-8:F3}|");
            }

            return sb.ToString();
        }
    }
}
