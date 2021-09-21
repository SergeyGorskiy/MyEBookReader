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
        }

        static string[] CreateWordArray(string uri)
        {
            Console.WriteLine($"Retrieving from {uri}");
            string s = new WebClient().DownloadString(uri);
            return s.Split(new char[] {' ', '\u000A', ',', '.', ';', ':', '-', '_', '?', '!', '/'},
                StringSplitOptions.RemoveEmptyEntries);
        }

        private static void GetCountForWord(string[] words, string term)
        {
            var findWord = words.Where(w => w.ToUpper().Contains(term.ToUpper())).Select(w => w);
            Console.WriteLine($@"Task 3 -- The word ""{term}"" occurs {findWord.Count()} times");
        }
    }
}
