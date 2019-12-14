using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdventOfCode19
{
    class Day10
    {
        public static string PartOne()
        {
            var v1 = new Vector(0, 1);
            var v2 = new Vector(1, 0);
            var v3 = new Vector(0, -1);
            var v4 = new Vector(-1, 0);
            var v5 = new Vector(1, -1);
            var v6 = new Vector(-1, -1);
            Console.WriteLine(Vector.AngleBetween(v1, v2));
            Console.WriteLine(Vector.AngleBetween(v1, v5));
            Console.WriteLine(Vector.AngleBetween(v1, v3));
            Console.WriteLine(Vector.AngleBetween(v1, v6));
            Console.WriteLine(Vector.AngleBetween(v1, v4));


            var asteroids = ParseData(@"..\..\..\..\..\data\10.txt");
            var anglesToOtherAsteroids = new Dictionary<Vector, List<double>>();
            int mostVisibleAsteroids = 0;
            Vector bestPosition = new Vector();
            var up = new Vector(0, -1);
            foreach (Vector vec in asteroids)
            {
                var angles = new HashSet<double>();
                foreach (Vector vec2 in asteroids)
                {
                    if (!vec.Equals(vec2))
                        angles.Add(Vector.AngleBetween(up, Vector.Subtract(vec, vec2)));
                }
                if (angles.Count > mostVisibleAsteroids)
                {
                    mostVisibleAsteroids = Math.Max(angles.Count, mostVisibleAsteroids);
                    bestPosition = vec;
                }
            }

            return $"{mostVisibleAsteroids} asteroids visible on position ({bestPosition.X},{bestPosition.Y})";
        }

        public static string PartTwo()
        {
            var asteroids = ParseData(@"..\..\..\..\..\data\10.txt");
            var anglesToOtherAsteroids = new SortedDictionary<double, List<Vector>>();
            var negativeAnglesToOtherAsteroids = new SortedDictionary<double, List<Vector>>();
            Vector bestPosition = new Vector(20, 20);
            var up = new Vector(0, -1);
            foreach (Vector vec in asteroids)
            {
                if (!vec.Equals(bestPosition))
                {
                    var angle = Vector.AngleBetween(up, Vector.Subtract(vec, bestPosition));
                    var otherAsteroids = new List<Vector>();
                    var dict = angle < 0 ? negativeAnglesToOtherAsteroids : anglesToOtherAsteroids;
                    if (dict.ContainsKey(angle))
                    {
                        dict.TryGetValue(angle, out otherAsteroids);
                    }
                    otherAsteroids.Add(vec);
                    try
                    {
                        dict.Add(angle, otherAsteroids);
                    }
                    catch (ArgumentException)
                    {
                        dict.Remove(angle);
                        dict.Add(angle, otherAsteroids);
                    }
                }
            }

            int laseredAsteroids = 0;
            int result = 0;
            string asteroid200 = "";

            double[] anglesToOtherAsteroidsKeys = anglesToOtherAsteroids.Keys.ToArray();
            double[] negativeAnglesToOtherAsteroidsKeys = negativeAnglesToOtherAsteroids.Keys.ToArray();

            while (laseredAsteroids < 200)
            {                
                foreach (double key in anglesToOtherAsteroidsKeys)
                {
                    List<Vector> vectors = new List<Vector>();
                    anglesToOtherAsteroids.TryGetValue(key, out vectors);
                    if (vectors.Count > 0)
                    {
                        var lastVector = vectors[0];
                        Console.WriteLine($"Destroyed asteroid {laseredAsteroids} ({lastVector.X},{lastVector.Y}) at angle {key}");
                        laseredAsteroids++;
                        if (laseredAsteroids == 200)
                            asteroid200 = $"({lastVector.X}, {lastVector.Y})";
                        vectors.RemoveAt(0);
                    }
                }
                foreach (double key in negativeAnglesToOtherAsteroidsKeys)
                {
                    List<Vector> vectors = new List<Vector>();
                    negativeAnglesToOtherAsteroids.TryGetValue(key, out vectors);
                    if (vectors.Count > 0)
                    {
                        var lastVector = vectors[0];
                        Console.WriteLine($"Destroyed asteroid {laseredAsteroids} ({lastVector.X},{lastVector.Y}) at angle {key}");
                        laseredAsteroids++;
                        if (laseredAsteroids == 200)
                        {
                            asteroid200 = $"({lastVector.X}, {lastVector.Y}";
                        }
                        vectors.RemoveAt(0);
                    }
                }
            }
            return asteroid200;
        }

        static List<Vector> ParseData(string path)
        {
            var result = new List<Vector>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                for (int y = 0; y < lines.Length; y++)
                {
                    for (int x = 0; x < lines[y].Length; x++)
                    {
                        if (lines[y][x] == '#')
                            result.Add(new Vector(x, y));
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("directory not found");
            }
            return result;
        }
    }
}
