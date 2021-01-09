using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.ProgrammingGuide
{
    public class Tuples
    {
        public static void Example()
        {
            var result = QueryCityData("New York city");

            var city = result.Item1;
            var pop = result.Item2;
            var size = result.Item3;

            // Other ways
            var (a, b, c) = QueryCityData("New York city");
            (string a1, var b1, var c1) = QueryCityData("New York city");

            // Discard
            var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

            Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
            Console.WriteLine($"{city}: ({pop}, {size})");
            // Do something with the data.
        }
        private static (string, int, double) QueryCityData(string name)
        {
            if (String.Equals("New York City", name, StringComparison.OrdinalIgnoreCase))
                return (name.ToUpper(), 8175133, 468.48);

            return ("", 0, 0);
        }

        private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
        {
            int population1 = 0, population2 = 0;
            double area = 0;

            if (name == "New York City")
            {
                area = 468.48;
                if (year1 == 1960)
                {
                    population1 = 7781984;
                }
                if (year2 == 2010)
                {
                    population2 = 8175133;
                }
                return (name, area, year1, population1, year2, population2);
            }

            return ("", 0, 0, 0, 0, 0);
        }
    }
}
