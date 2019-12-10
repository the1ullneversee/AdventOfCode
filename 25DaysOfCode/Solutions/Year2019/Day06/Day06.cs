using System;
using System.Collections.Generic;
using System.IO;
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
            input = new string[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" };
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
            //return CalculateSharedOrbits(orbitMaps).ToString();
            //return CalculateSharedOrbits(input).ToString();
            return "";
        }

        protected override string SolvePartTwo()
        {

            return FindShortestPathToSanta(orbitMaps);
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

        StreamWriter sr;
        public List<string> ReturnOrbitPath(string planet, Dictionary<string, List<string>> orbitMap)
        {

            List<string> sum = new List<string>();

            var planetList = orbitMap.Where(x => x.Value.Contains(planet));
            if (planetList.Count() != 0)
            {
                sr.Write(planet + ",");
                sum.Add(planet);
                sum.AddRange(ReturnOrbitPath(planetList.First().Key, orbitMap));
            }
            
            return sum;
        }

        public string FindShortestPathToSanta(Dictionary<string, List<string>> orbitMap)
        {
            sr = new StreamWriter(Directory.GetCurrentDirectory() + "Output.txt");
            //var paths = orbitMap.Values(x => x.Contains(planet));
            string me = "YOU";
            string santa = "SAN";

            var meList = orbitMap.Where(x => x.Value.Contains(me));
            var santaList = orbitMap.Where(x => x.Value.Contains(santa));
            var parentPlanet = santaList.First().Key;


            var possibleOrbitsFromMe = ReturnOrbitPath(meList.First().Key,orbitMap);
            sr.WriteLine("\n");
            var possibleOrbitsFromSanta = ReturnOrbitPath(santaList.First().Key, orbitMap);
            sr.WriteLine("\n");
            var intersections = possibleOrbitsFromMe.Intersect(possibleOrbitsFromSanta);
            sr.Flush();
            int lowest = int.MaxValue;
            foreach(var interesection in intersections)
            {
                int indexFromMe = possibleOrbitsFromMe.IndexOf(interesection);
                int indexFromSanta = possibleOrbitsFromSanta.IndexOf(interesection);
                int sum = indexFromMe + indexFromSanta;
                sr.WriteLine($"{indexFromMe},{indexFromSanta}");
                if (sum < lowest)
                {
                    lowest = sum;
                }

            }
            //to take out the end orbit
            int lowestPathToSanta = lowest;



            sr.Close();

            return lowestPathToSanta.ToString();
        }
    }
}
