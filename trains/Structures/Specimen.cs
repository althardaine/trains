using System;
using System.Collections.Generic;
using System.Linq;

namespace trains.Structures
{
    public class Specimen
    {
        public List<int> Distribution { get; private set; }
        public int Value { get; private set; }
        public Routes Routes { get; private set; }
        public List<Line> Lines { get; private set; }
        public int NumberOfBuses { get; private set; }
        public int BusCapacity { get; private set; }
        public Random Random { get; private set; }

        public Specimen(Routes routes, List<Line> lines, int numberOfBuses, int busCapacity, Random random)
        {
            Routes = routes;
            Lines = lines;
            NumberOfBuses = numberOfBuses;
            BusCapacity = busCapacity;
            Random = random;
            SetRandomDistribution();
            CalculateSpecimentValue();
        }

        public Specimen(Specimen toClone)
        {
            Routes = toClone.Routes;
            Lines = toClone.Lines;
            NumberOfBuses = toClone.NumberOfBuses;
            BusCapacity = toClone.BusCapacity;
            Random = toClone.Random;
            CopyDistribution(toClone);
            CalculateSpecimentValue();
        }

        public Specimen(Specimen basis, List<int> distribution)
        {
            Routes = basis.Routes;
            Lines = basis.Lines;
            NumberOfBuses = basis.NumberOfBuses;
            BusCapacity = basis.BusCapacity;
            Random = basis.Random;
            if (distribution.Count != Lines.Count)
            {
                throw new ArgumentException();
            }
            Distribution = distribution;
            CalculateSpecimentValue();
        }

        private void SetRandomDistribution()
        {
            Distribution = new List<int>();
            int[] leftSpace = { Lines.Count };
            int[] sum = { 0 };

            foreach (var toAdd in Lines.Select(line => Random.Next(1, (NumberOfBuses - sum[0]) / leftSpace[0])))
            {
                Distribution.Add(toAdd);
                sum[0] += toAdd;
                leftSpace[0] -= 1;
            }

            for (var i = 0; i < Lines.Count; i++)
            {
                var toSwap = Random.Next(0, Lines.Count - 1);
                var tmp = Distribution[toSwap];
                Distribution[toSwap] = Distribution[i];
                Distribution[i] = tmp;
            }
        }

        private void CopyDistribution(Specimen toClone)
        {
            Distribution = new List<int>();
            foreach (var value in toClone.Distribution)
            {
                Distribution.Add(value);
            }
        }

        public void CalculateSpecimentValue()
        {
            var result = Routes.GetCopyOfRoutesValues();

            for (var i = 0; i < Lines.Count; i++)
            {
                var value = Distribution[i] * BusCapacity;
                var linesTrace = Lines[i].Trace;
                foreach (var pointOnTrace in linesTrace)
                {
                    var oldValue = result[pointOnTrace];
                    result[pointOnTrace] = oldValue - value;
                }
            }
            Value = result.Sum(val => Math.Abs(val));
        }
    }
}
