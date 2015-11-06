using System;
using System.Collections.Generic;
using System.Linq;
using trains.Structures.Crossovers;
using trains.Structures.Mutations;

namespace trains.Structures
{
    internal class Solution
    {
        private readonly Random _random;
        private List<Specimen> _specimens = new List<Specimen>();

        public Solution(Routes routes, List<Line> lines, IMutation mutationType, ICrossover crossoverType,
            int numberOfBusses, int busCapacity, int numberOfIteration, int poolOfSpeciemens, Random random)
        {
            Routes = routes;
            Lines = lines;
            Mutation = mutationType;
            Crossover = crossoverType;
            NumberOfBuses = numberOfBusses;
            BusCapacity = busCapacity;
            PoolOfSpeciemens = poolOfSpeciemens;
            NumberOfIterations = numberOfIteration;
            _random = random;
        }

        public Specimen BestResult { get; private set; }
        public int PoolOfSpeciemens { get; private set; }
        public int NumberOfIterations { get; private set; }
        public Routes Routes { get; private set; }
        public List<Line> Lines { get; private set; }
        public int NumberOfBuses { get; private set; }
        public int BusCapacity { get; private set; }
        public IMutation Mutation { get; private set; }
        public ICrossover Crossover { get; private set; }

        public void Execute()
        {
            GenerateRandomPopulation();
            for (var i = 0; i < NumberOfIterations; i++)
            {
                _specimens = _specimens.OrderBy(o => o.Value).ToList();
                var nextPopulation = _specimens.Take(PoolOfSpeciemens/4).ToList();
                for (var j = 0; j < PoolOfSpeciemens/4; j++)
                {
                    nextPopulation.Add(new Specimen(Routes, Lines, NumberOfBuses, BusCapacity, _random));
                }
                for (var j = 0; j < PoolOfSpeciemens/2; j += 2)
                {
                    nextPopulation.Add(Mutation.Execute(Crossover.Execute(_specimens[j], _specimens[j + 1])));
                }
                _specimens = nextPopulation;
                FindBest();
            }
        }

        private void GenerateRandomPopulation()
        {
            for (var i = 0; i < PoolOfSpeciemens; i++)
            {
                _specimens.Add(new Specimen(Routes, Lines, NumberOfBuses, BusCapacity, _random));
            }
            BestResult = new Specimen(_specimens[0]);
            FindBest();
        }

        private void FindBest()
        {
            foreach (var speciment in _specimens.Where(speciment => speciment.Value < BestResult.Value))
            {
                BestResult = new Specimen(speciment);
            }
        }
    }
}
