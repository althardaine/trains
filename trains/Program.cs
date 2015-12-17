using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using trains.Structures;

namespace trains
{
    class Program
    {
        struct Result
        {
            public struct RunResult
            {
                public int Result { get; private set; }
                public double SecondsElapsed { get; private set; }

                public RunResult(int result,
                                 double secondsElapsed)
                {
                    Result = result;
                    SecondsElapsed = secondsElapsed;
                }
            }

            public readonly List<RunResult> Results;

            public int MinResult { get; private set; }
            public int MaxResult { get; private set; }
            public double AvgResult { get; private set; }

            public double MinSeconds { get; private set; }
            public double MaxSeconds { get; private set; }
            public double AvgSeconds { get; private set; }

            public double TotalSeconds { get; private set; }

            public Result(List<RunResult> results) : this()
            {
                Results = results;

                MinResult = results.Select(_ => _.Result).Min();
                MaxResult = results.Select(_ => _.Result).Max();
                AvgResult = results.Select(_ => _.Result).Average();

                MinSeconds = results.Select(_ => _.SecondsElapsed).Min();
                MaxSeconds = results.Select(_ => _.SecondsElapsed).Max();
                AvgSeconds = results.Select(_ => _.SecondsElapsed).Average();

                TotalSeconds = results.Select(_ => _.SecondsElapsed).Sum();
            }

            public override string ToString()
            {
                return string.Format("{0} iterations done in {1}s, {2}-{3}s (avg {4}s); result: {5}-{6} (avg: {7})",
                    Results.Count, TotalSeconds, MinSeconds, MaxSeconds, AvgSeconds, MinResult, MaxResult, AvgResult);
            }
        }
        static Result Benchmark(Solver solver,
                                Problem problem,
                                int numIterations)
        {
            List<Result.RunResult> results = new List<Result.RunResult>();

            for (int i = 0; i < numIterations; ++i)
            {
                Console.Error.Write(string.Format("{0}/{1}", i + 1, numIterations));

                Stopwatch watch = new Stopwatch();
                watch.Start();
                Solution solution = solver.Solve(problem);
                watch.Stop();

                Console.Error.WriteLine(string.Format(" {0}s, {1}", watch.Elapsed.TotalSeconds, solution.Value));

                Result.RunResult result = new Result.RunResult(solution.Value, watch.Elapsed.TotalSeconds);
                results.Add(result);
            }

            return new Result(results);
        }

        static void Main(string[] args)
        {
            Problem problem = Problem.LoadFromFile("../../../input/input_lolwut.txt");
            Solver.Config solverConfig = Solver.Config.LoadFromFile("../../../input/solver.cfg");
            Console.WriteLine("using config:\n" + solverConfig.ToString());
            Solver solver = new Solver(solverConfig);

            const int BENCHMARK_ITERATIONS = 10;

            Result result = Benchmark(solver, problem, BENCHMARK_ITERATIONS);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
