using System;
using trains.Structures;
using trains.Structures.Crossovers;
using trains.Structures.Mutations;
using trains.Utils;

namespace trains
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new InputDataLoader();
            
            var input = loader.LoadInput("../../../input/input_lolwut.txt");

            var routs = input.Item1;
            var lines = input.Item2;

            var c = input.Item3;

            var numberOfBuses = c.GetIntOrDefault("numberOfBuses", 200);
            var busCapacity = c.GetIntOrDefault("busCapacity", 5);
            var numberOfIterations = c.GetIntOrDefault("numberOfIterations", 400);
            var poolOfSpecimens = c.GetIntOrDefault("poolOfSpecimens", 20);

            var mutationChance = c.GetIntOrDefault("mutationChance", 20);
            var crossoverChance = c.GetIntOrDefault("crossoverChance", 10);

            var random = new Random();

            var mutationType = new Mutation(mutationChance);
            var crossoverType = new Crossover(10, crossoverChance, random);

            var solution = new Solution(
                routs, lines,
                mutationType,
                crossoverType,
                numberOfBuses,
                busCapacity,
                numberOfIterations,
                poolOfSpecimens,
                random);

            solution.Calculate();

            Console.WriteLine("Value: " + solution.BestResult.Value);
            Console.WriteLine("Solution:");
            solution.BestResult.Distribution.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
