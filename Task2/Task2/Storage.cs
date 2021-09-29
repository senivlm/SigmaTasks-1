using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Storage
    {
        private Product[] products = null;
        public Product this[int index]
        {
            get
            {
                if (products == null || index < 0 || index >= products.Length)
                {
                    throw new IndexOutOfRangeException($"Index was out of range. Must be non-negative and less than the size of the array. Parameter name: {nameof(index)}");
                }
                return products[index];
            }
        }
        public int Length
        {
            get => products.Length;
        }
        public void ReadProducts(int count)
        {
            if (count < 1)
            {
                throw new ArgumentException($"{nameof(count)} is lower than 1.");
            }
            if (products == null || products.Length != count)
            {
                products = new Product[count];
            }
            for (int i = 0; i < count; ++i)
            {
                Console.WriteLine($"Product #{i + 1}");
                Console.Write("Select type: Product(1), Meat(2), DairyProduct(3): ");
                int chose = Int32.Parse(Console.ReadLine());
                string name;
                double cost, weight;
                Console.Write("Name: ");
                name = Console.ReadLine();
                Console.Write("Cost: ");
                cost = Double.Parse(Console.ReadLine());
                Console.Write("Weight: ");
                weight = Double.Parse(Console.ReadLine());
                switch (chose)
                {
                    case 2:
                        Console.Write("Category: Highest (0), First (1), Second(2): ");
                        Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                        Console.Write("Kind: Sheep(0), Beef(1), Pork(2), Chicken(3): ");
                        Kind kind = (Kind)Enum.Parse(typeof(Kind), Console.ReadLine());
                        products[i] = new Meat(name, cost, weight, category, kind);
                        break;
                    case 3:
                        Console.Write("Days to expiration: ");
                        int daysToExpiration = Int32.Parse(Console.ReadLine());
                        products[i] = new DairyProducts(name, cost, weight, daysToExpiration);
                        break;
                    default:
                        products[i] = new Product(name, cost, weight);
                        break;
                }

            }
        }
        public string PrintProducts()
        {
            StringBuilder sb = new();
            for (int i = 0; i < products.Length; ++i)
            {
                sb.Append($"Product #{i + 1}\n");
                sb.Append(Check.PrintProduct(products[i]) + "\n");
            }
            return sb.ToString();
        }
        public Meat[] GetAllMeatProducts()
        {
            return products.Where(prod => prod as Meat != null).Select(meat => meat as Meat).ToArray();
        }
        public void ChangeCostForAll(double percents)
        {
            for (int i = 0; i < products.Length; ++i)
            {
                products[i].ChangeCost(percents);
            }
        }
        public Storage(params Product[] products)
        {
            this.products = new Product[products.Length];
            for (int i = 0; i < products.Length; ++i)
            {
                if (products[i] == null)
                {
                    throw new ArgumentNullException($"products[{i}] is null");
                }
                this.products[i] = new Product(products[i]);
            }
        }
        public Storage(int count)
        {
            products = new Product[count];
            ReadProducts(count);
        }
    }
}
