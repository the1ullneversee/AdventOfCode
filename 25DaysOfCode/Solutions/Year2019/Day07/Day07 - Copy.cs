using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode.Solutions.Year2019.Day07
{
    public class Day08 : AVC
    {
        //private enum PhaseSetting { 0,1,2,3,4};
        //PhaseSettings 0,1,2,3,4 Each used only once!
        string[] input;
        public Day08() : base("Amplification Circuit", 2019, 7)
        {
            //input = Input;
            input = new string[] { "" };
            //input[0] = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            input[0] = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            //input[0] = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            //input = arr.Select(s => s.ToString() + ",").ToArray();
        }

        protected override string SolvePartOne()
        {
            return GetHighestThrustOutput(0,4,false);
        }

        protected override string SolvePartTwo()
        {
            return "";
            //return GetHighestThrustOutput(5, 9, true);
        }

        public string GetHighestThrustOutput(int phaseSettingStart, int phaseSettingEnd, bool feedbackLoop)
        {
            ICC icc = new ICC(input);
            
            //need to run all iterations.
            // 0 - 4.
            Int64 highestOutput = 0;
            int max = 4;
            int amplifiers = 5;
            int comboCounter = 0;
            //int[] outputBuff = new int[1];
            //outputBuff[0] = acs.RunAmplifier(4, 0);
            //outputBuff[0] = acs.RunAmplifier(3, outputBuff[0]);
            //outputBuff[0] = acs.RunAmplifier(2, outputBuff[0]);
            //outputBuff[0] = acs.RunAmplifier(1, outputBuff[0]);
            //outputBuff[0] = acs.RunAmplifier(0, outputBuff[0]);
            ACS[] amps = Enumerable.Range(0, 5).Select(x => new ACS(new ICC(input))).ToArray();
            for (var i = 1; i < amps.Length; i++)
            {
                amps[i].input = amps[i - 1].output;
            }

            if (feedbackLoop)
            {
                amps[0].input = amps[amps.Length - 1].output;
            }
            for (int a = phaseSettingStart; a <= phaseSettingEnd; ++a)
            {
                for (int b = phaseSettingStart; b <= phaseSettingEnd; ++b)
                {
                    for (int c = phaseSettingStart; c <= phaseSettingEnd; ++c)
                    {
                        for (int d = phaseSettingStart; d <= phaseSettingEnd; ++d)
                        {
                            for (int e = phaseSettingStart; e <= phaseSettingEnd; ++e)
                            {
                                int[] perms = { a, b, c, d, e };
                                Int64 output  = RunPhase(amps, perms, feedbackLoop);
                                if (output > highestOutput)
                                {
                                    highestOutput = output;
                                    Console.WriteLine($"New Highest Output {highestOutput}");
                                }
                                icc.ResetMemory();
                                
                            }
                        }
                    }
                }
            }

            return highestOutput.ToString();
        }

        private static Int64 RunPhase(ACS[] amps, int[] phaseSettings, bool feedBackLoop)
        {
            //Each amplifier should run its own code
            

            int[] samePositions = new int[10];
            
            for(int x = 0; x< phaseSettings.Length; ++x) 
                samePositions[phaseSettings[x]] += 1;

            if (samePositions.Where(x => x > 1).Count() != 0)
                return 0;

            
            ICC.ICCOutput output = new ICC.ICCOutput
            {
                //input signal
                outputFromPhase = 0
            };

            for(int idx = 0; idx < phaseSettings.Length; ++idx)
            {
                amps[idx].Reset();
                amps[idx].icc.ResetMemory();

                
                //amps[idx].input.Enqueue(amps[idx-1].output)
            }
            amps[0].input.Enqueue(0);
            for (int idx = 0; idx < phaseSettings.Length; ++idx)
                amps[idx].input.Enqueue(phaseSettings[idx]);


            bool anyHalt = false;
            while (!anyHalt)
            {
                foreach (var amp in amps)
                {
                    output = amp.RunAmplifier();
                    Console.WriteLine($"Phase Output {output.outputFromPhase} OpCode {output.opCodeDetected}");
                    if (output.opCodeDetected == ICC.OpCodes.halt)
                    {
                       //break;
                    }

                }
                if (!feedBackLoop)
                {

                    break;
                }
            }

            //Console.WriteLine($"{a},{b},{c},{d},{e} output of phase { output.outputFromPhase}");
            return amps.Last().output.Single();
        }
    }
}
