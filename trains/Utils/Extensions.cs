using System.Collections.Generic;
using System.Globalization;

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
    }
}
