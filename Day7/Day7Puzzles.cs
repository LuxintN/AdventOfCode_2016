using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utilities;

namespace Day7
{
    public class Day7Puzzles
    {
        private static readonly Regex insideSquareBracketsRegex = new Regex(@"\[\w+\]");
        
        static void Main()
        {
            var addresses = FileHelper.ReadLines(Environment.CurrentDirectory + @"\..\..\Input.txt");

            var addressesSupportingTlsCount = addresses.Count(CheckIfSupportsTls);
            Console.WriteLine(addressesSupportingTlsCount);

            var addressesSupportingSslCount = addresses.Count(CheckIfSupportsSsl);
            Console.WriteLine(addressesSupportingSslCount);

            Console.ReadLine();
        }
        
        public static bool CheckIfSupportsTls(string input)
        {
            return GetValuesOutsideSquareBrackets(input).Any(ContainsABBA) && GetValuesInsideSquareBrackets(input).All(x => !ContainsABBA(x));
        }

        public static bool CheckIfSupportsSsl(string input)
        {
            var abaValues = GetValuesOutsideSquareBrackets(input).SelectMany(GetABAValues).ToList();
            
            return abaValues.Count > 0 && GetValuesInsideSquareBrackets(input).Any(x => ContainsBAB(x, abaValues));
        }

        private static bool ContainsABBA(string input)
        {
            for (int i = 0; i < input.Length-3; i++)
            {
                var substring = input.Substring(i, 4);

                if (substring[0] == substring[3] && substring[1] == substring[2] && substring[0] != substring[1])
                {
                    return true;
                }
            }

            return false;
        }

        private static IEnumerable<string> GetABAValues(string input)
        {
            var abaValues = new HashSet<string>();

            for (int i = 0; i < input.Length - 2; i++)
            {
                var substring = input.Substring(i, 3);

                if (substring[0] == substring[2] && substring[0] != substring[1])
                {
                    abaValues.Add(substring);
                }
            }

            return abaValues;
        }

        private static bool ContainsBAB(string input, List<string> abaValues)
        {
            foreach (var abaValue in abaValues)
            {
                for (int i = 0; i < input.Length - 2; i++)
                {
                    var substring = input.Substring(i, 3);

                    if (substring[0] == abaValue[1] && substring[1] == abaValue[0] && substring[2] == abaValue[1])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private static IEnumerable<string> GetValuesOutsideSquareBrackets(string input)
        {
            return insideSquareBracketsRegex.Split(input);
        }

        private static IEnumerable<string> GetValuesInsideSquareBrackets(string input)
        {
            return insideSquareBracketsRegex.Matches(input).Cast<Match>().Select(m => m.Value.Substring(1, m.Value.Length - 2));
        }
    }
}
