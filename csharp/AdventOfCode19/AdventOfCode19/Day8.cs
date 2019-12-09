using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day8
    {
        public static int PartOne()
        {
            var data = ParseData();
            var zero = new int[data.Count / (25 * 6)];
            var one = new int[data.Count / (25 * 6)];
            var two = new int[data.Count / (25 * 6)];
            int pointer = 0;
            while (pointer < data.Count)
            {
                if (data[pointer] == 0)
                    zero[pointer / (25 * 6)] = zero[pointer / (25 * 6)] + 1;
                else if (data[pointer] == 1)
                    one[pointer / (25 * 6)] = one[pointer / (25 * 6)] + 1;
                else if (data[pointer] == 2)
                    two[pointer / (25 * 6)] = two[pointer / (25 * 6)] + 1;
                pointer++;
            }
            int lowestZeroIdx = 0;
            int lowestZeroVal = int.MaxValue;
            for (int i = 0; i < zero.Length; i++)
            {
                if (zero[i] < lowestZeroVal)
                {
                    lowestZeroIdx = i;
                    lowestZeroVal = zero[i];
                }
            }
            return one[lowestZeroIdx] * two[lowestZeroIdx];
        }

        public static string PartTwo()
        {
            var data = ParseData();
            var img = new int[25 * 6];
            for (int i = 0; i < 25 * 6; i++)
            {
                int val = data[i];
                int j = 1;
                while (val == 2 && i + j * 25 * 6 < data.Count)
                {
                    val = data[i + j * 25 * 6];
                    j++;
                }
                img[i] = val;
            }
            string result = "\n";
            for (int i = 0; i < 25 * 6; i++)
            {
                result += i != 0 && i % 25 == 0 ? "\n" : "";
                result += img[i] != 0 ? "#" : " ";                
            }
                
            return result;
        }

        static List<int> ParseData()
        {
            var result = new List<int>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\8.txt");
                if (lines.Length > 0)
                {

                    foreach (char numStr in lines[0])
                    {
                        if (ConvertStringToInt(numStr.ToString()) != null)
                            result.Add((int)ConvertStringToInt(numStr.ToString()));
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("Day 1 directory not found");
            }
            return result;
        }

        static int? ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : (int?)null);
        }
    }
}
