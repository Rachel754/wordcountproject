using System;

namespace WordCountProgram
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!");

            while (true)
            {
                Console.WriteLine("Please enter a filepath to read:");

                var wordCountsResult = Methods.GetWordCountsFromFile(Console.ReadLine());

                if(wordCountsResult.Status == Status.Success)
                {
                    var topTenWords = Methods.GetTopWords(wordCountsResult.WordCounts, 10);

                    Methods.DisplayResults(topTenWords, wordCountsResult.TotalWordCount);

                    break;
                }
                else
                {
                    Console.WriteLine("Invalid file path, try again.");
                }
            }
        }
    }
}
