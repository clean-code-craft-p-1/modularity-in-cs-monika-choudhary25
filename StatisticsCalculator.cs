using System.Collections.Generic;
using System.Linq;

namespace TemperatureAnalysis
{
    public class TemperatureStatistics
    {
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double AverageTemperature { get; set; }
    }

    public class StatisticsCalculator
    {
        public TemperatureStatistics Calculate(List<TemperatureReading> readings)
        {
            if (readings == null || readings.Count == 0)
                return null;

            var temperatures = readings.Select(r => r.Temperature).ToList();

            return new TemperatureStatistics
            {
                MaxTemperature = temperatures.Max(),
                MinTemperature = temperatures.Min(),
                AverageTemperature = temperatures.Average()
            };
        }
    }
}
