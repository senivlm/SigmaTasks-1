using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public abstract class GiftBuilder
    {
        protected static readonly string DefaultWish = "Wish you, all your family and friends health and happiness.";
        protected static readonly string DefaultSweets = "Apples";
        protected static readonly string DefaultToy = "Book \"Harry Potter\"";
        protected GiftBuilder() { }
        public abstract void ChooseToy(int goodActionsCnt, int badActionsCnt, bool ignoreBad);
        public abstract void ChooseSweets(bool ignoreBad);
        public abstract void ChooseWish(string name, int goodActionsCnt, int badActionsCnt, bool ignoreBad);
        public abstract Gift GetGift();
    }
}
