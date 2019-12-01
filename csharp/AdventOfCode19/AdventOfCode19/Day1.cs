using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    public class Day1
    {
        public static int PartOne()
        {
            var masses = ParseData();
            int result = 0;
            foreach (int mass in masses)
            {
                result += GetFuelForMass(mass);                
            }
            return result;
        }

        public static int PartTwo()
        {
            var masses = ParseData();
            int result = 0;
            foreach (int mass in masses)
            {
                int currentFuel = GetFuelForMass(mass);
                int totalFuel = 0;
                while(currentFuel > 0)
                {
                    totalFuel += currentFuel;
                    currentFuel = GetFuelForMass(currentFuel);
                }
                result += totalFuel;
            }
            return result;
        }

        static int GetFuelForMass(int mass)
        {
            return (mass / 3) - 2;
        }        

        static List<int> ParseData()
        {
            var result = new List<int>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\1.txt");
                foreach (string line in lines)
                {
                    int? number = ConvertStringToInt(line);
                    if (number != null) result.Add((int)number);
                }
            }
            catch(System.IO.DirectoryNotFoundException err)
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
