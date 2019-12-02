using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day2
    {
        public static int PartOne()
        {
            var data = ParseData();
            data[1] = 12;
            data[2] = 2;
            int i = 0;
            while (i < data.Count() && data[i] != 99)
            {
                if (data[i] == 1)
                {
                    data[data[i + 3]] = data[data[i + 1]] + data[data[i + 2]];
                }                    
                else if (data[i] == 2)
                {
                    data[data[i + 3]] = data[data[i + 1]] * data[data[i + 2]];
                }            
                i += 4;
            }
            return data[0];
        }

        public static int PartTwo()
        {
            var parsedData = ParseData();
            int found = 0;
            for(int n = 0; n < 100; n++)
            {
                for(int v = 0; v < 100; v++)
                {
                    int[] data = parsedData.ToArray();
                    data[1] = n;
                    data[2] = v;
                    int i = 0;
                    while (i < data.Count() && data[i] != 99)
                    {
                        if (data[i] == 1)
                        {
                            data[data[i + 3]] = data[data[i + 1]] + data[data[i + 2]];
                        }
                        else if (data[i] == 2)
                        {
                            data[data[i + 3]] = data[data[i + 1]] * data[data[i + 2]];
                        }
                        i += 4;
                    }
                    if(data[0] == 19690720)
                    {
                        found = 100 * n + v;
                        break;
                    }
                }
            }
            
            return found;
        }

        static List<int> ParseData()
        {
            var result = new List<int>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\2.txt");
                if(lines.Length > 0)
                {
                    string[] numbersAsStrings = lines[0].Split(',');
                    foreach (string numStr in numbersAsStrings)
                    {
                        if(ConvertStringToInt(numStr) != null)
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
    }
}
