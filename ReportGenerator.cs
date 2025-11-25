using System;

namespace TemperatureAnalysis
{
    public class ReportGenerator
    {
        public void PrintSummary(ValidationResult validationResult, TemperatureStatistics statistics)
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("Temperature Analysis Summary");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"Total readings: {validationResult.TotalLines}");
            Console.WriteLine($"Valid readings: {validationResult.ValidReadings.Count}");
            Console.WriteLine($"Errors: {validationResult.ErrorCount}");
            Console.WriteLine(new string('-', 60));

            if (statistics != null)
            {
                Console.WriteLine($"Max temperature: {statistics.MaxTemperature:F2}");
                Console.WriteLine($"Min temperature: {statistics.MinTemperature:F2}");
                Console.WriteLine($"Average temperature: {statistics.AverageTemperature:F2}");
                Console.WriteLine(new string('-', 60));
            }

            if (validationResult.ErrorCount > 0)
            {
                Console.WriteLine("Invalid lines:");
                foreach (var (lineNumber, line) in validationResult.InvalidLines)
                {
                    Console.WriteLine($"  Line {lineNumber}: {line}");
                }
            }
        }
    }
}
