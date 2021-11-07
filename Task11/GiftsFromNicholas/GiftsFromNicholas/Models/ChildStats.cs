using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public class ChildStats
    {
        private readonly int _goodActionsCnt;
        private readonly int _badActionsCnt;
        private readonly string _name;

        /// <summary>
        /// Initializes ChildStats object.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="goodActionsCnt"></param>
        /// <param name="badActionsCnt"></param>
        /// <param name="name"></param>
        public ChildStats(int goodActionsCnt, int badActionsCnt, string name)
        {
            if (goodActionsCnt < 0)
            {
                throw new ArgumentException("The value cannot be less than 0.", nameof(goodActionsCnt));
            }

            if (badActionsCnt < 0)
            {
                throw new ArgumentException("The value cannot be less than 0.", nameof(badActionsCnt));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The value is either null or whitespace", nameof(name));
            }

            _goodActionsCnt = goodActionsCnt;
            _badActionsCnt = badActionsCnt;
            _name = name;
        }

        public (int GoodActions, int BadActions, string Name) GetStats()
        {
            return (_goodActionsCnt, _badActionsCnt, _name);
        }
    }
}
