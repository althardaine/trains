using System;
using System.Collections.Generic;
using System.Linq;

namespace trains.Structures
{
    public class Solution
    {
        public List<int> Distribution { get; protected set; }
        public int Value { get; protected set; }

        protected Solution()
        {
        }

        internal Solution(List<int> distribution,
                          int value)
        {
            Distribution = distribution;
            Value = value;
        }
    }

    class Specimen: Solution
    {
        public readonly Problem Problem;
        public Random Random { get; private set; }

        public Specimen(Problem problem, Random random)
        {
            Problem = problem;
            Random = random;
            SetRandomDistribution();
            CalculateSpecimentValue();
        }

        public Specimen(Specimen basis, List<int> distribution)
        {
            Problem = basis.Problem;
            Random = basis.Random;
            if (distribution.Count != Problem.Lines.Count)
            {
                throw new ArgumentException();
            }
            Distribution = distribution;
            CalculateSpecimentValue();
        }

        private void SetRandomDistribution()
        {
            Distribution = Enumerable.Repeat(0, Problem.NumberOfBuses).ToList();

            int busesToDeploy = Random.Next(0, Problem.NumberOfBuses);
            for (int _ = 0; _ < busesToDeploy; ++_)
            {
                ++Distribution[Random.Next(Distribution.Count)];
            }
        }

        public void CalculateSpecimentValue()
        {
            var result = Problem.PeoplePerSegments.ToList();

            for (var i = 0; i < Problem.Lines.Count; i++)
            {
                var value = Distribution[i] * Problem.BusCapacity;
                var linesTrace = Problem.Lines[i].Trace;
                foreach (var pointOnTrace in linesTrace)
                {
                    var oldValue = result[pointOnTrace];
                    result[pointOnTrace] = oldValue - value;
                }
            }
            Value = result.Sum(val => Math.Abs(val));
        }

        public Specimen Clone()
        {
            return new Specimen(Problem, Random);
        }
    }
}
