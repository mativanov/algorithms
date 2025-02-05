using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levenstein
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new List<(string file, string rec)>
            {
                  ("100words.txt", "ruoms"),
                  ("100words.txt", "washington"),
                  ("100words.txt", "commutingtown"),
                  ("1000words.txt", "thing"),
                  ("1000words.txt", "neikhbour’s"),
                  ("1000words.txt", "millsonaires—all"),
                  ("10000words.txt", "baker"),
                  ("10000words.txt", "entertaimed"),
                  ("10000words.txt", "enthusiasticalli"),
                  ("100kwords.txt", "Marya"),
                  ("100kwords.txt", "invitatiomS"),
                  ("100kwords.txt", "self-abnegration"),
                  ("hex_file_100.txt", "a9E44"),
                  ("hex_file_100.txt", "a3o23398dd"),
                  ("hex_file_100.txt", "a58514431094ad83ac8a"),
                  ("hex_file_1000.txt", "bb1d8"),
                  ("hex_file_1000.txt", "bbf1d54bce"),
                  ("hex_file_1000.txt", "d608b31f5abbaeaf8908"),
                  ("hex_file_10000.txt", "13f0e"),
                  ("hex_file_10000.txt", "de17bd51ac"),
                  ("hex_file_10000.txt", "213735bd365eaf2569f9"),
                  ("hex_file_100000.txt", "aC16"),
                  ("hex_file_100000.txt", "b5944561fe"),
                  ("hex_file_100000.txt", "ca2b19348384foo1956b")
             };

            foreach (var (file, rec) in test)
            {
                NadjiSlicnuRec(file, rec);
            }


            Console.ReadLine();
        }

        public static void NadjiSlicnuRec(string file, string trazenaRec)
        {
            int maxDistanca = (int)(trazenaRec.Length * 0.2); 

            foreach (string line in File.ReadLines(file))
            {
                foreach (string rec in line.Split(' '))
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    int distanca = Levenstein(trazenaRec, rec);
                    sw.Stop();
                    if (distanca > 0 && distanca <= maxDistanca)
                    {
                        Console.WriteLine("Za rec: " + trazenaRec + " slicna rec: " + rec + " udaljenost: " + distanca + " Vreme: " + sw.ElapsedTicks);
                    }

                }
            }
        }
        public static int Levenstein(string a, string b)
        {
            int[,] tablica = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
            {
                tablica[i, 0] = i;
            }

            for (int j = 0; j <= b.Length; j++)
            {
                tablica[0, j] = j;
            }

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost;
                    if (a[i - 1] == b[j - 1])
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    int deleteCost = tablica[i - 1, j] + 1;
                    int insertCost = tablica[i, j - 1] + 1;
                    int replaceCost = tablica[i - 1, j - 1] + cost;

                    tablica[i, j] = Math.Min(Math.Min(deleteCost, insertCost), replaceCost);
                }
            }

            return tablica[a.Length, b.Length];
        }

    }
}
