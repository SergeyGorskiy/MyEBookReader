using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyEBookReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = CreateWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");
            Parallel.Invoke(() =>
            {
                Console.WriteLine("Begin first task...");
                GetLongestWord(words);
            },
            () =>
            {
                Console.WriteLine("Begin second task...");
                GetMostCommonWords(words);
            },
            () =>
            {
                Console.WriteLine("Begin third task...");
                GetCountForWord(words, "oblomov");
            });
            Console.WriteLine("Returned from Parallel.Invoke");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static string[] CreateWordArray(string uri)
        {
            Console.WriteLine($"Retrieving from {uri}");
            string s = new WebClient().DownloadString(uri);
            return s.Split(new char[] {' ', '\u000A', ',', '.', ';', ':', '-', '_', '?', '!', '/'},
                StringSplitOptions.RemoveEmptyEntries);
        }

        private static void GetLongestWord(string[] words)
        {
            var longestWord = words
                .OrderByDescending(w => w.Length)
                .Select(w => new {Name = w, Lenght = w.Length})
                .First();
            Console.WriteLine($"Task 1 -- The longest word is {longestWord.Name} -- {longestWord.Lenght} letters");
        }
        private static void GetMostCommonWords(string[] words)
        {
            var commonWords = words
                .Where(w => w.Length > 6)
                .GroupBy(w => w.ToLower())
                .OrderByDescending(w => w.Count())
                .Select(w => new {Name = w.Key, Count = w.Count()})
                .Take(10);
            
            Console.WriteLine("Task 2 -- The most common words are:");
            foreach (var w in commonWords)
            {
                Console.WriteLine($"\t{w.Name} -- {w.Count}");
            }
        }

        private static void GetCountForWord(string[] words, string term)
        {
            var findWord = words
                .Where(w => w.ToLower().Contains(term.ToLower()))
                .Select(w => w);
            Console.WriteLine($@"Task 3 -- The word ""{term}"" occurs {findWord.Count()} times");
        }
    }
}
