using System.Collections.Generic;

namespace TemperatureAnalysis
{
    public class ValidationResult
    {
        public List<TemperatureReading> ValidReadings { get; set; }
        public List<(int LineNumber, string Line)> InvalidLines { get; set; }
        public int TotalLines { get; set; }
        public int ErrorCount { get; set; }

        public ValidationResult()
        {
            ValidReadings = new List<TemperatureReading>();
            InvalidLines = new List<(int, string)>();
        }
    }
}
