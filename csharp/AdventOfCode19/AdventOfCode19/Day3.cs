using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    struct Vector2 : IEquatable<Vector2>
    {
        public int X;
        public int Y;
        public Vector2(int p1, int p2)
        {
            X = p1;
            Y = p2;
        }

        public bool Equals(Vector2 other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
    
    class Day3
    {
        
        public static int PartOne()
        {
            var wires = ParseDataOne();
            int lowestManhattanDistance = 1000000000;
            wires[0].Remove(new Vector2(0, 0));
            wires[1].Remove(new Vector2(0, 0));
            wires[0].IntersectWith(wires[1]);
            foreach (var point in wires[0])
            {
                lowestManhattanDistance = Math.Min(Math.Abs(point.X) + Math.Abs(point.Y), lowestManhattanDistance);
            }
            return lowestManhattanDistance;
        }

        public static int PartTwo()
        {
            var wiresWithLength = ParseDataTwo();
            var wires = ParseDataOne();
            int lowestManhattanDistance = 1000000000;
            wires[0].Remove(new Vector2(0, 0));
            wires[1].Remove(new Vector2(0, 0));
            wires[0].IntersectWith(wires[1]);
            foreach (var point in wires[0])
            {
                lowestManhattanDistance = Math.Min(wiresWithLength[0][point] + wiresWithLength[1][point], lowestManhattanDistance);
            }
            
            return lowestManhattanDistance;
        }



        static HashSet<Vector2>[] ParseDataOne()
        {
            var wires = new List<HashSet<Vector2>>();
            try
            {
                string[] linesStr = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\3.txt");
                //string[] linesStr = new string[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
                foreach(string wire in linesStr)
                {
                    var wireSet = new HashSet<Vector2>();
                    string[] directions = wire.Split(',');
                    Vector2 currentPosition = new Vector2(0, 0);
                    foreach (string direction in directions)
                    {
                        var numStr = direction.Substring(1);
                        var num = (int)ConvertStringToInt(numStr);
                        Vector2 newPosition = new Vector2(currentPosition.X, currentPosition.Y);
                        if (direction[0] == 'U')
                        {
                            for(int i = 0; i < num; i++)
                            {
                                wireSet.Add(new Vector2(currentPosition.X, currentPosition.Y + i));
                                //Console.WriteLine($"({currentPosition.X},{currentPosition.Y + i}");
                            }
                            newPosition.Y += num;
                        }
                        else if (direction[0] == 'R')
                        {
                            for (int i = 0; i < num; i++)
                            {
                                wireSet.Add(new Vector2(currentPosition.X + i, currentPosition.Y));
                                //Console.WriteLine($"({currentPosition.X +i},{currentPosition.Y}");
                            }
                            newPosition.X += num;
                        }
                        else if (direction[0] == 'D')
                        {
                            for (int i = 0; i < num; i++)
                            {
                                wireSet.Add(new Vector2(currentPosition.X, currentPosition.Y - i));
                                //Console.WriteLine($"({currentPosition.X},{currentPosition.Y - i}");
                            }
                            newPosition.Y -= num;
                        }
                        else if (direction[0] == 'L')
                        {
                            for (int i = 0; i < num; i++)
                            {
                                wireSet.Add(new Vector2(currentPosition.X - i, currentPosition.Y));
                                //Console.WriteLine($"({currentPosition.X -i},{currentPosition.Y}");
                            }
                            newPosition.X -= num;
                        }
                        
                        currentPosition.X = newPosition.X;
                        currentPosition.Y = newPosition.Y;
                        currentPosition = new Vector2(newPosition.X, newPosition.Y);
                    }
                    wires.Add(wireSet);
                    //Console.WriteLine("next wire");
                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("Day 3 directory not found", err);
            }
            return wires.ToArray();
        }

        static Dictionary<Vector2, int>[] ParseDataTwo()
        {
            var wires = new List<Dictionary<Vector2, int>>();
            try
            {
                string[] linesStr = System.IO.File.ReadAllLines(@"..\..\..\..\..\data\3.txt");
                //string[] linesStr = new string[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
                foreach (string wire in linesStr)
                {
                    var wireSet = new Dictionary<Vector2, int>();
                    string[] directions = wire.Split(',');
                    Vector2 currentPosition = new Vector2(0, 0);
                    int length = 0;
                    foreach (string direction in directions)
                    {
                        var numStr = direction.Substring(1);
                        var num = (int)ConvertStringToInt(numStr);
                        Vector2 newPosition = new Vector2(currentPosition.X, currentPosition.Y);
                        if (direction[0] == 'U')
                        {
                            for (int i = 0; i < num; i++)
                            {                                
                                var p = new Vector2(currentPosition.X, currentPosition.Y + i);
                                if(!wireSet.ContainsKey(p))
                                    wireSet.Add(p, length);
                                length++;
                            }
                            newPosition.Y += num;
                        }
                        else if (direction[0] == 'R')
                        {
                            for (int i = 0; i < num; i++)
                            {                                
                                var p = new Vector2(currentPosition.X + i, currentPosition.Y);
                                if (!wireSet.ContainsKey(p))
                                    wireSet.Add(p, length);
                                length++;
                            }
                            newPosition.X += num;
                        }
                        else if (direction[0] == 'D')
                        {
                            for (int i = 0; i < num; i++)
                            {                                
                                var p = new Vector2(currentPosition.X, currentPosition.Y - i);
                                if (!wireSet.ContainsKey(p))
                                    wireSet.Add(p, length);
                                length++;
                            }
                            newPosition.Y -= num;
                        }
                        else if (direction[0] == 'L')
                        {
                            for (int i = 0; i < num; i++)
                            {                                
                                var p = new Vector2(currentPosition.X - i, currentPosition.Y);
                                if (!wireSet.ContainsKey(p))
                                    wireSet.Add(p, length);
                                length++;
                            }
                            newPosition.X -= num;
                        }

                        currentPosition.X = newPosition.X;
                        currentPosition.Y = newPosition.Y;
                        currentPosition = new Vector2(newPosition.X, newPosition.Y);
                    }
                    wires.Add(wireSet);
                    //Console.WriteLine("next wire");
                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("Day 3 directory not found", err);
            }
            return wires.ToArray();
        }

        static int? ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : (int?)null);
        }
    }
}
