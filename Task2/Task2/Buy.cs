using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Buy
    {
        private List<Product> products;
        public List<Product> Products
        {
            get => products;
        }
        public int Count
        {
            get => Products.Count;
        }

        public double OverallCost
        {
            get => Products.Sum(product => product.Cost);
        }
        public double OverallWeight
        {
            get => Products.Sum(product => product.Weight);
        }
        public Buy(params Product[] products)
        {
            this.products = new List<Product>();
            for (int i = 0; i < products.Length; ++i)
            {
                if (products[i] == null)
                {
                    throw new ArgumentNullException($"products[{i}] is null.");
                }
                Products.Add(new Product(products[i]));
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"Count: {Count}\n");
            sb.Append(String.Join("\n", Products.Select((product, index) => $"Product #{index + 1}:\n{product.ToString()}")));
            sb.Append($"Overall weight: {OverallWeight}\n");
            sb.Append($"Overall cost: {OverallCost}\n");
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            var other = obj as Buy;
            return this.Count == other.Count &&
                this.OverallWeight == other.OverallWeight &&
                this.OverallCost == other.OverallCost &&
                this.Products.Equals(other.Products);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Count, this.OverallCost, this.OverallWeight, this.Products);
        }
    }
}
