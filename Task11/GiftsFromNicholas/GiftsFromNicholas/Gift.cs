using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public class Gift
    {
        private string _toy;
        private string _sweets;
        private string _wish;
        public string Toy
        {
            get => _toy;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value is either null or whitespace.", nameof(value));
                }

                _toy = value;
            }
        }

        public string Sweets
        {
            get => _sweets;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value is either null or whitespace.", nameof(value));
                }

                _sweets = value;
            }
        }

        public string Wish
        {
            get => _wish;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value is either null or whitespace.", nameof(value));
                }

                _wish = value;
            }
        }

        public Gift()
        {
            _toy = "";
            _sweets = "";
            _wish = "";
        }

        public Gift(string toy, string sweets, string wish)
        {
            Toy = toy;
            Sweets = sweets;
            Wish = wish;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Toy: {Toy}");
            sb.AppendLine($"Sweets: {Sweets}");
            sb.AppendLine($"Wish: {Wish}");
            return sb.ToString();
        }
    }
}
