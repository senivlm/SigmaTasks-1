using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    public class ElectricityAccounting
    {
        private int apartmentCount;
        public double pricePerKiloWatt;
        public Quarter Quarter { get; private set; }
        public int ApartmentCount
        {
            get => apartmentCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Apartment count must be positive value.", nameof(value));
                }
                apartmentCount = value;
            }
        }
        public List<ApartmentInfo> ApartmentsInfo { get; private set; }
        public double PricePerKiloWatt
        {
            get => pricePerKiloWatt; 
            private set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentException("Price per kiloWatt must be positive value.", nameof(value));
                }
                pricePerKiloWatt = value;
            }
        }
        public ElectricityAccounting(string dataPath, double pricePerKiloWatt)
        {
            PricePerKiloWatt = pricePerKiloWatt;
            ReadFromFile(dataPath);
        }
        public void ReadFromFile(string dataPath)
        {
            if (!File.Exists(dataPath))
            {
                throw new FileNotFoundException("File not found.", dataPath);
            }
            if (String.Compare(new FileInfo(dataPath).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(dataPath)}", dataPath, "Only text files are supported (*.txt).");
            }
            using (StreamReader reader = new(dataPath))
            {
                string line;
                line = reader.ReadLine();
                if (String.Compare(line, "") == 0 && line == null)
                {
                    throw new FormatException("File has invalid format.");
                }
                string[] data = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (data.Length < 2)
                {
                    throw new FormatException("File has invalid format.");
                }
                if (!Enum.TryParse(data[1], out Quarter quarter) 
                        || !Int32.TryParse(data[0], out int apartmentCount))
                {
                    throw new FormatException("File has invalid format.");
                }
                this.Quarter = quarter;
                this.ApartmentCount = apartmentCount;
                data = reader.ReadToEnd().Split("\r\n");
                if (data.Length != ApartmentCount)
                {
                    throw new FormatException("Number of records is not equal to count of apartments.");
                }
                const int countOfDataPerLine = 5;
                ApartmentsInfo = new List<ApartmentInfo>();
                for (int i = 0; i < data.Length; ++i)
                {
                    string[] accountingInfo = data[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (accountingInfo.Length != countOfDataPerLine)
                    {
                        throw new FormatException($"Records #{i + 1} is invalid.");
                    }
                    if (!Int32.TryParse(accountingInfo[0], out int apartmentNumber) || apartmentNumber <= 0)
                    {
                        throw new FormatException($"Invalid apartment number in record #{i + 1}");
                    }
                    string owner = accountingInfo[1];
                    Dictionary<int, Tuple<int, int>> dIndicators = new();
                    for (int j = 2; j < accountingInfo.Length; ++j)
                    {
                        string[] indicators = accountingInfo[j].Split('-');
                        if (indicators.Length != 2)
                        {
                            throw new FormatException("Every record must contain input and output indicator.");
                        }
                        if (!Int32.TryParse(indicators[0], out int inputIndicator) 
                                || !Int32.TryParse(indicators[1], out int outputIndicator) 
                                || inputIndicator < 0 
                                || outputIndicator < 0 
                                || inputIndicator > outputIndicator)
                        {
                            throw new FormatException($"Invalid indicator value in record #{i + 1}");
                        }
                        dIndicators.Add(j - 1, new Tuple<int, int>(inputIndicator, outputIndicator));
                    }
                    ApartmentsInfo.Add(new ApartmentInfo(
                            owner,
                            dIndicators,
                            apartmentNumber,
                            this.Quarter,
                            pricePerKiloWatt)
                    );
                }
            }
        }
        public string PrintAllApartments()
        {
            StringBuilder sb = new();
            foreach (var apartmentInfo in ApartmentsInfo)
            {
                sb.Append(apartmentInfo.ToString());
                sb.Append('\n');
            }
            return sb.ToString();
        }
        public string GetApartmentInfo(int apartmentNumber)
        {
            var result = ApartmentsInfo.Where(apartment => apartment.ApartmentNumber == apartmentNumber);
            if (result.Count() == 0)
            {
                throw new ArgumentException("Failed to find apartment with given number.", nameof(apartmentNumber));
            }
            return result.First().ToString();
        }
        public string GetOwnerWithMostArrears()
        {
            return ApartmentsInfo
                .OrderByDescending(apartmentInfo => 
                    apartmentInfo
                    .Indicators
                    .Sum(indicator => (indicator.Value.Item2 - indicator.Value.Item1) * pricePerKiloWatt))
                .First()
                .Owner;
        }
        public int FindApartmentWithoutUsing()
        {
            var result = ApartmentsInfo.Where(
                apartment => apartment
                                .Indicators
                                .Sum(indicators => indicators.Value.Item2 - indicators.Value.Item1) == 0
            );
            if (result.Count() == 0)
            {
                return -1;
            }
            return result.First().ApartmentNumber;
        }
    }
}
