using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Product
    {
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
                    throw new ArgumentNullException(nameof(value), $"{nameof(Name)} cannot be set to null.");
                }
                if (string.Compare(value, "") == 0)
                {
                    throw new ArgumentException($"{nameof(value)} is empty string.", $"{nameof(Name)} cannot be set to empty string.");
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
                    throw new ArgumentException($"{nameof(value)} is lower than 0.0", $"{nameof(Cost)} cannot be set to negative number.");
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
                    throw new ArgumentException($"{nameof(value)} is lower than 0.0", $"{nameof(Weight)} cannot be set to negative number.");
                }
                weight = value;
            }
        }
        public Product(string name, double cost, double weight)
        {
            this.Name = name;
            this.Cost = cost;
            this.Weight = weight;
        }
        public Product(Product product) : this(product.Name, product.Cost, product.Weight) { }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            var other = obj as Product;
            return String.Compare(this.Name, other.name) == 0 &&
                this.Cost == other.Cost &&
                this.Weight == other.Weight;
        }
        public override string ToString()
        {
            return String.Concat($"Name: {this.Name}\n", $"Cost: ${this.Cost}\n", $"Weight: {this.Weight} kg");
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Cost, this.Weight);
        }
    }
}
