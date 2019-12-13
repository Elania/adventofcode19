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
                for (int i = 0; i < permutation.Length; i++)
                {
                    input = Day5.PartTwo(permutation[i], @"..\..\..\..\..\data\7.txt", input);
                }
                if (input > highestVal)
                {
                    highestVal = input;
                    permutation.CopyTo(bestPermutation, 0);
                }
            }
            return highestVal;
        }

        public static int PartTwo()
        {
            // all possible permutations
            var vals2 = new[] { 5, 6, 7, 8, 9 };
            var result2 = Permutations(vals2);
            int[] bestPermutation = new int[5];
            int highestVal2 = 0;
            foreach (var permutation in result2)
            {
                //var testPermutation = new int[] { 9, 8, 7, 6, 5 };
                var programs = new IntCodeProgram[5];
                for (int i = 0; i < programs.Length; i++)
                {
                    programs[i] = new IntCodeProgram(permutation[i], @"..\..\..\..\..\data\7.txt");
                    //programs[i] = new IntCodeProgram(testPermutation[i]);
                }
                int currentProgramIdx = 0;
                bool isFinished = false;
                int io = 0;
                while (!isFinished)
                {
                    //Console.WriteLine($"program {currentProgramIdx} i {io}");
                    io = programs[currentProgramIdx].Run(io);
                    isFinished = programs[currentProgramIdx].Finished;
                    //Console.WriteLine($"program {currentProgramIdx} o {io} isFinished {isFinished}");
                    if (currentProgramIdx == 4 && io > highestVal2)
                    {
                        highestVal2 = io;
                        permutation.CopyTo(bestPermutation, 0);
                    }
                    currentProgramIdx = (currentProgramIdx + 1) % programs.Length;
                }

                
            }
            return highestVal2;
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

    class IntCodeProgram
    {
        private int _phaseSetting;
        private List<int> _data;
        private int _output;
        private int _iteratorPos;

        public int Output
        {
            get
            {
                return _output;
            }

            set
            {
                _output = value;
            }
        }

        public bool Finished
        {
            get
            {
                return _data[_iteratorPos] == 99;
            }
        }

        public IntCodeProgram(int phaseSetting, string inputPath = @"..\..\..\..\..\data\7.txt")
        {
            //_data = ParseData(inputPath);
            _data = new List<int>()
                        {
                            3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5
                        };
            _iteratorPos = 2;
            _phaseSetting = phaseSetting;
            _data[_data[1]] = _phaseSetting;
        }
        public int Run(int input = 0)
        {            
            bool halt = false;
            bool inputConsumed = false;
            while (_iteratorPos < _data.Count() && _data[_iteratorPos] != 99 && !halt)
            {
                int[] codes = new int[] { 0, 0, 0, 0 };
                int[] parsedCodes = NumToArray(_data[_iteratorPos]);
                parsedCodes = parsedCodes.Reverse().ToArray();

                for (int j = 0; j < parsedCodes.Length; j++)
                {
                    codes[j] = parsedCodes[j];
                }
                int opcode = codes[0];
                int mode1 = codes[2];
                int mode2 = codes[3];
                int value1 = mode1 == 0 ? _data[_data[_iteratorPos + 1]] : _data[_iteratorPos + 1];
                int value2 = 0;
                if (opcode != 4 && opcode != 3)
                    value2 = mode2 == 0 ? _data[_data[_iteratorPos + 2]] : _data[_iteratorPos + 2];
                if (opcode == 1)
                {
                    _data[_data[_iteratorPos + 3]] = value1 + value2;
                    _iteratorPos += 4;
                }
                else if (opcode == 2)
                {
                    _data[_data[_iteratorPos + 3]] = value1 * value2;
                    _iteratorPos += 4;
                }
                else if (opcode == 3)
                {
                    if (inputConsumed) Console.WriteLine("Next input without Output");
                    _data[_data[_iteratorPos + 1]] = input;
                    inputConsumed = true;
                    _iteratorPos += 2;
                }
                else if (opcode == 4)
                {
                    Output = value1;
                    _iteratorPos += 2;
                    halt = true;
                }
                else if (opcode == 5)
                {
                    if (value1 != 0)
                        _iteratorPos = value2;
                    else
                        _iteratorPos += 3;
                }
                else if (opcode == 6)
                {
                    if (value1 == 0)
                        _iteratorPos = value2;
                    else
                        _iteratorPos += 3;
                }
                else if (opcode == 7)
                {
                    _data[_data[_iteratorPos + 3]] = value1 < value2 ? 1 : 0;
                    _iteratorPos += 4;
                }
                else if (opcode == 8)
                {
                    _data[_data[_iteratorPos + 3]] = value1 == value2 ? 1 : 0;
                    _iteratorPos += 4;
                }
            }
            if(!inputConsumed)
            {
                Console.WriteLine("Output before next input");
            }
            return this.Output;
        }
        static List<int> ParseData(string path)
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
                Console.WriteLine("directory not found");
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
