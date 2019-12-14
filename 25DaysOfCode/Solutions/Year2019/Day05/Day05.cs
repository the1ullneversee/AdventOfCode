using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode.Solutions.Year2019.Day05
{
    public class Day05 : AVC
    {
        string[] input;
        public Day05() : base("Sunny with a Chance of Asteroids",2019,5)
        {
            input = Input;
        }

        protected override string SolvePartOne()
        {
            ICC icc = new ICC(input);
            icc.manualInputs.Enqueue(1);
            return icc.IntCodeComputer().outputFromPhase.ToString();
        }

        protected override string SolvePartTwo()
        {
            ICC icc = new ICC(input);
            icc.manualInputs.Enqueue(7);
            return icc.IntCodeComputer().outputFromPhase.ToString();
        }
    }
}
