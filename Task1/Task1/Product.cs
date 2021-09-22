using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Product
    {
        public Product(string name, double cost, double weight)
        {
            this.name = name;
            this.cost = cost;
            this.weight = weight;
        }
        public Product(Product product)
        {
            this.name = product.name;
            this.cost = product.cost;
            this.weight = product.weight;
        }

        private string name = "Unnamed";
        private double cost = 0.0;
        private double weight = 0.0;

        public string Name
        {
            get => name;
            set
            {
                if (value == null)
                {
                    return;
                }
                if (string.Compare(value, "") == 0)
                {
                    return;
                }
                name = value;
            }
        }
        public double Cost
        {
            get => cost;
            set
            {
                if (value <= 0.0)
                {
                    return;
                }
                cost = value;
            }
        }
        public double Weight
        {
            get => weight;
            set
            {
                if (value < 0.0)
                {
                    return;
                }
                weight = value;
            }
        }
    }
}
