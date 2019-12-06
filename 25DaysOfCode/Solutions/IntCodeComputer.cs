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

        public int[] IntCodeComputer()
        {
            //InitMemory();
            int jumpIndex = 0;
            while (ProcessOptCode(ref opCodeSequence,jumpIndex) != false || jumpIndex >= opCodeSequence.Length)
            {
                jumpIndex += 4;
            }

            return opCodeSequence;
        }

        public int[] IntCodeComputerWithMode()
        {
            //InitMemory();
            int currentPosition = 0;
            int instructionLength = 4;
            while (ProcessOptCodeWithMode(ref opCodeSequence, currentPosition, ref instructionLength) != false || currentPosition >= opCodeSequence.Length)
            {
                currentPosition += instructionLength;
            }

            return opCodeSequence;
        }

        public bool ProcessOptCode(ref int[] opCodeSeq,int instructionStart)
        {
            try
            {
                //new route is ABCDE
                //int instruction = int.Parse($"{opCodeSeq[instructionStart]}{opCodeSeq[instructionStart + 1]}{opCodeSeq[instructionStart + 2]}{opCodeSeq[instructionStart + 3]}");
                //var InstructionRules = ProcessInstruction(instruction);
                //before we do the operation. We must work out what mode we are in.
                switch ((OpCodes)opCodeSeq[instructionStart])
                {
                    case OpCodes.add:
                        opCodeSeq[opCodeSeq[instructionStart + 3]] = opCodeSeq[opCodeSeq[instructionStart + 1]] + opCodeSeq[opCodeSeq[instructionStart + 2]];
                        break;
                    case OpCodes.times:
                        opCodeSeq[opCodeSeq[instructionStart + 3]] = opCodeSeq[opCodeSeq[instructionStart + 1]] * opCodeSeq[opCodeSeq[instructionStart + 2]];
                        break;
                    case OpCodes.halt:
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

        public bool ProcessOptCodeWithMode(ref int[] opCodeSeq, int instructionStart,ref int instructionLength)
        {
            try
            {
                //new route is ABCDE
                List<int> sequence = new List<int>();
                for (int idx = 0; idx < instructionLength; idx++)
                    sequence.Add(opCodeSeq[instructionStart + idx]);

                //int instruction = int.Parse($"{opCodeSeq[instructionStart]}{opCodeSeq[instructionStart + 1]}{opCodeSeq[instructionStart + 2]}{opCodeSeq[instructionStart + 3]}");
                var InstructionRules = ProcessInstruction(sequence);
                //var digits = IntegerToDigits(instruction);
                //before we do the operation. We must work out what mode we are in.
                int position = 0;
                List<int> paramList = new List<int>();
                //1002,4,3,4 <- Example
                //Parameters that an instruction writes to will never be in immediate mode.
                //Need to work out what mode each value is in.
                //instruction 4 is the opcode.
                switch (InstructionRules.Item1)
                {
                    case OpCodes.add:
                        //Write to will never be an immediate value // then need to work out these two
                        opCodeSeq[opCodeSeq[instructionStart + 3]] = RetrieveValueBasedOnMode(InstructionRules.Item2,opCodeSeq, instructionStart+1) + RetrieveValueBasedOnMode(InstructionRules.Item3, opCodeSeq, instructionStart + 2);
                        break;
                    case OpCodes.times:
                        opCodeSeq[opCodeSeq[instructionStart + 3]] = RetrieveValueBasedOnMode(InstructionRules.Item2, opCodeSeq, instructionStart + 1) * RetrieveValueBasedOnMode(InstructionRules.Item3, opCodeSeq, instructionStart + 2);
                        break;
                    case OpCodes.input:
                        break;
                    case OpCodes.output:
                        break;
                    case OpCodes.halt:
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

        private int RetrieveValueBasedOnMode(ModuleCodes mc,int[] opCodeSeq, int index)
        {
            switch(mc)
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
