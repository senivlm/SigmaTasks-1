using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public class BoyGiftFactory : IGiftFactory
    {
        public BoyGiftFactory() { }
        /// <summary>
        /// Creates gift for an object of ChildStats with BoyGiftBuilder
        /// </summary>
        /// <param name="stats">Contains child's stats throughout the year.</param>
        /// <param name="ignoreBad">Defines the way of choosing gift.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public Gift CreateGift(ChildStats stats, bool ignoreBad)
        {
            if (stats == null)
            {
                throw new ArgumentNullException(nameof(stats), "The value is null.");
            }
            GiftBuilder builder = new BoyGiftBuilder(stats, ignoreBad);
            builder.ChooseToy();
            builder.ChooseSweets();
            builder.ChooseWish();
            return builder.GetGift();
        }
    }
}
