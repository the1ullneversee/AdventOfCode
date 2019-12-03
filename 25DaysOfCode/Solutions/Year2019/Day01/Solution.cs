using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode
{
    class Day01 : AVC
    {
        int[] masses = null;
        DateTime start;

        public Day01() : base("The Tyranny of the Rocket Equation", 2019, 1)
        {
            masses = Input.Select(s => int.Parse(s)).ToArray();
        }

        

        //public static int GetTotalFuelRequirement()
        //{
        //    int fuelReqForFuel = 0;
        //    List<int> totals = new List<int>();
        //    int totalForAllModules = 0;
        //    using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\Resources\Day1Test.txt"))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            //get the mass.
        //            int mass = int.Parse(sr.ReadLine());
        //            //get the fuel requirement for the extra fuel for that mass.
        //            fuelReqForFuel = FuelRequirementForTheFuel(mass, fuelReqForFuel);

        //            //Store.
        //            totals.Add(fuelReqForFuel);
        //            fuelReqForFuel = 0;
        //        }
        //        //Add up all fuel totals'
        //        foreach (int val in totals)
        //            totalForAllModules += val;
        //    }
        //    return totalForAllModules;
        //}

        public static int FuelRequirementForTheFuel(int m)
        {
            //take its mass, divide by three, round down, and subtract 2
            //first is to divide by 3. Using an int automatically just droups the remainder.
            int fuel = FuelRequirement(m);
            if (fuel <= 0)
            {
                return fuel;
            }
            else
            {
                return fuel + FuelRequirementForTheFuel(fuel);
            }

        }

        public static int FuelRequirement(int mass_)
        {
            //take its mass, divide by three, round down, and subtract 2
            //first is to divide by 3. Using an int automatically just droups the remainder.
            int result = mass_ / 3;
            result -= 2;

            return result;
        }

        protected override string SolvePartOne()
        {
            return masses.Select(m => FuelRequirement(m)).Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            return masses.Select(m => FuelRequirementForTheFuel(m)).Sum().ToString();
        }
    }
}
