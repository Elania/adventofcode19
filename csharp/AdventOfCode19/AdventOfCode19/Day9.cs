using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day9
    {
        public static long PartOne()
        {
            var program = new IntCodeProgram9();
            var input = new Queue<long>();
            input.Enqueue(1);
            return program.Run(input);
        }

        public static long PartTwo()
        {
            var program = new IntCodeProgram9();
            var input = new Queue<long>();
            input.Enqueue(2);
            return program.Run(input);
        }            
    }
    
    class IntCodeProgram9
    {
        private Dictionary<int, long> _data;
        private long _output;
        private int _iteratorPos;
        private int _relativeBase;
        private IntCodeProgramState _state;

        public long Output
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

        public IntCodeProgramState State
        {
            get
            {
                return _state;
            }
        }
        public IntCodeProgram9(string inputPath = @"..\..\..\..\..\data\9.txt")
        {
            _data = ParseData(inputPath);
            //_data = ParseList(new List<long>()
            //{
            //    109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99
            //});
            _iteratorPos = 0;
            _relativeBase = 0;
        }
        public long Run(Queue<long> input)
        {
            _state = IntCodeProgramState.RUNNING;
            Output = 0;
            while (_iteratorPos < _data.Count() && _data[_iteratorPos] != 99 && _state == IntCodeProgramState.RUNNING)
            {
                //Console.WriteLine($"opcode: {_data[_iteratorPos]}");
                long[] codes = new long[] { 0, 0, 0, 0, 0 };
                long[] parsedCodes = NumToArray(_data[_iteratorPos]);
                parsedCodes = parsedCodes.Reverse().ToArray();

                for (int j = 0; j < parsedCodes.Length; j++)
                {
                    codes[j] = parsedCodes[j];
                }
                long opcode = codes[0];
                long mode1 = codes[2];
                long mode2 = codes[3];
                long mode3 = codes[4];
                long value1 = 0;
                switch (mode1)
                {
                    case 0:
                        value1 = GetData((int)GetData(_iteratorPos + 1));
                        break;
                    case 1:
                        value1 = GetData(_iteratorPos + 1);
                        break;
                    case 2:
                        value1 = GetData((int)GetData(_iteratorPos + 1) + _relativeBase);
                        break;
                }
                long value2 = 0;
                int value3 = 0;
                if (opcode != 4 && opcode != 3 && opcode != 9)
                {
                    switch (mode2)
                    {
                        case 0:
                            value2 = GetData((int)GetData(_iteratorPos + 2));
                            break;
                        case 1:
                            value2 = GetData(_iteratorPos + 2);
                            break;
                        case 2:
                            value2 = GetData((int)GetData(_iteratorPos + 2) + _relativeBase);
                            break;
                    }
                    if(opcode == 1 || opcode == 2 || opcode == 7 || opcode == 8)
                    {
                        switch (mode3)
                        {
                            case 0:
                                value3 = (int)GetData(_iteratorPos + 3);
                                break;
                            case 1:
                                value3 = _iteratorPos + 3;
                                break;
                            case 2:
                                value3 = (int)GetData(_iteratorPos + 3) + _relativeBase;
                                break;
                        }
                    }
                }
                
                if (opcode == 1)
                {
                    SetData(value3, value1 + value2);
                    _iteratorPos += 4;
                }
                else if (opcode == 2)
                {
                    SetData(value3, value1 * value2);
                    _iteratorPos += 4;
                }
                else if (opcode == 3)
                {
                    if (input.Count > 0)
                    {
                        long currentInput = input.Dequeue();
                        switch (mode1)
                        {
                            case 0:
                                value1 = (int)GetData(_iteratorPos + 1);
                                break;
                            case 1:
                                value1 = _iteratorPos + 1;
                                break;
                            case 2:
                                value1 = (int)GetData(_iteratorPos + 1) + _relativeBase;
                                break;
                        }
                        int tmpPos = (int)value1;
                        SetData(tmpPos, currentInput);
                        _iteratorPos += 2;
                    }
                    else
                        _state = IntCodeProgramState.PAUSED;
                }
                else if (opcode == 4)
                {
                    Output = value1;
                    //Console.WriteLine(value1);
                    _iteratorPos += 2;
                }
                else if (opcode == 5)
                {
                    if (value1 != 0)
                        _iteratorPos = (int)value2;
                    else
                        _iteratorPos += 3;
                }
                else if (opcode == 6)
                {
                    if (value1 == 0)
                        _iteratorPos = (int)value2;
                    else
                        _iteratorPos += 3;
                }
                else if (opcode == 7)
                {
                    SetData(value3, value1 < value2 ? 1 : 0);
                    _iteratorPos += 4;
                }
                else if (opcode == 8)
                {
                    SetData(value3, value1 == value2 ? 1 : 0);
                    _iteratorPos += 4;
                }
                else if (opcode == 9)
                {
                    _relativeBase += (int)value1;
                    _iteratorPos += 2;
                }
            }
            if (_data[_iteratorPos] == 99)
            {
                _state = IntCodeProgramState.FINISHED;
            }
            return this.Output;
        }

        void SetData(int index, long value)
        {
            try
            {
                _data.Add(index, value);
            }
            catch (ArgumentException)
            {
                _data.Remove(index);
                _data.Add(index, value);
            }
        }

        long GetData(int index)
        {
            long ret = 0;
            try
            {
                ret = _data[index];
            }
            catch (KeyNotFoundException)
            {
                _data.Add(index, 0);
            }
            return ret;
        }

        static Dictionary<int, long> ParseData(string path)
        {
            var result = new Dictionary<int,long>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                if (lines.Length > 0)
                {
                    string[] numbersAsStrings = lines[0].Split(',');
                    for (int i = 0; i < numbersAsStrings.Length; i++)
                    {
                        if (ConvertStringToInt(numbersAsStrings[i]) != null)
                            result.Add(i, (long)ConvertStringToInt(numbersAsStrings[i]));
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException err)
            {
                Console.WriteLine("directory not found");
            }
            return result;
        }

        static Dictionary<int, long> ParseList(List<long> list)
        {
            var result = new Dictionary<int, long>();
            for (int i = 0; i < list.Count; i++)
            {                        
                result.Add(i, list[i]);
            }
            return result;
        }

        static long? ConvertStringToInt(string intString)
        {
            long i = 0;
            return (Int64.TryParse(intString, out i) ? i : (long?)null);
        }

        static long[] NumToArray(long n)
        {
            var digits = new List<long>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }
    }
}

