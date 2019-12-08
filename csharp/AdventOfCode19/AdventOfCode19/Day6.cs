using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day6
    {
        public static int PartOne()
        {
            var childParent = new Dictionary<string, string>();
            int result = 0;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\6.txt");
                if (lines.Length > 0)
                {
                    foreach (string line in lines)
                    {
                        string[] planets = line.Split(')');

                        childParent.Add(planets[1], planets[0]);
                    }

                    foreach (KeyValuePair<string, string> entry in childParent)
                    {
                        string currentPlanet = entry.Key;
                        string currentParent;
                        while (childParent.TryGetValue(currentPlanet, out currentParent))
                        {
                            result += 1;
                            currentPlanet = currentParent;
                        }
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("Day 6 directory not found");
            }
            return result;
        }

        public static int PartTwo()
        {
            var childParent = new Dictionary<string, string>();
            int result = 0;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\6.txt");
                if (lines.Length > 0)
                {
                    foreach (string line in lines)
                    {
                        string[] planets = line.Split(')');

                        childParent.Add(planets[1], planets[0]);
                    }

                    var myParents = new HashSet<string>();
                    string currentPlanet = "YOU";
                    string currentParent;
                    while (childParent.TryGetValue(currentPlanet, out currentParent))
                    {
                        myParents.Add(currentParent);
                        currentPlanet = currentParent;
                    }

                    currentPlanet = "SAN";
                    int santasWay = 0;
                    string intersectionPlanet = "";
                    while (childParent.TryGetValue(currentPlanet, out currentParent))
                    {
                        if (myParents.Contains(currentParent))
                        {
                            intersectionPlanet = currentParent;
                            break;
                        }
                        santasWay++;
                        currentPlanet = currentParent;
                    }

                    currentPlanet = "YOU";
                    int myWay = 0;
                    while (childParent.TryGetValue(currentPlanet, out currentParent))
                    {
                        if (currentParent.CompareTo(intersectionPlanet) == 0)
                        {

                            break;
                        }
                        myWay++;
                        currentPlanet = currentParent;
                    }

                    return myWay + santasWay;

                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("Day 6 directory not found");
            }
            return result;
        }
    }
}
