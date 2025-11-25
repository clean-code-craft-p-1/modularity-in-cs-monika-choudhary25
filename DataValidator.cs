using System.Globalization;

namespace TemperatureAnalysis
{
    public class DataValidator
    {
        private const double MinValidTemperature = -100;
        private const double MaxValidTemperature = 200;

        public ValidationResult ValidateData(string[] lines)
        {
            var result = new ValidationResult
            {
                TotalLines = lines.Length
            };

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                    continue;

                if (!TryParseLine(line, out string timestamp, out double temperature))
                {
                    result.ErrorCount++;
                    result.InvalidLines.Add((i + 1, line));
                    continue;
                }

                result.ValidReadings.Add(new TemperatureReading(timestamp, temperature));
            }

            return result;
        }

        private bool TryParseLine(string line, out string timestamp, out double temperature)
        {
            timestamp = string.Empty;
            temperature = 0;

            string[] parts = line.Split(',');
            if (parts.Length != 2)
                return false;

            timestamp = parts[0].Trim();
            string valueStr = parts[1].Trim();

            // Validate timestamp (expecting HH:MM:SS format)
            if (timestamp.Split(':').Length != 3)
                return false;

            // Try to parse temperature value
            if (!double.TryParse(valueStr, NumberStyles.Float, CultureInfo.InvariantCulture, out temperature))
                return false;

            // Drop impossible temperatures
            if (temperature < MinValidTemperature || temperature > MaxValidTemperature)
                return false;

            return true;
        }
    }
}
