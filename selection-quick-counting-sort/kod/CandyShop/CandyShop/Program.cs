using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandyShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] cene = RandomNiz(1000);
            int n = cene.Length / 5; 
            
            int[] selectionCene = (int[])cene.Clone();
            Performanse(selectionCene, n, "Selection sort", SelectionSort);

            int[] quickCene = (int[])cene.Clone();
            Performanse(quickCene, n, "Quick sort",QuickSort);

            int[] countingCene = (int[])cene.Clone();
            Performanse(countingCene, n, "Counting sort", CountingSort);


            Console.ReadLine();

        }

  

        //selection sort
        static void SelectionSort(int[] niz)
        {
            int n = niz.Length;
            for (int i = 0;i < n-1; i++) {
                int minIndex = i;
                for(int j=i+1;j<n;j++)
                {
                    if (niz[j] < niz[minIndex]) {
                        minIndex = j;
                    }               
                }
                int temp = niz[minIndex];
                niz[minIndex] = niz[i];
                niz[i] = temp;
            }
        }

        // quick sort
        static void QuickSort(int[] niz)
        {
            QuickSort(niz, 0, niz.Length - 1);
        }

        static void QuickSort(int[] niz, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(niz, low, high);
                QuickSort(niz, low, pivotIndex - 1);
                QuickSort(niz, pivotIndex + 1, high);
            }
        }

        static int Partition(int[] niz, int low, int high)
        {
            int pivot = niz[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (niz[j] <= pivot)
                {
                    i++;
                    int temp = niz[i];
                    niz[i] = niz[j];
                    niz[j] = temp;
                }
            }
            int temp1 = niz[i + 1];
            niz[i + 1] = niz[high];
            niz[high] = temp1;
            return i + 1;
        }

        //counting sort
        static void CountingSort(int[] niz)
        {
            int max = 1000000;
            int[] count = new int[max + 1];
            int[] output = new int[niz.Length];

            for(int i=0;i<niz.Length; i++)
            {
                count[niz[i]]++;
            }
            for (int i = 1; i <= max; i++)
            {
                count[i] += count[i - 1];
            }
            for(int i=niz.Length-1; i>=0; i--)
            {
                output[count[niz[i]] - 1] = niz[i];
                count[niz[i]]--;
            }
            for(int i=0;i<niz.Length;i++)
            {
                niz[i] = output[i];
            }
        }

        static int[] RandomNiz(int n)
        {
            Random r = new Random();
            int[] niz = new int[n];
            for(int i=0;i<n;i++)
            {
                niz[i] = r.Next(0, 10001);//do 10 000 
            }
            return niz;
        }

        static void Performanse(int[] niz,int N,string nazivAlgortima, Action<int[]> sortAlgorithm)
        {  
            long memory1 = GetMemoryUsage();

            Stopwatch stopwatch = Stopwatch.StartNew(); 
            sortAlgorithm(niz);
            stopwatch.Stop();

            long memory2 = GetMemoryUsage();
            long utrosenaMem = memory2 - memory1;

            int ukupnaCena = CenaSlatkisa(niz, N);
            Console.WriteLine("Algoritam " + nazivAlgortima);
            Console.WriteLine("Vreme: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine("Utrosena memorija:  " + utrosenaMem + " bytes");
            Console.WriteLine("Ukupna cena:  " + ukupnaCena + " dinara");
            Console.WriteLine("-------------------");
        }
        static long GetMemoryUsage()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64;
        }

        static int CenaSlatkisa(int[] sortiraneCene, int N)
        {
            int cena = 0;
            for (int i = 0; i < sortiraneCene.Length; i += (N + 1))
            {
                cena += sortiraneCene[i];
            }
            return cena;
        }



    }
}
