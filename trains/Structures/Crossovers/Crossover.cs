using System;
using System.Collections.Generic;

namespace trains.Structures.Crossovers
{
    class Crossover : ICrossover
    {

        private readonly double _chance;
        private readonly int _probability;
        private readonly Random _random;

        public Crossover(int singleFeatureProbability, int probability, Random random)
        {
            _chance = singleFeatureProbability;
            _random = random;
            _probability = probability;
        }

        public Specimen Execute(Specimen specimen1, Specimen specimen2)
        {
            if (_random.Next(0, 100) >= _probability)
            {
                return specimen1.Value < specimen2.Value ? specimen1.Clone() : specimen2.Clone();
            }

            var distribution = new List<int>();
            for (var i = 0; i < specimen1.Lines.Count; i++)
            {
                distribution.Add(specimen1.Distribution[i] <= specimen2.Distribution[i] && _random.Next(0, 100) < _chance
                    ? specimen1.Distribution[i]
                    : specimen2.Distribution[i]);
            }
            return new Specimen(specimen1, distribution);
        }
    }
}
