using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public abstract class GiftBuilder
    {
        protected GiftBuilder(ChildStats stats) { }
        public abstract void ChooseToy();
        public abstract void ChooseSweets();
        public abstract void ChooseWish();
        public abstract Gift GetGift();
    }
}
