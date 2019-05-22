using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NameAnalysis
{
    public class StringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Compare(x, y, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

        public int GetHashCode(string obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
    static class NameAnalyzer
    {
        public static IEnumerable<Tuple<string, int>> GetTopByColumn(List<PersonName> personNameList, ColumnsEnum column, int iRank, bool isDescending)
        {
            Func<PersonName, string> getPersonProperty = GetFunctionStrategy(column);
            StringComparer comparer = new StringComparer();

            var groups = personNameList.GroupBy(getPersonProperty, new StringComparer());
            groups = isDescending ? groups.OrderByDescending(g => g.Count()) : groups.OrderBy(g => g.Count());

            return groups.Take(iRank).Select(g => new Tuple<string, int>(g.Key, g.Count())).ToList();
        }

        private static void GetWordCount(string text, Dictionary<string, int> wordCountDictionary)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string[] words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    wordCountDictionary[word.Trim()] = wordCountDictionary.GetValueOrDefault(word.Trim()) + 1;
                }
            }
        }

        private static Func<PersonName, string> GetFunctionStrategy(ColumnsEnum column)
        {
            Func<PersonName, string> getPersonProperty = (x) => x.FirstName;
            switch (column)
            {
                case ColumnsEnum.FirstLastName:
                    getPersonProperty = (x) => x.FirstLastName;
                    break;
                case ColumnsEnum.SecondLastName:
                    getPersonProperty = (x) => x.SecondLastName;
                    break;
            }

            return getPersonProperty;
        }

        public static IEnumerable<Tuple<string, int>> GetTopWordsByColumn(List<PersonName> personNameList,
            ColumnsEnum column, int iRank, bool isDescending)
        {
            Func<PersonName, string> getPersonProperty = GetFunctionStrategy(column);
            Dictionary<string, int> wordCountDictionary = new Dictionary<string, int>();

            personNameList.ForEach(personName => { GetWordCount(getPersonProperty(personName), wordCountDictionary); });
            var wordCounts = wordCountDictionary.Select(e => new Tuple<string, int>(e.Key, e.Value));
            wordCounts = isDescending ? wordCounts.OrderByDescending(t => t.Item2) : wordCounts.OrderBy(t => t.Item2);

            return wordCounts.Take(iRank);
        }
    }
}
