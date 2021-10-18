using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Task6
{
    public class SiteVisiting
    {
        private Dictionary<string, (List<TimeSpan>, List<DayOfWeek>)> _visiting;

        public (List<TimeSpan>, List<DayOfWeek>) this[string ip]
        {
            get
            {
                if (!_visiting.ContainsKey(ip))
                {
                    throw new IndexOutOfRangeException("We don't have information about such IP address.");
                }

                return _visiting[ip];
            }
        }

        public SiteVisiting(string filePath)
        {
            _visiting = new Dictionary<string, (List<TimeSpan>, List<DayOfWeek>)>();
            ReadFromFile(filePath);
        }

        public void ReadFromFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path is either null or empty or white space only.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            if (String.CompareOrdinal(new FileInfo(filePath).Extension, ".txt") != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(filePath), "Only text (.txt) files are supported.");
            }

            using StreamReader reader = new(filePath);
            string[] lines = reader.ReadToEnd().Split("\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (!IPDataTryParse(line, out var data))
                {
                    throw new FormatException("Line has invalid format");
                }
                AddInfo(data);
            }
        }

        public void AddInfo((string, TimeSpan, DayOfWeek) info)
        {
            if (!_visiting.ContainsKey(info.Item1))
            {
                _visiting.Add(
                    info.Item1,
                    (new List<TimeSpan>() { info.Item2 } ,
                        new List<DayOfWeek>() { info.Item3 } )
                    );
            }
            else
            {
                _visiting[info.Item1].Item1.Add(info.Item2);
                _visiting[info.Item1].Item2.Add(info.Item3);
            }
        }
        public bool IsValidIP(string ip)
        {Тут теж класична задача для регулярки!
            string[] ss = ip.Split(".");
            if (ss.Length != 4)
            {
                return false;
            }

            foreach (string s in ss)
            {
                if (!Int32.TryParse(s, out _))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IPDataTryParse(string s, out (string, TimeSpan, DayOfWeek) record)
        {
            string[] data = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            record = new("", TimeSpan.FromSeconds(0), DateTime.Now.DayOfWeek);
            if (data.Length != 3)
            {
                return false;
            }

            if (!IsValidIP(data[0]))
            {
                return false;
            }
            if (!TimeSpan.TryParseExact(data[1], @"hh\:mm\:ss", null, TimeSpanStyles.None, out TimeSpan time))
            {
                return false;
            }

            if (!Enum.TryParse(typeof(DayOfWeek), data[2], true, out object day))
            {
                return false;
            }

            record = new(data[0], time, (DayOfWeek) day);
            return true;
        }

        public Dictionary<string, (int, int)> GetMostPopularTimeForEachIP()
        {

            Dictionary<string, (int, int)> ipTimes = new();
            foreach (var ipInfo in _visiting)
            {
                if (ipInfo.Value.Item1.Count == 1 ||
                    ipInfo.Value.Item1.DistinctBy(timeSpan => timeSpan.Hours).Count() == 1)
                {
                    int hour = ipInfo.Value.Item1[0].Hours;
                    ipTimes.Add(ipInfo.Key, (hour, hour));
                }
                else
                {
                    int hour1 = ipInfo.
                        Value
                        .Item1
                        .GroupBy(timeSpan => timeSpan.Hours)
                        .OrderByDescending(hourGr => hourGr.Count())
                        .First()
                        .Key;
                    int hour2;
                    try
                    {
                        hour2 = ipInfo
                            .Value
                            .Item1
                            .Where(timeSpan => Math.Abs(timeSpan.Hours - hour1) <= 1 && timeSpan.Hours != hour1)
                            .GroupBy(timeSpan => timeSpan.Hours)
                            .OrderByDescending(hourGr => hourGr.Count())
                            .First()
                            .Key;
                        ipTimes.Add(ipInfo.Key, hour1 > hour2 ? (hour2, hour1) : (hour1, hour2));
                    }
                    catch (InvalidOperationException)
                    {
                        ipTimes.Add(ipInfo.Key, (hour1, hour1));
                    }
                }
            }
            return ipTimes;
        }

        public (int, int) GetMostPopularTime()
        {
            if (_visiting.Count == 0 ||
                _visiting.SelectMany(visit => visit.Value.Item1).DistinctBy(timeSpan => timeSpan.Hours).Count() == 1)
            {
                throw new InvalidOperationException("Could not find the most popular time due to lack of information.");
            }

            int hour1 = _visiting
                .SelectMany(visit => visit.Value.Item1)
                .GroupBy(timeSpan => timeSpan.Hours)
                .OrderByDescending(hourGroup => hourGroup.Count())
                .First()
                .Key;
            int hour2;
            try
            {
                hour2 = _visiting
                    .SelectMany(visit => visit.Value.Item1)
                    .Where(timeSpan => Math.Abs(timeSpan.Hours - hour1) <= 1 && timeSpan.Hours != hour1)
                    .GroupBy(timeSpan => timeSpan.Hours)
                    .OrderByDescending(hour => hour.Count())
                    .First()
                    .Key;
            }
            catch (InvalidOperationException)
            {
                return (hour1, hour1);
            }
            return hour2 > hour1 ? (hour1, hour2) : (hour2, hour1);
        }

        public DayOfWeek GetMostPopularDay()
        {
            if (_visiting.Count == 0)
            {
                throw new InvalidOperationException("Could not get the most popular day due to lack of information.");
            }
            
            return _visiting
                .SelectMany(visit => visit.Value.Item2)
                .GroupBy(day => day)
                .OrderByDescending(dayGroup => dayGroup.Count())
                .First()
                .Key;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            var ipsInfo = GetMostPopularTimeForEachIP();
            foreach (var ipInfo in ipsInfo)
            {
                sb.AppendLine($"IP: {ipInfo.Key}");
                sb.AppendLine($"The most popular time: {ipInfo.Value.Item1}:00:00-{ipInfo.Value.Item2}:00:00");
                sb.AppendLine();
            }

            (int, int) hours = GetMostPopularTime();
            sb.AppendLine($"The most popular time among all users is {hours.Item1}:00:00-{hours.Item2}:00:00");
            DayOfWeek day = GetMostPopularDay();
            sb.AppendLine($"The most popular day among all users is {day}");
            return sb.ToString();
        }
    }
}
