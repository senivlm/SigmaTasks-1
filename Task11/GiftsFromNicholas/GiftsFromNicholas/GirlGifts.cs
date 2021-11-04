using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public class GirlGifts : GiftCollection
    {
        protected override List<Gift> _gifts { get; }
        private static readonly Lazy<GirlGifts> _lazy = new(() => new GirlGifts());
        
        private GirlGifts()
        {
            _gifts = new()
            {
                new Gift(
                    "Doll", "Jar of \"Chupa-chups\"", "I wish you to be the most wonderful girl in the world."),
                new Gift(
                    "Makeup tools", "Cookies \"Esmeralda\"", "I wish you and all your family good health and happiness."),
                new Gift(
                    "2 dolls", "1 kg of \"Zoriane Syaivo\"", "I wish you success in studying, be the cleverest student in your class")
            };
        }
        public static GirlGifts GetInstance() => _lazy.Value;
    }
}
