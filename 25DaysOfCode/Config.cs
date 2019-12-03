using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _25DaysOfCode
{
   class Config {

        string _c; 
        int _y;
        int[] _d;

        public string Cookie { 
            get => _c;
            set {
                if(Regex.IsMatch(value, "^session=[a-z0-9]+$")) {
                    _c = value; 
                } else {
                    _c = "";
                }
            }
        }
        public int Year { 
            get => _y;
            set {
                if(value < 2015 || value > DateTime.Now.Year) {
                    _y = DateTime.Now.Year; 
                } else {
                    _y = value; 
                }
            } 
        }
        public int[] Days { 
            get => _d;
            set {
                bool allDaysCovered = false;
                _d = value.Where(v => {
                    if(v == 0) allDaysCovered = true; 
                    return v > 0 && v < 26;
                }).ToArray();

                if(allDaysCovered) {
                    _d = new int[]{0};
                } else {
                    Array.Sort(_d);
                }
            }
        }

        
        void setDefaults() {
            Cookie = ""; 
            Year = DateTime.Now.Year;
            Days = (DateTime.Now.Month == 12) ? new int[]{DateTime.Now.Day} : new int[]{0}; 
        }
    }
}
