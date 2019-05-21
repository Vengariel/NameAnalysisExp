using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NameAnalysis
{
    static class PathUtils
    {
        const string FILENAME = "PERSONAL_FEDERALIZADO_2T2012.txt";

        public static string GetPathToFile()
        {
            string pathToFile = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] pathParts = pathToFile.Split("\\");
            pathParts = pathParts.SkipLast(3).ToArray();
            string pathToApp = string.Join("\\", pathParts);

            return Path.Combine(pathToApp, "Data", FILENAME); ;
        }

    }
}
