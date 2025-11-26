using System;
using System.IO;

namespace TemperatureAnalysis
{
    public class FileReader
    {
        public string[] ReadLines(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("File not found.", filename);
            }

            return File.ReadAllLines(filename);
        }
    }
}
