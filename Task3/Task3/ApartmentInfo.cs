using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ApartmentInfo
    {
        private string owner;

        // TKey - number of month in the given quarter
        // TValue - Tuple with two integers. Item1 - input indicator, Item2 - output indicator
        private Dictionary<int, Tuple<int, int>> indicators;

        private int apartmentNumber;
        private Quarter quarter;
        private double pricePerKiloWatt;

        public string Owner { get => owner; }
        public Dictionary<int, Tuple<int, int>> Indicators { get => indicators; }
        public int ApartmentNumber { get => apartmentNumber; }
        public Quarter Quarter { get => quarter; }
        public double PricePerKiloWatt { get => pricePerKiloWatt; }
        public ApartmentInfo(
            string owner,
            Dictionary<int, Tuple<int, int>> indicators,
            int apartmentNumber,
            Quarter quarter,
            double pricePerKiloWatt
        )
        {
            if (String.Compare(owner, "") == 0)
            {
                throw new ArgumentException("Owner must not be empty string.",
                    nameof(owner));
            }
            if (indicators.Count != 3)
            {
                throw new ArgumentException("Too less or too many elements in indicators.",
                    nameof(indicators));
            }
            if (apartmentNumber <= 0)
            {
                throw new ArgumentException("Invalid value for ApartmentNumber. It must be positive value.");
            }
            if (pricePerKiloWatt <= 0.0)
            {
                throw new ArgumentException("Invalid value for PricePerKiloWatt. It must be positive value.",
                    nameof(pricePerKiloWatt));
            }
            this.owner = owner;
            this.indicators = indicators.ToDictionary(
                entry => entry.Key,
                entry => entry.Value
            );
            this.apartmentNumber = apartmentNumber;
            this.quarter = quarter;
            this.pricePerKiloWatt = pricePerKiloWatt;
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"Owner: {owner}\n");
            sb.Append($"Number: {ApartmentNumber}\n");
            sb.Append($"Quarter: {Quarter}\n");
            for (int i = 0; i < Indicators.Count; i++)
            {
                string month = QuarterHelper.QuarterMonths[this.Quarter][i];
                sb.Append($"\n{month}: \n");
                sb.Append($"Input indicator: {Indicators[i + 1].Item1}\n");
                sb.Append($"Output indicator: {Indicators[i + 1].Item2}\n\n");
            }
            double toPay = indicators
                .Sum(indicator => (indicator.Value.Item2 - indicator.Value.Item1) * pricePerKiloWatt);
            sb.Append(String.Format("To pay: {0:F2} UAH\n", toPay));
            sb.Append("-------------------------------------\n");
            return sb.ToString();
        }
    }
}
