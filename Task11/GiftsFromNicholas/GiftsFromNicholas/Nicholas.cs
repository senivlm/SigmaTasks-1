using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public class Nicholas
    {
        private Nicholas() { }

        public void MakeGift(string name, int goodActionsCnt, int badActionsCnt, GiftBuilder builder, bool ignoreBad = false)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is either null or whitespace.", nameof(name));
            }

            if (goodActionsCnt < 0)
            {
                throw new ArgumentException("Value cannot be less than 0.", nameof(goodActionsCnt));
            }

            if (badActionsCnt < 0)
            {
                throw new ArgumentException("Value cannot be less than 0.", nameof(badActionsCnt));
            }

            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder), "Value cannot be null.");
            }
            builder.ChooseToy(goodActionsCnt, badActionsCnt, ignoreBad);
            builder.ChooseSweets(ignoreBad);
            builder.ChooseWish(name, goodActionsCnt, badActionsCnt, ignoreBad);
        }

        private static readonly Lazy<Nicholas> _lazy = new(() => new Nicholas());
        public static Nicholas GetInstance() => _lazy.Value;

    }
}
