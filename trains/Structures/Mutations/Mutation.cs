using System;
using System.Linq;

namespace trains.Structures.Mutations
{
    internal class Mutation : IMutation
    {
        private readonly double _chance;

        public Mutation(double chance)
        {
            if (chance < 0 || chance > 1)
            {
                throw new ArgumentException();
            }
            _chance = chance;
        }

        public Specimen Execute(Specimen specimen)
        {
            var toBeChanged =
                specimen.Problem.Lines.Select(line => specimen.Random.NextDouble())
                    .Select(rolled => rolled < _chance ? 1 : 0)
                    .ToList();

            for (var i = 0; i < specimen.Problem.Lines.Count; i++)
            {
                if (toBeChanged[i] != 1) continue;
                var busses = specimen.Distribution.Sum();
                var max = specimen.Problem.NumberOfBuses - busses;
                var min = -(specimen.Distribution[i] - 1);
                if (min < max)
                {
                    specimen.Distribution[i] += specimen.Random.Next(min, max);
                }
            }
            specimen.CalculateSpecimentValue();
            return specimen;
        }
    }
}
