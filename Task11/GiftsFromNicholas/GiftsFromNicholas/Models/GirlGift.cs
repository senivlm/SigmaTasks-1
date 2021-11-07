using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiftsFromNicholas.Models
{
    public class GirlGift : Gift
    {
        [JsonProperty("GirlToy")]
        private GirlToy _toy;
        private Sweets _sweets;
        private string _wish;
        public GirlGift() { }
        public GirlGift(Enum toy, Sweets sweets, string wish)
        {
            Toy = toy;
            Sweets = sweets;
            Wish = wish;
        }
        public override Enum Toy
        {
            get => _toy;
            set
            {
                if (value.GetType() != typeof(GirlToy))
                {
                    throw new ArgumentException("The value should be of type BoyToy.", nameof(value));
                }

                _toy = (GirlToy) value;
            }
        }

        public override Sweets Sweets
        {
            get => _sweets;
            set
            {
                if (!Enum.IsDefined(typeof(Sweets), value))
                {
                    throw new ArgumentException("The value should be defined at BoyToy.", nameof(value));
                }

                _sweets = value;
            }
        }

        public override string Wish
        {
            get => _wish;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The value is either null or whitespace.", nameof(value));
                }

                _wish = value;
            }
        }
    }
}
