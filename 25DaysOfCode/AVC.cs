using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _25DaysOfCode
{
    /// <summary>
    /// base class for all solutions that will contain the basics that each one needs.
    /// </summary>
    public abstract class AVC
    {
        /// <summary>
        /// Lazy strings allow for slow loading modules, IE a different threads can come in and load them.
        /// </summary>
        Lazy<string> _part1, _part2;
        Lazy<string[]> _input;
        private string url = "https://adventofcode.com";

        //Use these to track statistics.
        DateTime start, stop;

        public string Name
        {
            get; set;
        }

        public int Year
        {
            get; set;
        }

        public int Day
        {
            get; set;
        }

        public string[] Input => _input.Value;
        public string Part1 => string.IsNullOrEmpty(_part1.Value) ? "" : _part1.Value;
        public string Part2 => string.IsNullOrEmpty(_part2.Value) ? "" : _part2.Value;

        

        private protected AVC(string name, int year, int day)
        {
            Name = name;
            Year = year;
            Day = day;
            _input = new Lazy<string[]>(() => GetInput());
            _part1 = new Lazy<string>(() => SolvePartOne());
            _part2 = new Lazy<string>(() => SolvePartTwo());
        }

        public void Solve(int part = 0)
        {
            start = DateTime.Now;
            if (_input == null) return;

            bool doOutput = false;
            string output = $"--- Day {Day}: {Name} --- \n";



            if(part != 2)
            {
                if(Part1 != "")
                {
                    output += $"Part 1: {Part1}\n";
                    doOutput = true;
                }
                else
                {
                    output += $"Part 1: Unsolved\n";
                    if (part == 1) doOutput = true;
                }
            }
            if (part != 1)
            {
                if (Part2 != "")
                {
                    output += $"Part 2: {Part2}";
                    doOutput = true;
                }
                else
                {
                    output += "Part 2: Unsolved";
                    if (part == 2) doOutput = true;
                }
            }
            stop = DateTime.Now;
            if(doOutput)
            {
                Console.WriteLine(output);
                Console.WriteLine("Solved in: " + (stop - start).TotalMilliseconds + "ms" + Environment.NewLine);
            }
        }
        

        public string[] GetInput()
        {
            string inputFilePath = $"./Solutions/Year{Year}/Day{Day.ToString("D2")}/input";
            string dayFolder = $"./Solutions/Year{Year}/Day{Day.ToString("D2")}";
            string[] input = null;
            if (File.Exists(inputFilePath))
            {
                input = File.ReadAllLines(inputFilePath);
            }
            else
            {
                if (!Directory.Exists(dayFolder))
                {
                    Directory.CreateDirectory(dayFolder);
                }

                using (WebClient wc = new WebClient())
                {
                    //wc.DownloadFile($"{url}/{Year}/Day/{Day}/input", "input.txt");
                    wc.Headers.Add(HttpRequestHeader.Cookie, "session=53616c7465645f5fac74322b34830eb6230c619690c5a481d537abaae55908a1e4facdf163055e4f55b136b881cbb780");
                    string URL = $"{url}/{Year}/day/{Day.ToString()}/input";
                    File.WriteAllText(inputFilePath, wc.DownloadString(URL).Trim());
                    input = File.ReadAllLines(inputFilePath);

                }
            }
            return input;
        }

        protected abstract string SolvePartOne();
        protected abstract string SolvePartTwo();
        //Crate the project area, meaning create the test files, test area, get the test input etc...

    }
}
