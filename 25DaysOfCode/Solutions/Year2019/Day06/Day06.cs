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
        Dictionary<string, List<string>> orbitMaps;
        public Day06() : base ("Universal Orbit Map",2019,6)
        {
            input = Input;
            orbitMaps = new Dictionary<string, List<string>>();
            PopulateOrbitMaps(input);
        }

        private void PopulateOrbitMaps(string[] input)
        {
            //AA)BB
            foreach(var map in input)
            {
                var planet = map.Split(')')[0];
                var orbiter = map.Split(')')[1];

                if(orbitMaps.TryGetValue(planet, out List<string> orbits))
                {
                    orbits.Add(orbiter);
                } else
                {
                    orbitMaps.Add(planet, new List<string> { orbiter });
                }
            }
        }

        protected override string SolvePartOne()
        {
            return CalculateSharedOrbits(orbitMaps).ToString();
            //return CalculateSharedOrbits(input).ToString();
        }

        protected override string SolvePartTwo()
        {
            return "";
        }

        public int CalculateSharedOrbits(Dictionary<string, List<string>> orbitMaps)
        {
            //foreach orbit map
            //foreach orbiter in the map
            //search for paths to that orbiter.
            int sum = 0;
            foreach(var parent in orbitMaps.Keys)
            {
                foreach(var orbiter in orbitMaps[parent])
                {
                    sum++;
                    sum += FindOrbits(orbiter);
                }
            }

            return sum;
        }

        public int FindOrbits(string planet)
        {
            int sum = 0;
            if (orbitMaps.TryGetValue(planet, out List<string> orbiters))
            {
                foreach (var orbiter in orbiters)
                {
                    sum++;
                    sum += FindOrbits(orbiter);
                }
            }
            return sum;
        }

        public List<string> FindPathToPlanet(Dictionary<string, List<string>> orbitMap, string planet)
        {
            //var paths = orbitMap.Values(x => x.Contains(planet));
            string me = "YOU";
            string santa = "SANTA";

            foreach (var reference in orbitMap.Keys)
            {
                
            }

            if(orbitMap.TryGetValue(planet, out List<string> orbiters))
            {

            }

            return new List<string>();
        }
    }
}
