using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using trains.Structures;

namespace trains.Utils
{
    class InputDataLoader
    {
        public Tuple<Routes, List<Line>, IDictionary<string, string>> LoadInput(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(string.Format("{0} does not exists.", path));

            var data = File.ReadLines(path).Where(s => s != "");

            if (data.Count() < 2)
                throw new FormatException();

            var routes = new Routes(data.ElementAt(0));

            var linesCount = int.Parse(data.ElementAt(1));

            var lines = data.Skip(2).Take(linesCount).Select(s => new Line(s));

            var parameters = data.Skip(2).Skip(linesCount)
                .Select(s => s.Split('='))
                .ToDictionary(a => a[0], a => a[1]);

            return new Tuple<Routes, List<Line>, IDictionary<string, string>>(
                routes, lines.ToList(), parameters);
        }
    }
}
