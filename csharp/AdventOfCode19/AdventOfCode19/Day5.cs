using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day5
    {
        public static int PartOne()
        {
            var data = ParseData();
            data[data[1]] = 1;
            int i = 2;
            int lastOutput = 0;
            while (i < data.Count() && data[i] != 99)
            {
                int[] codes = new int[] { 0, 0, 0, 0 };
                int[] parsedCodes = NumToArray(data[i]);
                parsedCodes = parsedCodes.Reverse().ToArray();

                for(int j = 0; j < parsedCodes.Length; j++)
                {
                    codes[j] = parsedCodes[j];
                }
                int opcode = codes[0];
                int mode1 = codes[2];
                int mode2 = codes[3];
                int value1 = mode1 == 0 ? data[data[i + 1]] : data[i + 1];
                int value2 = 0;
                if (opcode == 1 || opcode == 2)
                    value2 = mode2 == 0 ? data[data[i + 2]] : data[i + 2];
                if (opcode == 1)
                {
                    data[data[i + 3]] = value1 + value2;
                    i += 4;
                }
                else if (opcode == 2)
                {
                    data[data[i + 3]] = value1 * value2;
                    i += 4;
                }
                else if (opcode == 4)
                {
                    lastOutput = value1;
                    i += 2;
                }                
            }
            return lastOutput;
        }

        public static int PartTwo(int input = 5, string inputPath = @"..\..\..\..\..\data\5.txt", int secondInput = 0)
        {
            var data = ParseData(inputPath);
            data[data[1]] = input;
            int i = 2;
            int lastOutput = 0;
            while (i < data.Count() && data[i] != 99)
            {
                int[] codes = new int[] { 0, 0, 0, 0 };
                int[] parsedCodes = NumToArray(data[i]);
                parsedCodes = parsedCodes.Reverse().ToArray();

                for (int j = 0; j < parsedCodes.Length; j++)
                {
                    codes[j] = parsedCodes[j];
                }
                int opcode = codes[0];
                int mode1 = codes[2];
                int mode2 = codes[3];
                int value1 = mode1 == 0 ? data[data[i + 1]] : data[i + 1];
                int value2 = 0;
                if (opcode != 4 && opcode != 3)
                    value2 = mode2 == 0 ? data[data[i + 2]] : data[i + 2];
                if (opcode == 1)
                {
                    data[data[i + 3]] = value1 + value2;
                    i += 4;
                }
                else if (opcode == 2)
                {
                    data[data[i + 3]] = value1 * value2;
                    i += 4;
                }
                else if (opcode == 3)
                {
                    data[data[i + 1]] = secondInput;
                    i += 2;
                }
                else if (opcode == 4)
                {
                    lastOutput = value1;
                    i += 2;
                }
                else if(opcode == 5)
                {
                    if (value1 != 0)
                        i = value2;
                    else
                        i += 3;
                }
                else if(opcode == 6)
                {
                    if (value1 == 0)
                        i = value2;
                    else
                        i += 3;
                }
                else if(opcode == 7)
                {
                    data[data[i + 3]] = value1 < value2 ? 1 : 0;
                    i += 4;
                }
                else if (opcode == 8)
                {
                    data[data[i + 3]] = value1 == value2 ? 1 : 0;
                    i += 4;
                }
            }
            return lastOutput;
        }

        static List<int> ParseData(string path = @"..\..\..\..\..\data\5.txt")
        {
            var result = new List<int>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                if (lines.Length > 0)
                {
                    string[] numbersAsStrings = lines[0].Split(',');
                    foreach (string numStr in numbersAsStrings)
                    {
                        if (ConvertStringToInt(numStr) != null)
                            result.Add((int)ConvertStringToInt(numStr));
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

        static int[] NumToArray(int n)
        {
            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }
    }
}
