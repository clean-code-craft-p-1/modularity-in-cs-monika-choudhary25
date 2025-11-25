using System;
using System.IO;

namespace TemperatureAnalysis
{
    class Program
    {
        static void ProcessBatch(string filename)
        {
            try
            {
                var fileReader = new FileReader();
                var validator = new DataValidator();
                var statsCalculator = new StatisticsCalculator();
                var reportGenerator = new ReportGenerator();

                // Read file
                string[] lines = fileReader.ReadLines(filename);

                // Validate data
                var validationResult = validator.ValidateData(lines);

                if (validationResult.ValidReadings.Count == 0)
                {
                    Console.WriteLine("No valid temperature data found.");
                    return;
                }

                // Calculate statistics
                var statistics = statsCalculator.Calculate(validationResult.ValidReadings);

                // Generate reports
                reportGenerator.PrintSummary(validationResult, statistics);
                reportGenerator.SaveReport(filename, validationResult, statistics);
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
            // Generate test data file
            string testFilename = "test_temps.csv";
            string[] testData = {
                "09:15:30,23.5",
                "09:16:00,24.1",
                "09:16:30,22.8",
                "09:17:00,25.3",
                "09:17:30,23.9",
                "09:18:00,24.7",
                "09:18:30,22.4",
                "09:19:00,26.1",
                "09:19:30,23.2",
                "09:20:00,25.0"
            };

            File.WriteAllLines(testFilename, testData);
            Console.WriteLine($"Created test file: {testFilename}");

            // Process the test file
            ProcessBatch(testFilename);

            // Verify the summary file was created
            string summaryFile = testFilename + "_summary.txt";
            if (File.Exists(summaryFile))
            {
                Console.WriteLine($"\nSummary file created: {summaryFile}");
                string content = File.ReadAllText(summaryFile);
                
                if (content.Contains("Total readings: 10") && 
                    content.Contains("Valid readings: 10") && 
                    content.Contains("Errors: 0"))
                {
                    Console.WriteLine("✓ Summary file contents verified");
                }
                else
                {
                    Console.WriteLine("✗ Summary file verification failed");
                }
            }

            // Clean up test files
            try
            {
                if (File.Exists(testFilename))
                    File.Delete(testFilename);
                if (File.Exists(summaryFile))
                    File.Delete(summaryFile);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Warning: Could not clean up files: {e.Message}");
            }
        }
    }
}