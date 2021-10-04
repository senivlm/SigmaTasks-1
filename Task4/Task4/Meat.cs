using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class Meat : Product
    {
        public Category Category { get; set; }
        public Kind Kind { get; set; }
        public Meat(
            string name, 
            double cost, 
            double weight,
            int daysToExpiration,
            DateTime madeDate,
            Category category, 
            Kind kind
            ) : base(name, cost, weight, daysToExpiration, madeDate) 
        {
            Category = category;
            Kind = kind;
        }
        public Meat(Meat meat) : base(meat)
        {
            Category = meat.Category;
            Kind = meat.Kind;
        }

        public Meat(string s) : base()
        {
            Parse(s);
        }

        public override void Parse(string s)
        {
            var fields = s.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length != 7)
            {
                throw new FormatException($"{nameof(s)} has invalid format. It must consist of 7 non-whitespace values separated by comma (\", \")");
            }

            base.Parse(String.Join(", ", fields.Where((_, index) => index < 5)));
            if (!Enum.TryParse(typeof(Category), fields[5], true, out object category) | 
                !Enum.TryParse(typeof(Kind), fields[6], true, out object kind))
            {
                throw new FormatException("Couldn't parse category or kind.");
            }
            this.Category = (Category) category;
            this.Kind = (Kind) kind;
        }

        public override void ChangeCost(double percents)
        {
            if (percents < -100.0)
            {
                throw new ArgumentException($"Invalid percents passed. {nameof(percents)} must be greater or equal to -100");
            }
            base.ChangeCost(percents);
            switch (this.Category)
            {
                case Category.First:
                    base.ChangeCost(-5.0);
                    break;
                case Category.Second:
                    base.ChangeCost(-10.0);
                    break;
                default:
                    break;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            var other = obj as Meat;
            return this.Category.Equals(other.Category) &&
                this.Kind.Equals(other.Kind) &&
                base.Equals(obj);
        }
        public override string ToString()
        {
            return String.Concat($"Category: {this.Category.ToString()}\n",
                $"Kind: {this.Kind.ToString()}\n",
                base.ToString());
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Category, this.Kind, base.GetHashCode());
        }
    }
}
