using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode.Solutions.Year2019.Day06
{
    public class Day06 : AVC
    {
        string[] input;

        public Day06() : base ("Universal Orbit Map",2019,6)
        {
            input = Input;
        }

        protected override string SolvePartOne()
        {
            return CalculateSharedOrbits(input).ToString();
        }

        protected override string SolvePartTwo()
        {
            throw new NotImplementedException();
        }

        public int CalculateSharedOrbits(string[] orbitMap)
        {

            HashSet<string> uo = new HashSet<string>();

            foreach(var pair in orbitMap)
            {
                var parent = pair.Split(')')[0];
                int length = parent.Length;
                var child = pair.Split(')')[1];
                var matches = uo.Where(x => x.Substring(x.Length - length) == parent);
                if(matches.Count() == 0)
                {
                    uo.Add($"{parent}{child}");
                }
                else
                {
                    var found = true;
                }
                
            }

            return 0;
        }
    }
}
