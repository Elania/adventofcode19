using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode19
{
    class Day4
    {
        public static int PartOne()
        {
            int low = 158126;
            int high = 624574;
            int total = 0;
            for(int i = low; i < high; i++)
            {
                int[] digits = NumToArray(i);
                if (DoIncrease(digits) && HasMatchingDigits(digits))
                    total++;
            }
            return total;
        }

        public static int PartTwo()
        {
            int low = 158126;
            int high = 624574;
            int total = 0;
            for (int i = low; i < high; i++)
            {
                int[] digits = NumToArray(i);
                if (DoIncrease(digits) && HasPair(digits))
                {
                    total++;
                }

            }
            return total;
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

        static bool HasMatchingDigits(int[] digits)
        {
            for(int i = 1; i < digits.Length; i++)
            {
                if (digits[i-1] == digits[i])
                    return true;
            }
            return false;
        }

        static bool HasPair(int[] digits)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                // two adjacent digits are the same
                if ((i + 1 < digits.Length && digits[i] == digits[i + 1]))
                {
                    // rear digit is different or empty                           previous digit is different or empty
                    if ((i + 2 >= digits.Length || digits[i] != digits[i + 2]) && (i - 1 < 0 || digits[i] != digits[i - 1]))
                    {
                        return true;
                    }                        
                }
            }
            return false;
        }

        static bool DoIncrease(int[] digits)
        {
            int lastDigit = digits[0];
            for (int i = 1; i < digits.Length; i++)
            {
                if (lastDigit > digits[i])
                    return false;
                lastDigit = digits[i];
            }
            return true;
        }
    }
}
