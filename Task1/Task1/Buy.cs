using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Buy
    {
        private int count;
        private Product product;
        public Product Product
        {
            get => product;
        }

        public double OverallCost
        {
            get => product.Cost * count;
        }
        public double OverallWeight
        {
            get => product.Weight * count;
        }
        public int Count
        {
            get => count;
            private set
            {
                if (value < 1)
                {
                    count = 1;
                    return;
                }
                count = value;
            }
        }
        public Buy(Product product, int count = 1)
        {
            this.product = new Product(product);
            this.Count = count;
        }
    }
}
