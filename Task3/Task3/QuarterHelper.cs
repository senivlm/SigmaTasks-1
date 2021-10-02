using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class QuarterHelper
    {
        private QuarterHelper() { }
        public static readonly Dictionary<Quarter, string[]> QuarterMonths = new Dictionary<Quarter, string[]>
        {
            { Quarter.First, new string[] { "January", "February", "March" } },
            { Quarter.Second, new string[] { "April", "May", "June" } },
            { Quarter.Third, new string[] { "July", "August", "September" } },
            { Quarter.Fourth, new string[] { "October", "November", "December" } }
        };
    }
}
