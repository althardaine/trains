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
            Problem problem = Problem.LoadFromFile("../../../input/input_lolwut.txt");
            Solver.Config solverConfig = Solver.Config.LoadFromFile("../../../input/solver.cfg");
            Solver solver = new Solver(solverConfig);

            for (var i = 0; i < 10; i++)
            {
                Solution solution = solver.Solve(problem);
                Console.WriteLine("Value: " + solution.Value);
            }

            //Console.WriteLine("Solution:");
            //solution.BestResult.Distribution.ForEach(Console.WriteLine);

            Console.WriteLine("finished");
            Console.ReadLine();
        }
    }
}
