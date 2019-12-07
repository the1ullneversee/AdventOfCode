using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode.Solutions
{
    public class ICC
    {
        public enum OpCodes { add = 1, times = 2, halt = 99 , input = 3, output = 4};
        public enum ModuleCodes { positionMode = 0, immediateMode = 1}
        int[] opCodeSequence;
        int[] MasterSequence;

        public ICC(string[] Input_)
        {
            MasterSequence = Input_[0].Split(',').Select(int.Parse).ToArray();
            opCodeSequence = (int[])MasterSequence.Clone();
        }

        

        public void ResetMemory()
        {
            opCodeSequence = (int[])MasterSequence.Clone();
        }

        public void InitMemory()
        {
            opCodeSequence = (int[])MasterSequence.Clone();
        }

        public void SetArrayValues(int index, int value)
        {
            opCodeSequence[index] = value;
        }

        public string IntCodeComputer()
        {
            //InitMemory();
            int jumpIndex = 0;
            string output = "";
            while (ProcessOptCode(ref opCodeSequence, jumpIndex, ref output) && jumpIndex <= opCodeSequence.Length)
            {
                //output = ProcessOptCode(ref opCodeSequence, jumpIndex);
                jumpIndex += 4;
            }

            return output;
        }

        public int[] IntCodeComputerWithMode()
        {
            //InitMemory();
            int currentPosition = 0;
            int instructionLength = 4;
            string output = "";
            while (currentPosition <= opCodeSequence.Length)
            {
                output += ProcessOptCodeWithMode(ref opCodeSequence, currentPosition, ref instructionLength);
                currentPosition += instructionLength;
            }

            return opCodeSequence;
        }

        public bool ProcessOptCode(ref int[] opCodeSeq,int instructionStart, ref string output)
        {
            try
            {
                //new route is ABCDE
                //before we do the operation. We must work out what mode we are in.
                int pos1, pos2, pos3;
                pos3 = opCodeSeq[instructionStart + 3];
                pos2 = opCodeSeq[instructionStart + 2];
                pos1 = opCodeSeq[instructionStart + 1];
                switch ((OpCodes)opCodeSeq[instructionStart])
                {
                    case OpCodes.add:
                        opCodeSeq[pos3] = opCodeSeq[pos1] + opCodeSeq[pos2];
                        break;
                    case OpCodes.times:
                        opCodeSeq[pos3] = opCodeSeq[pos1] * opCodeSeq[pos2];
                        break;
                    case OpCodes.halt:
                        output = opCodeSeq[0].ToString();
                        return false;
                }
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }

        }

        public Tuple<OpCodes, ModuleCodes, ModuleCodes, ModuleCodes> ProcessInstruction(List<int> instruction_)
        {
            //insturction should be in the format of ABCDE. But in digits.
            //DE is the two digit OpCode
            //C mode of 1st Param
            //B mode of 2nd Param
            //A mode of 3rd Param
            //instruction_ = 1002;
            //var list = IntegerToDigits(instruction_);
            //need to append a 0

            Tuple<OpCodes, ModuleCodes, ModuleCodes, ModuleCodes> oc = new Tuple<OpCodes, ModuleCodes, ModuleCodes, ModuleCodes>((OpCodes)instruction_[0],
                (ModuleCodes)instruction_[1],
                (ModuleCodes)instruction_[2],
                instruction_.Count >= 5 ? (ModuleCodes)instruction_[4] : ModuleCodes.positionMode);

            return oc;

        }

        public string ProcessOptCodeWithMode(ref int[] opCodeSeq, int instructionStart,ref int instructionLength)
        {
            try
            {
                //new route is ABCDE
                List<int> sequence = new List<int>();
                for (int idx = 0; idx < instructionLength; idx++)
                    sequence.Add(opCodeSeq[instructionStart + idx]);

                //int instruction = int.Parse($"{opCodeSeq[instructionStart]}{opCodeSeq[instructionStart + 1]}{opCodeSeq[instructionStart + 2]}{opCodeSeq[instructionStart + 3]}");
                //var InstructionRules = ProcessInstruction(sequence);
                //var digits = IntegerToDigits(instruction);
                //before we do the operation. We must work out what mode we are in.
                
                List<int> paramList = new List<int>();
                //1002,4,3,4 <- Example
                //Parameters that an instruction writes to will never be in immediate mode.
                //Need to work out what mode each value is in.
                //instruction 4 is the opcode.
                switch ((OpCodes)sequence[0])
                {
                    case OpCodes.add:
                        //you know that an add will need a certain number of parameters.
                        //Write to will never be an immediate value // then need to work out these two
                        // OpCode , Module, Module, Module.
                        opCodeSeq[opCodeSeq[instructionStart + 3]] = RetrieveValueBasedOnMode((ModuleCodes)sequence[2], opCodeSeq, instructionStart+1) + RetrieveValueBasedOnMode((ModuleCodes)sequence[3], opCodeSeq, instructionStart + 2);
                        instructionLength = 4;
                        break;
                    case OpCodes.times:
                        //opCodeSeq[opCodeSeq[instructionStart + 3]] = RetrieveValueBasedOnMode((ModuleCodes)opCodeSeq, opCodeSeq, instructionStart + 1) * RetrieveValueBasedOnMode((ModuleCodes)sequence[3], opCodeSeq, instructionStart + 2);
                        instructionLength = 4;
                        break;
                    case OpCodes.input:
                        Console.WriteLine("Enter an input");
                        int val = int.Parse(Console.ReadLine());
                        int position = opCodeSeq[instructionStart + 1];
                        opCodeSeq[position] = val;
                        instructionLength = 2;
                        break;
                    case OpCodes.output:
                        instructionLength = 2;
                        return opCodeSeq[opCodeSeq[instructionStart + 1]].ToString();

                    case OpCodes.halt:
                        return "";
                }
                return "";
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return "";
            }

        }

        private int RetrieveValueBasedOnMode(ModuleCodes mc,int[] opCodeSeq, int index)
        {
            switch (mc)
            {
                case ModuleCodes.immediateMode:
                    return opCodeSeq[index];
                case ModuleCodes.positionMode:
                    return opCodeSeq[opCodeSeq[index]];
                default:
                    return 0;
            }
        }

        private List<int> IntegerToDigits(int base_)
        {
            int temp = base_;
            List<int> digits = new List<int>();
            while (temp > 0)
            {
                digits.Add(temp % 10);
                temp /= 10;
            }
            digits.Reverse();
            return digits;
        }

    }
}
