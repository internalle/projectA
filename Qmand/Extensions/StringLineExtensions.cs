using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Extensions
{
    internal static class StringLineExtensions
    {
        public static string GetFirst(this string line, char split = ' ')
        {
            return line.Split(split).FirstOrDefault();
        }

        public static string GetRest(this string line, char split = ' ')
        {
            return string.Join(split.ToString(), line.Split(split).Skip(1));
        }

        public static Dictionary<string, string> GetCommandParameters(this string line)
        {
            var pairs = line
                .Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(x => new KeyValuePair<string, string>(x.Split(' ').FirstOrDefault(), string.Join(" ", x.Split(' ').Skip(1))));

            var dictionary = new Dictionary<string, string>();

            dictionary.Add(pairs);

            return dictionary;
        }

        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            foreach (var pair in list)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
        }
    }
}
