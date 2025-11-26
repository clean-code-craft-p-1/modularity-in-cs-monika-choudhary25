namespace TemperatureAnalysis
{
    public class TemperatureReading
    {
        public string Timestamp { get; set; }
        public double Temperature { get; set; }

        public TemperatureReading(string timestamp, double temperature)
        {
            Timestamp = timestamp;
            Temperature = temperature;
        }
    }
}
