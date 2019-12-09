using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day7
    {
        public static int PartOne()
        {
            // all possible permutations
            var vals = new[] { 0, 1, 2, 3, 4 };
            var result = Permutations(vals);
            int[] bestPermutation = new int[5];
            int highestVal = 0;
            foreach (var permutation in result)
            {
                int input = 0;
                for(int i = 0; i < permutation.Length; i++)
                {
                    input = Day5.PartTwo(permutation[i], @"..\..\..\..\..\data\7.txt", input);
                }
                if(input > highestVal)
                {
                    highestVal = input;
                    permutation.CopyTo(bestPermutation, 0);
                }
            }
            return highestVal;
        }

        public static int PartTwo()
        {
            return 0;
        }

        public static IEnumerable<T[]> Permutations<T>(T[] values, int fromInd = 0)
        {
            if (fromInd + 1 == values.Length)
                yield return values;
            else
            {
                foreach (var v in Permutations(values, fromInd + 1))
                    yield return v;

                for (var i = fromInd + 1; i < values.Length; i++)
                {
                    SwapValues(values, fromInd, i);
                    foreach (var v in Permutations(values, fromInd + 1))
                        yield return v;
                    SwapValues(values, fromInd, i);
                }
            }
        }

        private static void SwapValues<T>(T[] values, int pos1, int pos2)
        {
            if (pos1 != pos2)
            {
                T tmp = values[pos1];
                values[pos1] = values[pos2];
                values[pos2] = tmp;
            }
        }
    }
}
