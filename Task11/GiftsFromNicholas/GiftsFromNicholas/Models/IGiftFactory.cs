using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftsFromNicholas.Models
{
    public interface IGiftFactory
    {
        Gift CreateGift(ChildStats stats, bool ignoreBad);
    }
}
