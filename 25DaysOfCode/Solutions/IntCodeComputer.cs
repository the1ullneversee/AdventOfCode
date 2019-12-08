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
        public enum OpCodes { add = 01, times = 02, halt = 99 , input = 03, output = 04, jumpIfTrue = 05, jumpIfFalse = 06, isLessThan = 07, isEquals = 08 };
        public enum ModuleCodes { positionMode = 0, immediateMode = 1}
        public int[] opCodeSequence;
        int[] MasterSequence;
        public int manualInput;
        public ICC(string[] Input_)
        {
            MasterSequence = Input_[0].Split(',').Select(int.Parse).ToArray();
            //MasterSequence = new int[]{
            //   3,3,1105,-1,9,1101,0,0,12,4,12,99,1};
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
            bool haltOrBadOutput = false;
            while (jumpIndex <= opCodeSequence.Length)
            {
                haltOrBadOutput = ProcessOptCode(ref opCodeSequence, ref jumpIndex, ref output);
                int idx = 0;
                //Debug.Write($"After: ");
                //foreach (var num in opCodeSequence)
                //    Console.Write($"[{idx++},{num}]");
                //Debug.Write("\n");
                if (haltOrBadOutput)
                    break;
                //output = ProcessOptCode(ref opCodeSequence, jumpIndex);
                //jumpIndex += 4;
            }
            //Console.WriteLine(output);
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

        public bool ProcessOptCode(ref int[] opCodeSeq,ref int instructionStart, ref string output)
        {
            try
            {
                //new route is ABCDE
                //before we do the operation. We must work out what mode we are in.
                int pos1, pos2, pos3;
                int sequenceSize = opCodeSeq.Length - 1;
                pos3 = (instructionStart + 3) <= sequenceSize ? opCodeSeq[instructionStart + 3] : 0;
                pos2 = instructionStart + 2 <= sequenceSize ? opCodeSeq[instructionStart + 2] : 0;
                pos1 = instructionStart + 1 <= sequenceSize ? opCodeSeq[instructionStart + 1] : 0;
                var opCodeAndParamModeList = ProcessInstruction(opCodeSeq[instructionStart]);
                int val1, val2, val3;

                switch (opCodeAndParamModeList.Item1)
                {
                    case OpCodes.add:
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        val2 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                        val3 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[2], opCodeSeq, pos3);
                        //moves by 4 params.
                        //Work out what the mode is, then perform the operation.
                        opCodeSeq[pos3] = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1) + RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                        instructionStart += 4;
                        break;
                    case OpCodes.times:
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        val2 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                        val3 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[2], opCodeSeq, pos3);
                        //moves by 4 params
                        opCodeSeq[pos3] = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1) * RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                        instructionStart += 4;
                        break;
                    case OpCodes.input:
                        opCodeSeq[pos1] = manualInput;
                        instructionStart += 2;
                        break;
                    case OpCodes.output:
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        output += val1.ToString();
                        instructionStart += 2;
                        break;
                    case OpCodes.jumpIfTrue:
                        //Opcode 5 is jump-if-true: if the first parameter is non-zero,
                        //it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        if (Math.Abs(val1) > 0)
                        {
                            val2 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                            instructionStart = val2;

                        }
                        else
                        {
                            instructionStart += 3;
                        }
                        break;
                    case OpCodes.jumpIfFalse:
                        //Opcode 6 is jump-if-false: if the first parameter is zero,
                        //it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        if (val1 == 0)
                        {
                            val2 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                            instructionStart = val2;
                        }
                        else
                        {
                            instructionStart += 3;
                        }
                        break;
                    case OpCodes.isLessThan:
                        //Opcode 7 is less than: if the first parameter is less than the second parameter, 
                        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        val2 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                        val3 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[2], opCodeSeq, pos3);
                        opCodeSeq[pos3] = val1 < val2 ? 1 : 0;
                        instructionStart += 4;
                        break;
                    case OpCodes.isEquals:
                        //Opcode 8 is equals: if the first parameter is equal to the second parameter,
                        //it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                        val1 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[0], opCodeSeq, pos1);
                        val2 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[1], opCodeSeq, pos2);
                        val3 = RetrieveValueBasedOnMode(opCodeAndParamModeList.Item2[2], opCodeSeq, pos3);
                        opCodeSeq[pos3] = val1 == val2 ? 1 : 0;
                        instructionStart += 4;
                        break;
                    case OpCodes.halt:
                        return true;
                }
               
                return false;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }

        }

        public Tuple<OpCodes, List<ModuleCodes>> ProcessInstruction(int instruction_)
        {
            //insturction should be in the format of ABCDE. But in digits.
            //DE is the two digit OpCode
            //C mode of 1st Param
            //B mode of 2nd Param
            //A mode of 3rd Param
            //instruction_ = 1002;
            //first 2 digits are the opcode.
            var list = IntegerToDigits(instruction_);
            int count = list.Count();
            var secondDigit = list[count-1] == 9 ? list[count-2].ToString() : "";
            var opCode = (OpCodes)int.Parse($"{list[count-1]}{secondDigit}");
            //if no value is provided, then the param mode should be zero
            var moduleList = new List<ModuleCodes>() { 0, 0, 0 };
            int counter = 0;
            list.Reverse();
            for (int idx = 2; idx < list.Count; idx++) {
                moduleList[counter] = (ModuleCodes)list[idx];
                counter++;
            }
            Tuple<OpCodes, List<ModuleCodes>> oc = new Tuple<OpCodes, List<ModuleCodes>>(opCode, new List<ModuleCodes>(moduleList));
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
                    return index;
                case ModuleCodes.positionMode:
                    return opCodeSeq[index];
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
