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
    public class Solver
    {
        public struct Config
        {
            public int numberOfIterations;
            public int specimenPoolSize;
            public int mutationChance;
            public int singleFeatureCrossoverChance;
            public int crossoverChance;

            public static Config LoadFromFile(string path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException(string.Format("{0} does not exists.", path));

                var textLines = File.ReadAllLines(path)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s) && s[0] != '#');

                var cfgDict = textLines.Select(s => s.Split('='))
                    .ToDictionary(t => t[0], t => t[1]);

                return new Config()
                {
                    numberOfIterations = cfgDict.GetIntOrDefault("numberOfIterations", 100),
                    specimenPoolSize = cfgDict.GetIntOrDefault("specimenPoolSize", 100),
                    mutationChance = cfgDict.GetIntOrDefault("mutationChance", 10),
                    singleFeatureCrossoverChance = cfgDict.GetIntOrDefault("singleFeatureCrossoverChance", 10),
                    crossoverChance = cfgDict.GetIntOrDefault("crossoverChance", 10)
                };
            }
        }

        private Config config;
        private Random random;
        private IMutation mutation;
        private ICrossover crossover;

        public Solver(Config cfg)
        {
            config = cfg;
            random = new Random();
            mutation = new Mutation(config.mutationChance);
            crossover = new Crossover(config.singleFeatureCrossoverChance, config.crossoverChance, random);
        }

        public Solution Solve(Problem problem)
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
                    specimens.OrderBy(s => Guid.NewGuid())
                             .Take(specimens.Count / 2)
                             .AsQueryable()
                             .Zip(specimens,
                                  (a, b) => mutation.Execute(crossover.Execute(a, b))));
                //                    nextPopulation.Add(Mutation.Execute(Crossover.Execute(_specimens[j], _specimens[j + 1])));
                //                }
                specimens = nextPopulation;
                bestSpecimen = FindBestSpecimen(specimens).Clone();
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
