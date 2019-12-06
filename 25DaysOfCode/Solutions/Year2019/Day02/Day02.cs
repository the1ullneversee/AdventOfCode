using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode.Solutions.Year2019.Day02
{
    public class Day02 : AVC
    {
        int[] opCodeSequence;
        public Day02() : base("1202 Program Alarm",2019,2)
        {
            opCodeSequence = Input[0].Split(',').Select(int.Parse).ToArray();
        }

        protected override string SolvePartOne()
        {
            ICC icc = new ICC(Input);
            icc.SetArrayValues(1, 12);
            icc.SetArrayValues(2, 2);
            return icc.IntCodeComputer()[0].ToString();
        }

        protected override string SolvePartTwo()
        {
            return FindStartingPair(19690720);
        }

        public string FindStartingPair(int product)
        {
            ICC icc = new ICC(Input);
            int verb = 1;
            int max = 99;

            int noun = max / 2;
            bool found = false;
            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    icc.ResetMemory();
                    icc.SetArrayValues(1, i);
                    icc.SetArrayValues(2, j);
                    var output = icc.IntCodeComputer();
                    if (output[0] == product)
                    {
                        verb = j;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    noun = i;
                    break;
                }

            }
            return $"{100 * noun + verb}";
        }
    }
}
