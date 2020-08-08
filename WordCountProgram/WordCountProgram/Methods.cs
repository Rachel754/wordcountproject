using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordCountProgram
{
    public class Methods
    {
        /// <summary>
        /// Reads file from given location and counts instances of all words
        /// </summary>
        /// <param name="filePath">File path of file to read</param>
        public static ReadFileResult GetWordCountsFromFile(string filePath)
        {
            var result = new ReadFileResult
            {
                Status = Status.Error
            };

            try
            {
                // Use default file if none provided
                var sr = new StreamReader(filePath != "" ? filePath : "..\\..\\..\\lotr.txt");

                // Ignore puctuation and empty lines
                char[] delims = { '.', '\'', '"', '!', '?', ',', '(', ')', '\t', '\n', '\r', '/', ':', ';', ' ' };

                string[] words = sr.ReadToEnd().ToLower()
                    .Split(delims, StringSplitOptions.RemoveEmptyEntries);

                var wordCounts = new Dictionary<string, int>();

                foreach (string word in words)
                {
                    // Checks to see if the word already exists in the dictionary
                    if (wordCounts.ContainsKey(word))
                    {
                        // If word exists already, increase the counter by 1
                        wordCounts[word]++;
                    }
                    else
                    {
                        // If it does not exist, add a new pair to the dictionary with that word, and start counter at 1
                        wordCounts.Add(word, 1);
                    }
                }

                var totalWordCount = 0;

                foreach (KeyValuePair<string, int> entry in wordCounts)
                {
                    totalWordCount += entry.Value;
                }

                result.Status = Status.Success;
                result.WordCounts = wordCounts;
                result.TotalWordCount = totalWordCount;

            }
            // Catches the invalid file name
            catch (Exception e)
            {
                //Tells the user what the error was
                Console.WriteLine(e.Message);

                result.Status = Status.InvalidFilePath;

            }

            return result;
        }

        /// <summary>
        /// Gets top words from given dictionary
        /// </summary>
        /// <param name="wordsCounts">Dictonary containing all words and counts</param>
        /// <param name="numberToReturn">Number of words to return in top list</param>
        public static Dictionary<string, int> GetTopWords(Dictionary<string, int> wordsCounts, int numberToReturn)
        {
            Dictionary<string, int> topWords = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> pair in wordsCounts.OrderByDescending(key => key.Value).Take(numberToReturn))
            {
                topWords.Add(pair.Key, pair.Value);
            }

            return topWords;
        }

        /// <summary>
        /// Displays results to the console
        /// </summary>
        /// <param name="topWords">Dictonary containing top words and counts to display</param>
        /// <param name="totalWordCount">Total words counted to display</param>
        public static void DisplayResults(Dictionary<string, int> topWords, int totalWordCount)
        {
            Console.WriteLine($"Top {topWords.Count} words found -");

            //loops through each pair in the dictionary
            foreach (KeyValuePair<string, int> pair in topWords)
            {
                //write the key and value of each pair to the console
                Console.WriteLine("{0,-20} | {1,3}", pair.Key, pair.Value);
            }

            Console.WriteLine($"Total number of words counted - {totalWordCount}");
        }
    }
}
