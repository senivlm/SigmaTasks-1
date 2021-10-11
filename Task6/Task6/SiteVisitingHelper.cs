using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class SiteVisitingHelper
    {
        private SiteVisitingHelper() {}

        public static void FillFileWithRandomIPs(string filePath, int number = 1000, int repeatIPs = 1)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Number of records must be greater than 0.", nameof(number));
            }

            if (repeatIPs < 1)
            {
                throw new ArgumentException("Number of repeats must be greater than 0.");
            }
            Random random = new();
            using StreamWriter writer = new(filePath);
            for (int i = 0; i < number; ++i)
            {
                List<int> numbers = new();
                for (int j = 0; j < 4; ++j)
                {
                    numbers.Add(random.Next(0, 256));
                }

                string ip = string.Join(".", numbers);
                for (int j = 0; j < repeatIPs; ++j)
                {
                    int hour = random.Next(0, 24);
                    int minute = random.Next(0, 60);
                    int second = random.Next(0, 60);
                    TimeSpan timeSpan = new TimeSpan(hour, minute, second);
                    DayOfWeek day = (DayOfWeek)random.Next(0, 7);
                    writer.WriteLine($"{ip} {timeSpan} {day}");
                }
            }
        }
    }
}
