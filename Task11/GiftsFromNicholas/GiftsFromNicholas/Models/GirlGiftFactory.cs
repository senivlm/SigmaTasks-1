using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public class GirlGiftFactory : IGiftFactory
    {
        public GirlGiftFactory() { }
        /// <summary>
        /// Creates gift for an object of ChildStats with GirlGiftBuilder
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
            GiftBuilder builder = new GirlGiftBuilder(stats, ignoreBad);
            builder.ChooseToy();
            builder.ChooseSweets();
            builder.ChooseWish();
            return builder.GetGift();
        }
    }
}
