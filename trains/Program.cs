using System;
using System.Diagnostics;
using trains.Structures;

namespace trains
{
    class Program
    {
        static void Main(string[] args)
        {
            Problem problem = Problem.LoadFromFile("../../../input/input_lolwut.txt");
            Solver.Config solverConfig = Solver.Config.LoadFromFile("../../../input/solver.cfg");
            Console.WriteLine("using config:\n" + solverConfig.ToString());
            Solver solver = new Solver(solverConfig);

            for (var i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                Solution solution = solver.Solve(problem);

                watch.Stop();
                Console.WriteLine(string.Format("Value: {0} found in {1}s",
                                                solution.Value,
                                                (float)watch.ElapsedMilliseconds / 1000.0));

                //Console.WriteLine("Solution:");
                //solution.Distribution.ForEach(Console.WriteLine);
            }

            Console.WriteLine("finished");
            Console.ReadLine();
        }
    }
}
