using System.IO;
using System.Linq;
using trains.Utils;

namespace trains.Structures
{
    public abstract class Solver
    {
        public struct Config
        {
            public int numberOfIterations;
            public int specimenPoolSize;
            public double mutationChance;
            public double singleFeatureCrossoverChance;
            public double crossoverChance;

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
                    mutationChance = cfgDict.GetDoubleOrDefault("mutationChance", 0.1),
                    singleFeatureCrossoverChance = cfgDict.GetDoubleOrDefault("singleFeatureCrossoverChance", 0.1),
                    crossoverChance = cfgDict.GetDoubleOrDefault("crossoverChance", 0.1)
                };
            }

            public override string ToString()
            {
                var self = this;
                return string.Join("\n", GetType().GetFields().Select(f => string.Format("{0} = {1}", f.Name, f.GetValue(self))));
            }
        }

        public abstract Solution Solve(Problem problem);
    }
}