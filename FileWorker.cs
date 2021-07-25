using System.Collections.Generic;
using System.IO;

namespace GermanVocabularyApp
{
    public static class FileWorker
    {
        public static List<string> ReadFileToListStrings(string pathFoFile)
        {
            string inputStringWords = File.ReadAllText(pathFoFile);
            return VocabularyConverter.ToLoverList(inputStringWords);
        }

        public static void WriteListToFile(List<string> rows, string pathToFile)
        {
            File.WriteAllLines(pathToFile, rows);
        }
    }
}
