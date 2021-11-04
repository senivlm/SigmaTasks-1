using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public class BoyGifts : GiftCollection
    {
        protected override List<Gift> _gifts { get; }
        private static readonly Lazy<BoyGifts> _lazy = new(() => new BoyGifts());

        private BoyGifts()
        {
            _gifts = new()
            {
                new Gift(
                    "Car", "Cookies \"Esmeralda\"", "I wish you and all your family good health and happiness."),
                new Gift(
                    "Robot", "Jar of \"Chupa-chups\"", "I wish you success in studying, be the cleverest student in your class."),
                new Gift(
                    "Dumbbells", "1 kg of \"Zoriane Syaivo\"", "Be strong, clever and hard-working.")
            };
        }

        public static BoyGifts GetInstance() => _lazy.Value;
    }
}
