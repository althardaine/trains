using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace trains.Utils
{
    public static class Extensions
    {
        private static readonly NumberFormatInfo DecimalSeparatorFormat = new NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberGroupSeparator = ","
        };

        public static int GetIntOrDefault(this IDictionary<string, string> dictionary, string key, int defaultValue)
        {
            return dictionary.ContainsKey(key) ? int.Parse(dictionary[key]) : defaultValue;
        }

        public static double GetDoubleOrDefault(this IDictionary<string, string> dictionary, string key, double defaultValue)
        {
            return dictionary.ContainsKey(key) ? double.Parse(dictionary[key], NumberStyles.Any, DecimalSeparatorFormat) : defaultValue;
        }

        // http://stackoverflow.com/a/1287572
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}
