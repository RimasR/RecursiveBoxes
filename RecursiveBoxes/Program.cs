using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveBoxes
{
    public class Program
    {
        private static List<int> _boxList;
        private static int _maxBoxVolume;

        public static void Main(string[] args)
        {
            _boxList = new List<int> { 8, 2, 4, 3, 5, 6, 1};
            _maxBoxVolume = 10;
            GetMinimumBoxes();

        }

        public static int GetMinimumBoxes()
        {
            List<List<int>> setOfResultSets = new List<List<int>>();
            int result = 0;
            var listOfExceedingVolumes = new List<int>();
            _boxList = _boxList.OrderByDescending(x => x).ToList();

            foreach (var boxVolume in _boxList)
            {
                if (boxVolume > _maxBoxVolume)
                {
                    listOfExceedingVolumes.Add(boxVolume);
                }
            }

            foreach (var boxVolume in listOfExceedingVolumes)
            {
                _boxList.Remove(boxVolume);
            }

            while (_boxList.Count != 0)
            {
                var subsetOfBoxVolumes = Sort(0, 0);

                if (subsetOfBoxVolumes != null)
                {
                    setOfResultSets.Add(subsetOfBoxVolumes);
                    foreach (int boxVolume in subsetOfBoxVolumes)
                    {
                        _boxList.Remove(boxVolume);
                    }
                }

                result++;
            }


            foreach (var subset in setOfResultSets)
            {
                Console.Write("{ ");
                foreach (var volume in subset)
                {
                    Console.Write($"{volume} ");
                }
                Console.Write("},");
            }

            Console.WriteLine($"\r\nMinimum boxes needed: {result}");

            return result;
        }

        public static List<int> Sort(int sum, int nextIndex)
        {
            var nextElement = _boxList.ElementAt(nextIndex);
            sum += nextElement;

            if (sum == _maxBoxVolume)
            {
                return new List<int> { nextElement };
            }

            if (sum > _maxBoxVolume)
            {
                return null;
            }

            if (sum < _maxBoxVolume)
            {
                for (int index = nextIndex + 1; index < _boxList.Count; index++)
                {
                    var boxVolumeSubset = Sort(sum, index);
                    if (boxVolumeSubset != null)
                    {
                        boxVolumeSubset.Add(nextElement);
                        return boxVolumeSubset;
                    }
                }

                return new List<int> { nextElement };
            }

            return null;
        }
    }
}
