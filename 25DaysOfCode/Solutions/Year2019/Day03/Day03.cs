using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _25DaysOfCode.Solutions.Year2019.Day03
{
    class Day03 : AVC
    {
        string line1= null;
        string line2 = null;
        DateTime start;

        public Day03() : base("Crossed Wires", 2019, 3)
        {
            line1 = Input[0];
            line2 = Input[1];
            //masses = Input.Select(s => int.Parse(s)).ToArray();
        }

        protected override string SolvePartOne()
        {
            return WorkOutManhattanDistanceBetweenTwoLines(line1, line2);
        }

        protected override string SolvePartTwo()
        {
            return "";
        }

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
                int movementAmount = int.Parse(movement.Substring(1, movement.Length -1));
                int idx = 1;
                while (idx <= movementAmount)
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
    }
}
