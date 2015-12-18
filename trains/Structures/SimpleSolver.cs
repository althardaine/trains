using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using trains.Structures.Crossovers;
using trains.Structures.Mutations;
using trains.Utils;

namespace trains.Structures
{
    public class SimpleSolver : Solver
    {
        private Solver.Config config;
        private Random random;

        public SimpleSolver(Solver.Config cfg)
        {
            config = cfg;
            random = new Random();
        }

        private Specimen Crossover(Specimen a,
                                   Specimen b)
        {
            List<int> distribution = new List<int>();
            int totalBuses = 0;
            for (int line = 0; line < a.Distribution.Count; ++line)
            {
                int busesPerLine;
                if (random.Next(2) == 0)
                    busesPerLine = a.Distribution[line];
                else
                    busesPerLine = b.Distribution[line];

                distribution.Add(busesPerLine);
                totalBuses += busesPerLine;
            }

            while (totalBuses > a.Problem.NumberOfBuses)
            {
                int line = random.Next(distribution.Count);
                if (distribution[line] > 0)
                {
                    --distribution[line];
                    --totalBuses;
                }
            }

            return new Specimen(a, distribution);
        }

        private Specimen Mutate(Specimen specimen)
        {
            Specimen result = specimen.Clone();

            int to;
            int from = specimen.Distribution
                .Select((x, idx) => new Tuple<int, int>(idx, x))
                .Where(_ => _.Item2 != 0)
                .Shuffle(random)
                .Select(_ => _.Item1)
                .Concat(-1)
                .First();

            do
            {
                to = random.Next(specimen.Distribution.Count);
            } while (to == from);

            int severity;
            if (from >= 0)
            {
                severity = random.Next(specimen.Distribution[from]);
                result.Distribution[from] -= severity;
            }
            else
            {
                severity = random.Next(specimen.Problem.NumberOfBuses);
            }
            result.Distribution[to] += severity;

            return result;
        }

        public override Solution Solve(Problem problem)
        {
            var specimens = GenerateRandomPopulation(problem);
            var bestSpecimen = FindBestSpecimen(specimens).Clone();

            for (var i = 0; i < config.numberOfIterations; i++)
            {
                specimens = specimens.OrderBy(o => o.Value).ToList();
                var nextPopulation = specimens.Take(config.specimenPoolSize / 4).ToList();
                for (var j = 0; j < config.specimenPoolSize / 4; j++)
                {
                    nextPopulation.Add(new Specimen(problem, random));
                }

                nextPopulation.AddRange(
                    specimens.Shuffle(random)
                        .Take(specimens.Count / 2)
                        .AsQueryable()
                        .Zip(specimens,
                             (a, b) => random.NextDouble() < config.mutationChance ? Mutate(Crossover(a, b)) : Crossover(a, b)));
                //                    nextPopulation.Add(Mutation.Execute(Crossover.Execute(_specimens[j], _specimens[j + 1])));
                //                }
                specimens = nextPopulation;
                bestSpecimen = FindBestSpecimen(specimens).Clone();

                //Console.Error.WriteLine(string.Format("  {0}", bestSpecimen.Value));
            }

            return bestSpecimen;
        }

        private List<Specimen> GenerateRandomPopulation(Problem problem)
        {
            return Enumerable.Range(0, config.specimenPoolSize)
                .Select(_ => new Specimen(problem, random))
                .ToList();
        }

        private Specimen FindBestSpecimen(ICollection<Specimen> specimens)
        {
            return specimens.MinBy(s => s.Value);
        }
    }
}