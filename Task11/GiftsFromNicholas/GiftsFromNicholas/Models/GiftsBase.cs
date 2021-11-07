using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiftsFromNicholas.Models
{
    public class GiftsBase
    { 
        [JsonProperty("BoyGifts")]
        private List<BoyGift> _boyGifts;
        [JsonProperty("GirlGifts")]
        private List<GirlGift> _girlGifts;
        private int _boyCounter;
        private int _girlCounter;
        public GiftsBase()
        {
            _boyGifts = new();
            _girlGifts = new();
            _boyCounter = 0;
            _girlCounter = 0;
        }

        public Gift GetCurrentBoyGift()
        {
            if (!_boyGifts.Any())
            {
                throw new InvalidOperationException("Cannot get gift because list is empty.");
            }
            return _boyGifts[_boyCounter];
        }

        public Gift GetCurrentGirlGift()
        {
            if (!_girlGifts.Any())
            {
                throw new InvalidOperationException("Cannot get gift because list is empty.");
            }
            return _girlGifts[_girlCounter];
        }

        public void IncrementBoyCounter()
        {
            if (_boyCounter + 1 >= _boyGifts.Count)
            {
                _boyCounter = -1;
            }

            _boyCounter++;
        }

        public void IncrementGirlCounter()
        {
            if (_girlCounter + 1 >= _girlGifts.Count)
            {
                _girlCounter = -1;
            }

            _girlCounter++;
        }

        public Gift GetRandomBoyGift()
        {
            if (!_boyGifts.Any())
            {
                throw new InvalidOperationException("Cannot get gift because list is empty.");
            }

            Random random = new();
            return _boyGifts[random.Next(0, _boyGifts.Count)];
        }
        public Gift GetRandomGirlGift()
        {
            if (!_girlGifts.Any())
            {
                throw new InvalidOperationException("Cannot get gift because list is empty.");
            }

            Random random = new();
            return _girlGifts[random.Next(0, _girlGifts.Count)];
        }
    }
}
