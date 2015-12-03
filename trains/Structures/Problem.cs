using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace trains.Structures
{
    public class Problem
    {
        public List<int> PeoplePerSegments { get; private set; }
        public List<Line> Lines { get; private set; }
        public int NumberOfBuses { get; private set; }
        public int BusCapacity { get; private set; }

        public static Problem LoadFromFile(string path)
        {
            try {
                if (!File.Exists(path))
                    throw new FileNotFoundException(string.Format("{0} does not exists.", path));

                var textLines = File.ReadAllLines(path)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s) && s[0] != '#');

                if (textLines.Count() < 2)
                    throw new FormatException();

                var segments = textLines.ElementAt(0).Split(',').Select(x => int.Parse(x)).ToList();
                var numLines = int.Parse(textLines.ElementAt(1));
                var lines = textLines.Skip(2).Take(numLines).Select(s => new Line(s)).ToList();
                var config = textLines.Skip(2 + numLines)
                    .Select(s => s.Split('='))
                    .ToDictionary(t => t[0], t => t[1]);

                return new Problem()
                {
                    PeoplePerSegments = segments,
                    Lines = lines,
                    NumberOfBuses = int.Parse(config["numberOfBuses"]),
                    BusCapacity = int.Parse(config["busCapacity"])
                };
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                throw new FormatException(string.Format("Could not load problem from file {0}", path), e);
            }
        }
    }

}
