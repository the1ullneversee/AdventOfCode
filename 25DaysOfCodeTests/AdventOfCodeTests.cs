using Microsoft.VisualStudio.TestTools.UnitTesting;
using _25DaysOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace _25DaysOfCode.Tests
{
    [TestClass()]
    public class AdventOfCodeTests
    {
        [TestMethod()]
        public void FuelRequirementTest()
        {
            //int[] masses = { 12, 14, 1969, 100756 };
            //int[] expectedFuelReq = { 2, 2, 654, 33583 };
            ////Work out the fuel requirement.
            //int counter = 0;
            //foreach (int mass in masses)
            //{
            //    int result = AdventOfCode.FuelRequirement(mass);
            //    Debug.WriteLine($"With mass {mass} fuel req is {result} should be {expectedFuelReq[counter]}");
            //    Assert.AreEqual(expectedFuelReq[counter], result);
            //    counter++;
            //}


        }

        [TestMethod()]
        public void FuelRequirementForTheFuelTest()
        {
            ////A module of mass 14 requires 2 fuel.This fuel requires no further fuel(2 divided by 3 and rounded down is 0, which would call for a negative fuel), so the total fuel required is still just 2.
            ////At first, a module of mass 1969 requires 654 fuel.Then, this fuel requires 216 more fuel(654 / 3 - 2). 216 then requires 70 more fuel, which requires 21 fuel, which requires 5 fuel, which requires no further fuel.So, the total fuel required for a module of mass 1969 is 654 + 216 + 70 + 21 + 5 = 966.
            ////The fuel required by a module of mass 100756 and its fuel is: 33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2 = 50346.
            //int[] masses = { 12, 14, 1969, 100756 };
            //int[] expectedFuelReq = { 2, 2, 966, 50346 };
            //int counter = 0;
            //foreach (int mass in masses)
            //{
            //    int fuelReq = 0;
            //    fuelReq = AdventOfCode.FuelRequirementForTheFuel(mass, fuelReq);
            //    Debug.WriteLine($"With mass {mass} fuel req is {fuelReq} should be {expectedFuelReq[counter]}");
            //    Assert.AreEqual(expectedFuelReq[counter], fuelReq);
            //    counter++;
            //}
        }

        [TestMethod()]
        public void ManhattanDistance()
        {
            string[] line1 = { "R8", "U5", "L5", "D3" };
            string[] line2 = { "U7", "R6", "D4", "L4" };
            
            //work out the transformation from the given input movements, store each transformation.
            List<Vector> CoordianteListL1 = TransformMovementList(line1);
            List<Vector> CoordianteListL2 = TransformMovementList(line2);
            foreach (var vec in CoordianteListL1)
                Debug.Write($"[{vec.X},{vec.Y}] ");
            foreach (var vec in CoordianteListL2)
                Debug.Write($"[{vec.X},{vec.Y}] ");
            //for (int idx = 0; idx < line1.Length; ++idx)
            //{
            //    distance += Math.Abs(line1[idx] - line2[idx]);
            //}
        }

        private static List<Vector> TransformMovementList(string[] movements)
        {
            List<Vector> tempCoordinateList = new List<Vector>();
            Vector v1 = new Vector(0, 0);
            foreach (var movement in movements)
            {
                char direction = movement.Substring(0, 1).ToCharArray()[0];
                int movementAmount = int.Parse(movement.Substring(1, 1));
                switch (direction)
                {
                    case 'R':
                        v1.X += movementAmount;
                        break;
                    case 'U':
                        v1.Y += movementAmount;
                        break;
                    case 'L':
                        v1.X -= movementAmount;
                        break;
                    case 'D':
                        v1.Y -= movementAmount;
                        break;
                }
                tempCoordinateList.Add(new Vector(v1.X, v1.Y));
            }
            return tempCoordinateList;
        }

        [TestMethod()]
        public void IntCodeComputerTest()
        {
            int[] opCodeSeq;
            int[] resOpCodeSeq;
            //first read the input data and create the op sequence.
            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\Resources\Day2Test.txt"))
            {
                while (!sr.EndOfStream)
                {
                    opCodeSeq = AdventOfCode.LineToArray(sr);
                    resOpCodeSeq = AdventOfCode.LineToArray(sr);
                    var output = AdventOfCode.IntCodeComputer(opCodeSeq);
                    for(int idx = 0; idx < opCodeSeq.Length; idx++)
                    {
                        Assert.AreEqual(resOpCodeSeq[idx], output[idx]);
                    }
                }
            }
            
        }

        
    }
}