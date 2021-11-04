using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas
{
    public abstract class GiftCollection
    {
        protected int _counter; 
        protected abstract List<Gift> _gifts { get; }

        protected GiftCollection()
        {
            _counter = 0;
        }
        public Gift GetCurrentGift()
        {
            if (!_gifts.Any())
            {
                throw new InvalidOperationException(
                    "Gift collection is empty. Nicholas does not have anything to gift."
                );
            }

            return _gifts[_counter];
        }

        public Gift GetRandomGift()
        {
            if (!_gifts.Any())
            {
                throw new InvalidOperationException(
                    "Gift collection is empty. Nicholas does not have anything to gift."
                );
            }

            Random random = new();
            return _gifts[random.Next(0, _gifts.Count)];
        }

        public void IncrementCounter()
        {
            if (_counter + 1 >= _gifts.Count())
            {
                _counter = -1;
            }

            _counter++;
        }
    }
}
