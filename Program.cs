using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GermanVocabularyApp
{
    class Program
    {
        static readonly string s_vocabularyPathWords = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\", "Data\\de-dictionary.tsv"));
        static void Main(string[] args)
        {
            Console.WriteLine("Enter input path to File:");
            string inputPathWords = Console.ReadLine();

            try
            {
                Console.WriteLine("Enter output path to File:");
                string resultPath = Console.ReadLine();

                List<string> vocabularyMain = FileWorker.ReadFileToListStrings(s_vocabularyPathWords);
                List<string> vocabularyToRewrite = FileWorker.ReadFileToListStrings(inputPathWords);

                List<string> result = new();

                foreach (var wordToRewrite in vocabularyToRewrite)
                {
                    List<string> rewrittenWords = FindSuitableWords(wordToRewrite, vocabularyMain);
                    result.AddRange(rewrittenWords);
                }

                FileWorker.WriteListToFile(result, resultPath);
            }
            catch
            {
                throw new ArgumentNullException("File does not exist");
            }
        }

        private static List<string> FindSuitableWords(string wordToRewrite, List<string> vocabularyMain)
        {
            List<string> result = new();

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
                result.Add($"{wordToRewrite} -> (out) {firstWord}, {secondWord}");
            else
                result.Add($"{wordToRewrite} -> (out) {wordToRewrite}");

            return result;
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
