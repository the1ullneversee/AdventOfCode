using _25DaysOfCode.Solutions;
using _25DaysOfCode.Solutions.Year2019.Day04;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _25DaysOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int day = 2;
            string type = $"_25DaysOfCode.Solutions.Year{2019}.Day{day.ToString("D2")}.Day{day.ToString("D2")}";
            var solution = Type.GetType(type);
            AVC avc = (AVC)Activator.CreateInstance(solution);
            avc.Solve();

        }


    }



    public static class AdventOfCode
    {

        private static string WorkOutManhattanDistanceBetweenTwoLines(string line1, string line2)
        {
            //work out the transformation from the given input movements, store each transformation.
            List<Point> CoordianteListL1 = TransformMovementList(line1);
            List<Point> CoordianteListL2 = TransformMovementList(line2);

            Point start = new Point(0, 0);
            int lowestDistance = int.MaxValue;
            foreach (var interesct in CoordianteListL1.Intersect(CoordianteListL2))
            {

                //(q,p) = | p - q | 
                var distance = Math.Abs(start.X - interesct.X) + Math.Abs(start.Y - interesct.Y);
                if (distance < lowestDistance)
                {
                    lowestDistance = (int)distance;
                }
                Console.Write($"[{interesct.X},{interesct.Y}] ");
                Console.Write($"Distance is [{distance}] ");
            }
            return lowestDistance.ToString();
        }


        private static List<Point> TransformMovementList(string movements)
        {
            List<Point> tempCoordinateList = new List<Point>();
            Point v1 = new Point(0, 0);
            foreach (var movement in movements.Split(','))
            {
                char direction = movement.Substring(0, 1).ToCharArray()[0];
                int movementAmount = int.Parse(movement.Substring(1, 1));
                int idx = 1;
                while(idx <= movementAmount)
                {
                    switch (direction)
                    {
                        case 'R':
                            v1.X += 1;
                            break;
                        case 'U':
                            v1.Y += 1;
                            break;
                        case 'L':
                            v1.X -= 1;
                            break;
                        case 'D':
                            v1.Y -= 1;
                            break;
                    }
                    tempCoordinateList.Add(new Point(v1.X, v1.Y));
                    ++idx;
                }
            }
            return tempCoordinateList;
        }

        #region Day1

        #endregion

        #region Day2
        //99 means that the program is finished and should immediately halt. Encountering an unknown opcode means something went wrong.
        //Opcode 1 adds together numbers read from two positions and stores the result in a third position
        //Opcode 2 works exactly like opcode 1, except it multiplies the two inputs
        //Hopes 4 along each time.
        public enum opCodes { add = 1, times = 2, halt = 99 }

        public static int[] IntCodeComputer(int[] opCodeSeq)
        {
            int jumpIndex = 0;
            while (ProcessOptCode(ref opCodeSeq, jumpIndex) != false || jumpIndex >= opCodeSeq.Length)
            {
                jumpIndex += 4;
            }

            return opCodeSeq;
        }

        public static bool ProcessOptCode(ref int[] opCodeSeq, int posStart)
        {
            try
            {
                switch ((opCodes)opCodeSeq[posStart])
                {
                    case opCodes.add:
                        opCodeSeq[opCodeSeq[posStart + 3]] = opCodeSeq[opCodeSeq[posStart + 1]] + opCodeSeq[opCodeSeq[posStart + 2]];
                        break;
                    case opCodes.times:
                        opCodeSeq[opCodeSeq[posStart + 3]] = opCodeSeq[opCodeSeq[posStart + 1]] * opCodeSeq[opCodeSeq[posStart + 2]];
                        break;
                    case opCodes.halt:
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

        public static int[] LineToArray(StreamReader sr)
        {
            var line = sr.ReadLine();
            var lineArray = line.Split(',');
            int[] array = new int[lineArray.Length];
            int index = 0;
            foreach (var str in lineArray)
            {
                array[index] = int.Parse(str);
                index++;
            }

            return array;
        }

        //The inputs should still be provided to the program by replacing the values at addresses 1 and 2, 
        //just like before.In this program, the value placed in address 1 is called the noun, and the value placed in address 2 is called the verb.
        //Each of the two input values will be between 0 and 99, inclusive.

        public static Tuple<int, int> FindStartingPair(int product)
        {
            int verb = 1;
            int max = 99;

            int noun = max / 2;
            bool found = false;
            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\Resources\Day2Test.txt"))
                    {
                        int[] opCodeSeq = LineToArray(sr);
                        opCodeSeq[1] = i;
                        opCodeSeq[2] = j;
                        var output = AdventOfCode.IntCodeComputer(opCodeSeq);
                        if (output[0] == product)
                        {
                            verb = j;
                            found = true;
                            break;
                        }
                    }
                }
                if (found)
                {
                    noun = i;
                    break;
                }

            }
            return new Tuple<int, int>(noun, verb);
        }
        #endregion


    }

}
