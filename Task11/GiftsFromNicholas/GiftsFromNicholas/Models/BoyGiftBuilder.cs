using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public class BoyGiftBuilder : GiftBuilder
    {
        private readonly Gift _gift;
        private readonly ChildStats _stats;
        private readonly bool _ignoreBad;

        /// <summary>
        /// Initializes BoyGiftBuilder object with values.
        /// </summary>
        /// <param name="stats">Contains child's stats throughout the year.</param>
        /// <param name="ignoreBad">Defines the way of building gift.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BoyGiftBuilder(ChildStats stats, bool ignoreBad) : base(stats)
        {
            _gift = new BoyGift();
            _stats = stats ?? throw new ArgumentNullException(nameof(stats), "The value is null.");
            _ignoreBad = ignoreBad;
        }

        public override void ChooseToy()
        {
            GiftsBase gBase = Nicholas.Instance.Gifts;
            try
            {
                if (_ignoreBad)
                {
                    _gift.Toy = gBase.GetCurrentBoyGift().Toy;
                }
                else
                {
                    (int goodActions, int badActions, _) = _stats.GetStats();
                    _gift.Toy = goodActions < badActions ? BoyToy.Rod : gBase.GetRandomBoyGift().Toy;
                }
            }
            catch (InvalidOperationException)
            {
                // Default Toy value.
                _gift.Toy = BoyToy.Car;
            }
        }

        public override void ChooseSweets()
        {
            GiftsBase gBase = Nicholas.Instance.Gifts;
            try
            {
                _gift.Sweets = _ignoreBad
                    ? gBase.GetCurrentBoyGift().Sweets
                    : gBase.GetRandomBoyGift().Sweets;
            }
            catch (InvalidOperationException)
            {
                // Default Sweets value.
                _gift.Sweets = Sweets.Cookies;
            }
        }

        public override void ChooseWish()
        {
            GiftsBase gBase = Nicholas.Instance.Gifts;
            (_, _, string name) = _stats.GetStats();
            try
            {
                _gift.Wish = _ignoreBad
                    ? $"Dear {name},\n" + gBase.GetCurrentBoyGift().Wish
                    : $"Dear {name},\n" + gBase.GetRandomBoyGift().Wish;
            }
            catch (InvalidOperationException)
            {
                _gift.Wish = $"Dear {name},\nI wish you and all your family and friends good health and success.";
            }
        }

        public override Gift GetGift()
        {
            if (!_ignoreBad)
            {
                Nicholas.Instance.Gifts.IncrementBoyCounter();
            }
            return _gift;
        }
    }
}
