using System;
using System.Collections.Generic;

namespace ABCSort_13
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string>{ "аыв", "аа", "цау", "ро" };
            List<string> str = ABSSort(words);
            Console.WriteLine();
        }
    public static List<string> ABSSort(List<string> words, int rank = 0)
        {
            if (words.Count <= 1)
                return words;

            var square = new Dictionary<char, List<string>>(62);
            var result = new List<string>();
            int shortWordsCounter = 0;
            foreach (var word in words)
            {
                if (rank < word.Length)
                {
                    if (square.ContainsKey(word[rank]))
                        square[word[rank]].Add(word);
                    else
                        square.Add(word[rank], new List<string> { word });
                }
                else
                {
                    result.Add(word);
                    shortWordsCounter++;
                }
            }
            if (shortWordsCounter == words.Count)
                return words;

            for (char i = 'А'; i <= 'я'; i++)
            {
                if (square.ContainsKey(i))
                {
                    foreach (var word in ABSSort(square[i], rank + 1))
                        result.Add(word);
                }
            }
            return result;
        }
    }
}
