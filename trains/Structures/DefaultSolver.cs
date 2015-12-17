using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using trains.Structures.Crossovers;
using trains.Structures.Mutations;
using trains.Utils;

namespace trains.Structures
{
    public class DefaultSolver : Solver
    {
        private Solver.Config config;
        private Random random;
        private IMutation mutation;
        private ICrossover crossover;

        public DefaultSolver(Solver.Config cfg)
        {
            config = cfg;
            random = new Random();
            mutation = new Mutation(config.mutationChance);
            crossover = new Crossover(config.singleFeatureCrossoverChance, config.crossoverChance, random);
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
                                  (a, b) => mutation.Execute(crossover.Execute(a, b))));
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
