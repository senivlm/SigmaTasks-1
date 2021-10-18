using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    public class DishInfo
    {
        private double _weight;
        private double _price;

        public double Weight
        {
            get => _weight;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Weight must be greater than 0.");
                }

                _weight = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Price must be greater than 0.");
                }

                _price = value;
            }
        }

        public DishInfo(double weight, double price)
        {
            Weight = weight;
            Price = price;
        }
    }
}
