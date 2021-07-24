using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GermanVocabularyApp
{
    public static class FileWorker
    {
        public static List<string> ReadFileToListStrings(string pathFoFile)
        {
            string inputStringWords = File.ReadAllText(pathFoFile);
            return VocabularyConverter.ToLoverList(inputStringWords);
        }
    }
}
