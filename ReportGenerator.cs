using System;
using System.IO;

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

        public void SaveReport(string filename, ValidationResult validationResult, TemperatureStatistics statistics)
        {
            string outName = filename + "_summary.txt";
            
            using (var writer = new StreamWriter(outName))
            {
                writer.WriteLine("Temperature Analysis Summary");
                writer.WriteLine(new string('=', 50));
                writer.WriteLine($"File analyzed: {filename}");
                writer.WriteLine($"Total readings: {validationResult.TotalLines}");
                writer.WriteLine($"Valid readings: {validationResult.ValidReadings.Count}");
                writer.WriteLine($"Errors: {validationResult.ErrorCount}");

                if (statistics != null)
                {
                    writer.WriteLine($"Max temperature: {statistics.MaxTemperature:F2}");
                    writer.WriteLine($"Min temperature: {statistics.MinTemperature:F2}");
                    writer.WriteLine($"Average temperature: {statistics.AverageTemperature:F2}");
                }
                
                writer.WriteLine(new string('-', 60));
                
                if (validationResult.ErrorCount > 0)
                {
                    writer.WriteLine("\nInvalid lines:");
                    foreach (var (lineNumber, line) in validationResult.InvalidLines)
                    {
                        writer.WriteLine($"  Line {lineNumber}: {line}");
                    }
                }
            }

            Console.WriteLine($"Report saved to {outName}");
        }
    }
}
