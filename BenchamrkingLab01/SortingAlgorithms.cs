using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BenchmarkingLab01
{
    [MemoryDiagnoser]
    public class SortingAlgorithms
    {
        private Dictionary<string, int[]> arrays;

        public SortingAlgorithms()
        {
            arrays = new Dictionary<string, int[]>
            {
                { "Random", Generators.GenerateRandom(100,1,1000) },
                { "Sorted", Generators.GenerateSorted(100,1,1000) },
                { "Reversed", Generators.GenerateReversed(100,1,1000) },
                { "AlmostSorted", Generators.AlmostSorted(100,1,1000) },
                { "Few Unique", Generators.FewUnique(100) }
            };
        }

        public IEnumerable<string> GetArrayNames()
        {
            return arrays.Keys;
        }

        [ParamsSource(nameof(GetArrayNames))]
        public string ArrayName { get; set; }

        [Benchmark]
        public int[] InsertionSort()
        {
            int[] arrClone = (int[])arrays[ArrayName].Clone();
            int n = arrClone.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arrClone[i];
                int j = i - 1;

                while (j >= 0 && arrClone[j] > key)
                {
                    arrClone[j + 1] = arrClone[j];
                    j = j - 1;
                }
                arrClone[j + 1] = key;
            }
            return arrClone;
        }
        [Benchmark]
        public void MergeSort()
        {
            int[] arrClone = (int[])arrays[ArrayName].Clone();
            sort(arrClone, 0, arrClone.Length - 1);
        }
        private void merge(int[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;

            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            i = 0;
            j = 0;

            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
        private void sort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;

                sort(arr, l, m);
                sort(arr, m + 1, r);

                merge(arr, l, m, r);
            }

        }
        [Benchmark]
        public void QuickSort()
        {
            int[] arrClone = (int[])arrays[ArrayName].Clone();
            quickSort(arrClone, 0, arrClone.Length - 1);
        }

        private void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        private int partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];

            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, high);
            return (i + 1);
        }
        private void quickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = partition(arr, low, high);

                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }
    }
}
