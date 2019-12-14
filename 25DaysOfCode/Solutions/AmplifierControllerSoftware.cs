using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _25DaysOfCode.Solutions.ICC;

namespace _25DaysOfCode.Solutions
{
  
    public class ACS
    {

        int[] mem;
        int ip;
        public Queue<int> input = new Queue<int>();
        public Queue<int> output = new Queue<int>();

        public void Reset()
        {
            //mem = prgInput.Split(',').Select(int.Parse).ToArray();
            input.Clear();
            output.Clear();
            ip = 0;
        }

        public ICC icc;

        public ACS(ICC icc_)
        {
            icc = icc_;
        }

        public ICCOutput RunAmplifier()
        {
            
            icc.manualInputs = input;
            foreach(var val in icc.manualInputs)
            {
                Console.WriteLine(val);
            }
            
            var result = icc.IntCodeComputer();
            output.Enqueue((int)result.outputFromPhase);
            return result;
        }
    }
}
