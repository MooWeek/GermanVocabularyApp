using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GermanVocabularyApp
{
    class Program
    {
        static string s_vocabularyPathWords = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", "Data\\de-dictionary.tsv"));
        static void Main(string[] args)
        {

            //string inputPathWords = Console.ReadLine();

            string inputPathWords = "I:\\Develop\\C#Projs\\GermanVocabularyApp\\Data\\de-test-words.tsv"; // TO DELETE(ONLY FOR TEST)
            try
            {
                //string resultPath = Console.ReadLine();

                List<string> vocabularyMain = FileWorker.ReadFileToListStrings(s_vocabularyPathWords);

                List<string> vocabularyToRewrite = FileWorker.ReadFileToListStrings(inputPathWords);

                foreach(var wordToRewrite in vocabularyToRewrite)
                {
                    FindSuitableWords(wordToRewrite, vocabularyMain);
                }
            }
            catch
            {
                throw new ArgumentNullException("File does not exist");
            }
        }

        private static void FindSuitableWords(string wordToRewrite, List<string> vocabularyMain)
        {
            string firstWord = "";
            string secondWord = "";

            List<string> wordsEqualsFirstPart = FindWordsForFirstPart(wordToRewrite, vocabularyMain);


            foreach (var firstPartWord in wordsEqualsFirstPart)
            {
                string wordSecondPart = wordToRewrite.Substring(firstPartWord.Length);

                List<string> wordsEqualsSecondPart = FindWordsForSecondPart(wordSecondPart, vocabularyMain);

                if (wordsEqualsSecondPart.Count != 0)
                {
                    firstWord = firstPartWord;
                    secondWord = wordsEqualsSecondPart[0];
                    break;
                }
            }

            if (firstWord.Length > 0 && secondWord.Length > 0)
            {
                Console.WriteLine($"{wordToRewrite} -> (out) {firstWord}, {secondWord}");
            }
            else
            {
                Console.WriteLine($"{wordToRewrite} -> (out) {wordToRewrite}");
            }

            Console.WriteLine();
        }

        private static List<string> FindWordsForSecondPart(string wordSecondPart, List<string> vocabularyMain)
        {
            return vocabularyMain.Where(word => word[0] == wordSecondPart[0]).
                                  Where(word => word.Length <= wordSecondPart.Length).
                                  Where(word => word == wordSecondPart).
                                  ToList();
        }

        private static List<string> FindWordsForFirstPart(string wordToRewrite, List<string> vocabularyMain)
        {
            return vocabularyMain.Where(word => word[0] == wordToRewrite[0]).
                                  Where(word => word.Length < wordToRewrite.Length).
                                  Where(word => word == wordToRewrite.Substring(0, word.Length)).
                                  OrderByDescending(word => word.Length).
                                  ToList();
        }
    }
}
