using System;
using System.Collections.Generic;
using System.IO;

namespace NameAnalysis
{

    class Program
    {
        const int COLUMNNUMBER = 12;
        const char SEPARATOR = '|';
        static void Main(string[] args)
        {

           
            string pathToFile = PathUtils.GetPathToFile();
            Console.WriteLine("------------WHOLE WORD STATISTICS");
            List<PersonName> personNames = NameExtractor.GetPersonNameList(pathToFile, SEPARATOR, COLUMNNUMBER);
            PrintTopByField(personNames, ColumnsEnum.Name, 25, true);
            PrintTopByField(personNames, ColumnsEnum.FirstLastName, 25, true);
            PrintTopByField(personNames, ColumnsEnum.SecondLastName, 25, true);

            Console.WriteLine("------------SPLIT WORD STATISTICS");
            Console.ReadLine();

            PrintTopByField(personNames, ColumnsEnum.Name, 25, true, false);
            PrintTopByField(personNames, ColumnsEnum.FirstLastName, 25, true, false);
            PrintTopByField(personNames, ColumnsEnum.SecondLastName, 25, true, false);
            Console.ReadLine();
        }

        static void PrintTopByField(List<PersonName> personNames, ColumnsEnum column, int iRank, bool isDescending, bool useWholeWord = true)
        {
            IEnumerable<Tuple<string, int>> topNames = useWholeWord ? NameAnalyzer.GetTopByColumn(personNames, column, iRank, isDescending)
                : NameAnalyzer.GetTopWordsByColumn(personNames, column, iRank, isDescending);

            Console.WriteLine($"------------------{(isDescending ? "TOP" : "LAST") } {iRank } BY {((object)column).ToString()} ");
            if (topNames != null)
            {
                foreach (var topName in topNames)
                {
                    Console.WriteLine($"{topName.Item1}: {topName.Item2}");
                }
            }
        }
    }
}
