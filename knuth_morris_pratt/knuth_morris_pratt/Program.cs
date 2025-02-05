using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knuth_morris_pratt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var testFiles = new List<(string FileName, string[] Patterns)>
            {
                ("100words.txt", new[] { "Dodge", "commuting", "cooked breakfast and", "a weather-beaten cardboard bungalow at eighty" }),
                ("1000words.txt", new[] { "voice", "impression", "We walked through a", "the room, blew curtains in at one end and out the" }),
                ("10000words.txt", new[] { "Daisy", "neighbour", "looked at us all", "I meant nothing in particular by this remark, but" }),
                ("100kwords.txt", new[] { "Genoa", "well-known", "All her invitations", "One of the next arrivals was a stout, heavily built" }),
                ("hex_file_100.txt", new[] { "a9e44", "bc1046ef0f", "a58514441094ad83ac8a", "a58514441094ad83ac8aa58514441094ad83ac8a" }),
                ("hex_file_1000.txt", new[] { "bce7c", "3e9b27e80c", "bf5f1ff1eb61bde5f7a7", "1579f6c87ce1bdb31297d75c4fe3992c39cdf0222d0740cd45" }),
                ("hex_file_10000.txt", new[] { "1729b", "c64c9ea19d", "213765bd365eaf2569f9", "2d8e07717c9c3011a562ae09ae7c4e099870d46637b8815c43" }),
                ("hex_file_100000.txt", new[] { "d35de", "5ffd8e3eab", "8270aa4cb1167411c846", "301ec9191aba54b03843e9931631c6d97af6d89f52f5bfbba8" })
            };


            foreach (var (fileName, patterns) in testFiles)
            {
                string text = File.ReadAllText(fileName);
                foreach (var pattern in patterns)
                {
                    KMP(pattern, text);
                }
                Console.WriteLine("------------------");
            }

            Console.ReadLine();
        }
        public static void KMP(string pattern, string text)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = text.Length;
            int m = pattern.Length;
            int[] lps = ComputePrefixFunction(pattern);
            int q = 0;
            int pon = 0;

            for (int i = 0; i < n; i++)
            {
                while (q > 0 && pattern[q] != text[i])
                {
                    q = lps[q - 1];
                }

                if (pattern[q] == text[i])
                {
                    q++;
                }

                if (q == m)
                {
                    pon++;
                    q = lps[q - 1];
                }
            }

            stopwatch.Stop();

            Console.WriteLine("Vreme: " + stopwatch.ElapsedTicks + " ticks");
            Console.WriteLine("Rec " + pattern + " nadjena je " + pon + " puta.");
            Console.WriteLine("***********************");


        }




        static int[] ComputePrefixFunction(string pattern)
        {
            int m = pattern.Length;
            int[] lps = new int[m];
            int k = 0;

            for (int q = 1; q < m; q++)
            {
                while (k > 0 && pattern[k] != pattern[q])
                {
                    k = lps[k - 1];
                }

                if (pattern[k] == pattern[q])
                {
                    k++;
                }

                lps[q] = k;
            }

            return lps;
        }
    }
}
