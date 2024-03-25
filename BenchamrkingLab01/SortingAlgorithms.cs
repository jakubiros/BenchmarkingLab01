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
                { "Random", Generators.GenerateRandom(10,1,100) },
                { "Sorted", Generators.GenerateSorted(10,1,100) },
                { "Reversed", Generators.GenerateReversed(10,1,100) },
                { "AlmostSorted", Generators.AlmostSorted(10,1,100) },
                { "Few Unique", Generators.FewUnique(10) }
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
    }
}
