using System.Collections.Generic;
using System.Linq;

namespace trains.Structures
{
    public class Line
    {
        public List<int> Trace { get; private set; }

        public int LineId { get; private set; }

        public Line(string lineInputString)
        {
            Trace = new List<int>();
            var splitted = lineInputString.Split(':');

            LineId = int.Parse(splitted[0]);

            splitted[1].Split(',').ToList().ForEach(pointOnTrace => Trace.Add(int.Parse(pointOnTrace)));
        }
    }
}

