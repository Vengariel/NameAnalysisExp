using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace NameAnalysis
{
    public static class NameExtractor
    {
        public static List<PersonName> GetPersonNameList(string csvPath, char cSeparator, int iColumnNumber, bool useUniqueCURP =false)
        {
            List<PersonName> personNameList = null;

            if (File.Exists(csvPath))
            {
                personNameList = new List<PersonName>();

                using (StreamReader sr = new StreamReader(csvPath))
                {
                    if (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                    }

                    while (!sr.EndOfStream)
                    {
                        string row = sr.ReadLine();
                        string[] sWords;
                        if (!string.IsNullOrEmpty(row))
                        {
                            sWords = row.Split(cSeparator);
                            if (sWords.Length == iColumnNumber)
                            {
                                PersonName person = new PersonName()
                                {
                                    FirstName = sWords[(int)ColumnsEnum.Name],
                                    FirstLastName = sWords[(int)ColumnsEnum.FirstLastName],
                                    SecondLastName = sWords[(int)ColumnsEnum.SecondLastName],
                                    CURP = sWords[(int)ColumnsEnum.CURP]
                                };
                                if (person.FirstName == null)
                                {
                                    Console.WriteLine(person);
                                }
                                personNameList.Add(person);
                            }
                        }
                    }
                }
            }
            return useUniqueCURP ? GetUniquePersonName( personNameList) : personNameList;
        }

        private static List<PersonName> GetUniquePersonName(List<PersonName> personNames)
        {
            var curpGroups = personNames.GroupBy(p => p, new PersonComparer());
            return curpGroups.Select(g => g.First()).ToList();
        }
    }
}
