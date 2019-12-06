using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode.Solutions.Year2019.Day04
{
    public class Day04 : AVC
    {
        int lowerRange, upperRange;
        public Day04() : base ("",2019,04)
        {
            lowerRange = int.Parse(Input[0].Substring(0, 6));
            upperRange = int.Parse(Input[0].Substring(7, 6));
        }

        protected override string SolvePartOne()
        {
            return "";//IterateThroughRangeCheckingForMatches(lowerRange, upperRange, false);
        }

        protected override string SolvePartTwo()
        {
            return IterateThroughRangeCheckingForMatches(lowerRange, upperRange, true);
        }

        private string IterateThroughRangeCheckingForMatches(int lowerRange,int upperRange, bool doNotAllowGroupDigits)
        {
            int num = 111122;
            Console.WriteLine(CheckIfNumberMeetsCriteria(num, true));
            int totalMatches = 0;
            int password = lowerRange;
            while (password <= upperRange)
            {
                //Console.Write($"Checking {password} ");
                if (CheckIfNumberMeetsCriteria(password, doNotAllowGroupDigits))
                {
                    //Console.Write($"Meets criteria!");
                    totalMatches++;
                }
                //Console.Write(Environment.NewLine);
                password++;
            }
            return totalMatches.ToString();
        }

        public bool CheckIfNumberMeetsCriteria(int num_, bool doNotAllowGroupDigits)
        {
            bool matchingDigitsCriteriaPass = false;
            //Numbers going from left to right must never decrease, only stay the same or increase.
            bool decreasingNumber = false;
            List<int> digits = IntegerToDigits(num_);
            int[] pairs = new int[10];
            for (int idx = 0; idx < digits.Count - 1; ++idx)
            {
                if (digits[idx] > digits[idx + 1] && digits[idx] != digits[idx + 1])
                {
                    decreasingNumber = true;
                    break;
                }
                if (digits[idx] == digits[idx + 1])
                {
                    pairs[digits[idx]] += 1;
                    matchingDigitsCriteriaPass = true;
                }
            }

            if (matchingDigitsCriteriaPass && doNotAllowGroupDigits) {
                if(!pairs.Any(x => x == 1))
                {
                    if(pairs.Any(x => x > 1))
                    {
                        matchingDigitsCriteriaPass = false;
                    }
                }
            }
            if (matchingDigitsCriteriaPass && !decreasingNumber)
                return true;
            else
                return false;

        }

        private List<int> IntegerToDigits(int base_)
        {
            int temp = base_;
            List<int> digits = new List<int>();
            while(temp > 0)
            {
                digits.Add(temp % 10);
                temp /= 10;
            }
            digits.Reverse();
            return digits;
        }
    }
}
