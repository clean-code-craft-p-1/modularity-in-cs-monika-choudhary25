using System;
using System.IO;

namespace TemperatureAnalysis
{
    class TemperatureAnalysisDemo
    {
        static void ProcessBatch(string filename)
        {
            try
            {
                var fileReader = new FileReader();
                var validator = new DataValidator();
                var statsCalculator = new StatisticsCalculator();
                var reportGenerator = new ReportGenerator();
                var reportSaver = new ReportSaver();

                string[] lines = fileReader.ReadLines(filename);
                var validationResult = validator.ValidateData(lines);

                if (validationResult.ValidReadings.Count == 0)
                {
                    Console.WriteLine("No valid temperature data found.");
                    return;
                }

                var statistics = statsCalculator.Calculate(validationResult.ValidReadings);
                reportGenerator.PrintSummary(validationResult, statistics);
                reportSaver.SaveReport(filename, validationResult, statistics);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: File not found.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing file: {e.Message}");
            }
        }

        static void Main(string[] args)
        {
            var testDataGenerator = new TestDataGenerator();
            string testFilename = testDataGenerator.GenerateTestFile();
            ProcessBatch(testFilename);
            testDataGenerator.VerifyAndCleanup(testFilename);
        }
    }
}