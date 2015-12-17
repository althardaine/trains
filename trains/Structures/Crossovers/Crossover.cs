using System;
using System.Collections.Generic;

namespace trains.Structures.Crossovers
{
    class Crossover : ICrossover
    {

        private readonly double _chance;
        private readonly double _probability;
        private readonly Random _random;

        public Crossover(double singleFeatureProbability, double probability, Random random)
        {
            _chance = singleFeatureProbability;
            _random = random;
            _probability = probability;
        }

        public Specimen Execute(Specimen specimen1, Specimen specimen2)
        {
            if (_random.NextDouble() >= _probability)
            {
                return specimen1.Value < specimen2.Value ? specimen1.Clone() : specimen2.Clone();
            }

            var distribution = new List<int>();
            for (var i = 0; i < specimen1.Problem.Lines.Count; i++)
            {
                distribution.Add(specimen1.Distribution[i] <= specimen2.Distribution[i] && _random.NextDouble() < _chance
                    ? specimen1.Distribution[i]
                    : specimen2.Distribution[i]);
            }
            return new Specimen(specimen1, distribution);
        }
    }
}
