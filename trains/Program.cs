using System;
using System.Collections.Generic;
using trains.Structures;
using trains.Structures.Crossovers;
using trains.Structures.Mutations;

namespace trains
{
    class Program
    {
        static void Main(string[] args)
        {
            var routs = new Routes("0:10,1:40,2:20,3:10,4:40,5:20,6:60,7:50,8:10,9:20,10:10");
            var lines = new List<Line>
            {
                new Line("0:0,1,2,7,8,9,10"),
                new Line("1:4,6,8,9,10"),
                new Line("2:3,7,6,5"),
                new Line("3:0,1,4,5")
            };

            const int numberOfBuses = 10;
            const int busCapacity = 5;
            const int numberOfIterations = 40;
            const int poolOfSpecimens = 20;

            const int mutationChance = 2;
            const int crossoverChance = 100;

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

            solution.Execute();

            Console.WriteLine("Value: " + solution.BestResult.Value);
            Console.WriteLine("Solution:");
            solution.BestResult.Distribution.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
