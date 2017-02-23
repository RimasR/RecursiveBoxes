using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveBoxes
{
    public class Program
    {
        private static List<int> boxList;
        private static int M = 20;
        public static void Main(string[] args)
        {
            List<List<int>> possibleRes = new List<List<int>>();
            int result = 0;
            var tooBigList = new List<int>();
            boxList = new List<int> { 8, 2, 3, 5, 9, 11, 5, 6, 7, 8, 1, 1, 1, 1, 14, 7, 3, 4, 9, 4 };
            boxList = boxList.OrderByDescending(x => x).ToList();

            foreach (var i in boxList)
            {
                if (i > M)
                {
                    tooBigList.Add(i);
                }
            }

            foreach (var j in tooBigList)
            {
                boxList.Remove(j);
            }

            while (boxList.Count != 0)
            {
                var rez = Sort(0, 0);

                if (rez != null)
                {
                    possibleRes.Add(rez);
                    foreach (int value in rez)
                    {
                        boxList.Remove(value);
                    }
                }

                result++;
            }


            foreach (var subset in possibleRes)
            {
                Console.Write("{ ");
                foreach (var variable in subset)
                {
                    Console.Write($"{variable} ");
                }
                Console.Write("},");
            }

            Console.WriteLine($"\r\nMinimum boxes needed: {result}");

        }

        public static List<int> Sort(int sum, int nextIndex)
        {

            var nextElement = boxList.ElementAt(nextIndex);
            sum += nextElement;

            if (sum == M)
            {
                return new List<int> { nextElement };
            }

            if (sum > M)
            {
                return null;
            }

            if (sum < M)
            {
                for (int j = nextIndex + 1; j < boxList.Count; j++)
                {
                    var x = Sort(sum, j);
                    if (x != null)
                    {
                        x.Add(nextElement);
                        return x;
                    }
                }

                return new List<int> { nextElement };
            }

            return null;
        }
    }
}
