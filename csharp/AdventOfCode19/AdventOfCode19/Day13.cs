using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdventOfCode19
{
    class Day13
    {
        public static int PartOne()
        {
            var program = new IntCodeProgram11(@"..\..\..\..\..\data\13.txt");
            var input = new Queue<long>();
            var output = program.Run(input);
            var blocks = new HashSet<Vector>();
            while (output.Count > 0)
            {
                var x = output.Dequeue();
                var y = output.Dequeue();
                var type = output.Dequeue();
                if (type == 2)
                    blocks.Add(new Vector(x, y));
            }
            return blocks.Count;
        }
        public static long PartTwo()
        {
            var program = new IntCodeProgram11(@"..\..\..\..\..\data\13.txt");

            var height = 21;
            var width = 38;
            long score = 0;
            long currentPaddlePos = 0;
            long currentBallPos = 0;
            long lastBallPos = 0;
            long[] game = new long[width * height];
            {
                var input = new Queue<long>();
                //input.Enqueue(2);

                var output = program.Run(input);
                while (output.Count > 0)
                {
                    var x = output.Dequeue();
                    var y = output.Dequeue();
                    var type = output.Dequeue();
                    if (x == -1 && y == 0)
                        score = type;
                    else
                        game[x + y * width] = type;
                    if (type == 3)
                        currentPaddlePos = x;
                    else if(type == 4)
                    {
                        currentBallPos = x;
                        lastBallPos = x;
                    }

                }

                Print(game, score, width, height);
            }
            long nextInput = -1;
            while(ContainsBlocks(game))
            {
                var input = new Queue<long>();
                input.Enqueue(nextInput);
                var output = program.Run(input);
                while (output.Count > 0)
                {
                    var x = output.Dequeue();
                    var y = output.Dequeue();
                    var type = output.Dequeue();
                    if (x == -1 && y == 0)
                        score = type;
                    else
                        game[x + y * width] = type;
                    if (type == 3)
                        currentPaddlePos = x;
                    else if (type == 4)
                    {
                        currentBallPos = x;
                    }
                }
                Print(game, score, width, height);
                if (currentPaddlePos < currentBallPos)
                {                
                    nextInput = 1;
                    //currentPaddlePos++;
                }
                else if (currentPaddlePos > currentBallPos)
                {
                    nextInput = -1;
                    //currentPaddlePos--;
                }
                else
                    nextInput = 0;
            }


            return score;
        }

        static bool ContainsBlocks(long[] game)
        {
            int i = 38;
            while (i < game.Length)
            {
                if (game[i] == 2)
                    return true;
                i++;
            }
            return false;
        }

        static void Print(long[] game, long score, int width, int height)
        {
            int i = 0;
            string line = $"Score: {score}\n";
            while (i < width * height)
            {
                switch (game[i])
                {
                    case 0:
                        line += " ";
                        break;
                    case 1:
                        line += "#";
                        break;
                    case 2:
                        line += "X";
                        break;
                    case 3:
                        line += "_";
                        break;
                    case 4:
                        line += "O";
                        break;
                }
                if (i % width == width - 1)
                    line += "\n";
                i++;
            }
            Console.WriteLine(line);
        }
    }
}
