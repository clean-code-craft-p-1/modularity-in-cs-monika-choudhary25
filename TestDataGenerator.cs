using System;
using System.IO;

namespace TemperatureAnalysis
{
    public class TestDataGenerator
    {
        public string GenerateTestFile()
        {
            string testFilename = "test_temps.csv";
            string[] testData = {
                "09:15:30,23.5", "09:16:00,24.1", "09:16:30,22.8", "09:17:00,25.3",
                "09:17:30,23.9", "09:18:00,24.7", "09:18:30,22.4", "09:19:00,26.1",
                "09:19:30,23.2", "09:20:00,25.0"
            };

            File.WriteAllLines(testFilename, testData);
            Console.WriteLine($"Created test file: {testFilename}");
            return testFilename;
        }

        public void VerifyAndCleanup(string testFilename)
        {
            string summaryFile = testFilename + "_summary.txt";
            if (File.Exists(summaryFile))
            {
                Console.WriteLine($"\nSummary file created: {summaryFile}");
                string content = File.ReadAllText(summaryFile);
                
                bool isValid = content.Contains("Total readings: 10") && 
                               content.Contains("Valid readings: 10") && 
                               content.Contains("Errors: 0");
                
                Console.WriteLine(isValid ? "✓ Summary file contents verified" : "✗ Summary file verification failed");
            }

            try
            {
                if (File.Exists(testFilename)) File.Delete(testFilename);
                if (File.Exists(summaryFile)) File.Delete(summaryFile);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Warning: Could not clean up files: {e.Message}");
            }
        }
    }
}
