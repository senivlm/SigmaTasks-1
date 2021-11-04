using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public class BoyGiftBuilder : GiftBuilder
    {
        private Gift _gift;

        public BoyGiftBuilder()
        {
            _gift = new();
        }
        public override void ChooseToy(int goodActionsCnt, int badActionsCnt, bool ignoreBad)
        {
            GiftCollection gifts = BoyGifts.GetInstance();
            try
            {
                if (ignoreBad)
                {
                    _gift.Toy = gifts.GetCurrentGift().Toy;
                }
                else
                {
                    _gift.Toy = goodActionsCnt < badActionsCnt ? "Rizka" : gifts.GetRandomGift().Toy;
                }
            }
            catch (InvalidOperationException)
            {
                _gift.Toy = DefaultToy;
            }
        }

        public override void ChooseSweets(bool ignoreBad)
        {
            GiftCollection gifts = BoyGifts.GetInstance();
            try
            {
                _gift.Sweets = ignoreBad ? gifts.GetCurrentGift().Sweets : gifts.GetRandomGift().Sweets;
            }
            catch (InvalidOperationException)
            {
                _gift.Sweets = DefaultSweets;
            }
        }

        public override void ChooseWish(string name, int goodActionsCnt, int badActionsCnt, bool ignoreBad)
        {
            GiftCollection gifts = BoyGifts.GetInstance();
            try
            {
                if (ignoreBad)
                {
                    _gift.Wish = $"Dear {name},\n" + gifts.GetCurrentGift().Wish;
                }
                else
                {
                    _gift.Wish = goodActionsCnt < badActionsCnt
                        ? $"Dear {name},\n" +
                          "Please next time be courteous, but still I wish you and your family good health and happiness."
                        : $"Dear {name},\n" + gifts.GetRandomGift().Wish;
                }
            }
            catch (InvalidOperationException)
            {
                _gift.Wish = $"Dear {name},\n" + DefaultWish;
            }
        }

        public override Gift GetGift()
        {
            BoyGifts.GetInstance().IncrementCounter();
            return _gift;
        }
    }
}
