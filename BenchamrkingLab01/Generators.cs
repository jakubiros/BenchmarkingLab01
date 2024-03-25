using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BenchmarkingLab01
{
    public static class Generators
    {
        public static int[] GenerateRandom(int size, int minVal, int maxVal=0)
        {
            var rand=new Random();
            int[] a = new int[size];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rand.Next(minVal, maxVal);
            }
            return a;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }
        public static int[] AlmostSorted(int size, int minVal, int maxVal, double percentage=0.1)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            foreach (var item in a) { var t = item; }
            int minCount = (int)(size * percentage);
            var rand = new Random();
            for (int i = 0; i< minCount; i++)
            {
                int index1 = rand.Next(size);
                int index2 = rand.Next(size);
                int temp = a[index1];
                a[index1] = a[index2];
                a[index2] = temp;
            }
            return a;
        }

    }
}
