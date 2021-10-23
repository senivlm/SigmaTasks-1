using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Product
    {
        protected string _name = "Unnamed";
        protected double _cost = 0.0;
        protected double _weight = 0.0;
        protected int _daysToExpiration = 0;
        protected DateTime _madeDate = DateTime.Now;
        public string Name
        {
            get => _name;
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
                _name = value;
            }
        }
        public double Cost
        {
            get => _cost;
            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException($"{nameof(value)} is lower than 0.0", $"{nameof(Cost)} cannot be set to negative number.");
                }
                _cost = value;
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException($"{nameof(value)} is lower than 0.0", $"{nameof(Weight)} cannot be set to negative number.");
                }
                _weight = value;
            }
        }
        public int DaysToExpiration
        {
            get => _daysToExpiration;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(DaysToExpiration)} cannot be set to negative value.", nameof(value));
                }
                _daysToExpiration = value;
            }
        }
        public DateTime MadeDate => _madeDate;
        public bool IsExpired => _madeDate.AddDays(_daysToExpiration) < DateTime.Now;

        public Product(string name, double cost, double weight, int daysToExpiration, DateTime madeDate)
        {
            this.Name = name;
            this.Cost = cost;
            this.Weight = weight;
            this.DaysToExpiration = daysToExpiration;
            if (madeDate > DateTime.Now)
            {
                throw new ArgumentException("Made date cannot be bigger than today's date.", nameof(madeDate));
            }
            this._madeDate = madeDate;
        }
        public Product(Product product) : this(
            product.Name,
            product.Cost,
            product.Weight, 
            product.DaysToExpiration,
            product.MadeDate
        ) { }
        public Product(string s)
        {
            Parse(s);
        }

        public Product() { }

        public virtual void ChangeCost(double percents)
        {
            if (percents < -100.0)
            {
                throw new ArgumentException($"Invalid percents passed. {nameof(percents)} must be greater or equal to -100");
            }
            double change = Cost * percents / 100.0;
            Cost += change;
        }
        public virtual void Parse(string s)
        {
            var fields = s.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length < 5 || fields.Length > 5)
            {
                throw new FormatException($"{nameof(s)} has invalid format. It must consist of 5 non-whitespace values separated by comma (\", \")");
            }
            this.Name = fields[0];
            double cost, weight;
            if (!Double.TryParse(fields[1], out cost) | !Double.TryParse(fields[2], out weight))
            {
                throw new FormatException("Couldn't parse cost or weight.");
            }
            int daysToExpiration;
            if (!Int32.TryParse(fields[3], out daysToExpiration))
            {
                throw new FormatException("Couldn't parse days to expiration.");
            }

            if (!DateTime.TryParseExact(fields[4], "dd.MM.yyyy", null, DateTimeStyles.None, out _madeDate))
            {
                throw new FormatException("Couldn't parse made date.");
            }
            this.Cost = cost;
            this.Weight = weight;
            this.DaysToExpiration = daysToExpiration;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            var other = obj as Product;
            return String.Compare(this.Name, other._name) == 0 &&
                this.Cost == other.Cost &&
                this.Weight == other.Weight;
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Cost: {this.Cost}");
            sb.AppendLine($"Weight: {this.Weight}");
            sb.AppendLine($"Days to expiration: {this.DaysToExpiration}");
            sb.AppendLine($"Made date: {this.MadeDate:dd.MM.yyyy}");
            sb.AppendLine($"Is expired: {this.IsExpired}");
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Cost, this.Weight, this.DaysToExpiration, this.MadeDate);
        }
    }
}
