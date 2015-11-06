using System.Collections.Generic;
using System.Linq;

namespace trains.Structures
{
    public class Routes
    {
        public List<int> RoutesValues { get; private set; }

        public Routes(string inputRoutesString)
        {
            RoutesValues = new List<int>();
            var splitted = inputRoutesString.Split(',');
            foreach (var values in splitted.Select(rout => rout.Split(':')))
            {
                RoutesValues.Add(int.Parse(values[1]));
            }
        }

        public List<int> GetCopyOfRoutesValues()
        {
            return RoutesValues.ToList();
        }
    }
}
