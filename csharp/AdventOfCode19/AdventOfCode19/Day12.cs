using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day12
    {
        public static int PartOne()
        {
            //var moons = new List<List<int>>() { new List<int>() { -8, -10, 0 }, new List<int>() { 5, 5, 10 }, new List<int>() { 2, -7, 3 }, new List<int>() { 9, -8, -3 } };
            //var moons = new List<List<int>>() { new List<int>() { -1, 0, 2 }, new List<int>() { 2, -10, -7 }, new List<int>() { 4, -8, 8 }, new List<int>() { 3, 5, -1 } };
            var moons = new List<List<int>>() { new List<int>() { 5, 4, 4 }, new List<int>() { -11, -11, -3 }, new List<int>() { 0, 7, 0 }, new List<int>() { -13, 2, 10 } };
            var velocities = new List<List<int>>() { new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 } };
            var allPairs = new List<Tuple<int, int>>() { new Tuple<int, int>(0, 1), new Tuple<int, int>(0, 2), new Tuple<int, int>(0, 3), new Tuple<int, int>(1, 2), new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 3) };
            Console.WriteLine($"After 0 steps:");
            for (int m = 0; m < moons.Count; m++)
            {
                moons[m][0] += velocities[m][0];
                moons[m][1] += velocities[m][1];
                moons[m][2] += velocities[m][2];

                Console.WriteLine($"pos=<x={moons[m][0]}, y={moons[m][1]}, z={moons[m][2]}>, vel=<x={velocities[m][0]}, y={velocities[m][1]}, z={velocities[m][2]}>");
            }
            for (int i = 0; i < 1000; i++)
            {
                
                foreach (Tuple<int, int> pair in allPairs)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (moons[pair.Item1][j] != moons[pair.Item2][j])
                        {
                            velocities[pair.Item1][j] = moons[pair.Item1][j] < moons[pair.Item2][j] ? velocities[pair.Item1][j] + 1 : velocities[pair.Item1][j] - 1;
                            velocities[pair.Item2][j] = moons[pair.Item1][j] < moons[pair.Item2][j] ? velocities[pair.Item2][j] - 1 : velocities[pair.Item2][j] + 1;
                        }
                    }
                }
                if((i+1)%100 == 0) Console.WriteLine($"After {i + 1} steps:");
                for (int m = 0; m < moons.Count; m++)
                {
                    moons[m][0] += velocities[m][0];
                    moons[m][1] += velocities[m][1];
                    moons[m][2] += velocities[m][2];

                    if ((i + 1) % 10 == 0) Console.WriteLine($"pos=<x={moons[m][0]}, y={moons[m][1]}, z={moons[m][2]}>, vel=<x={velocities[m][0]}, y={velocities[m][1]}, z={velocities[m][2]}>");
                }
            }
            int ret = 0;
            for (int m = 0; m < moons.Count; m++)
            {
                int pot = 0;
                int kin = 0;
                for (int c = 0; c < 3; c++)
                {
                    pot += Math.Abs(moons[m][c]);
                    kin += Math.Abs(velocities[m][c]);
                }
                ret += pot * kin;
            }
            Console.WriteLine($"Total energy: {ret}");
            return ret;
        }

        public static int PartTwo()
        {
            return 0;
        }
    }
}
