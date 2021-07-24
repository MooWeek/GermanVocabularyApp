using System.Collections.Generic;
using System.Linq;

namespace GermanVocabularyApp
{
    public static class VocabularyConverter
    {
        public static List<string> ToLoverList(string vocabularyWords)
        {
            return vocabularyWords.ToLower().Replace("\r", "").Split("\n").ToList();
        }
    }
}
