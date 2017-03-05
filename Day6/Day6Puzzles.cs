using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    public class Day6Puzzles
    {
        static void Main(string[] args)
        {
            var input = LoadDataFromInputFile();

            Console.WriteLine(GetMessage(input, true));
            Console.WriteLine(GetMessage(input, false));

            Console.ReadLine();
        }

        public static string GetMessage(string input, bool selectMostCommonCharacter)
        {
            var columns = SplitIntoColumns(input);

            if (columns.Count == 0 || columns.Any(c => c.Length != columns.First().Length)) 
                throw new ArgumentException("Column list is empty or columns have unequal lengths");

            var message = string.Concat(
                columns.Select(c => c.ToList())
                    .Select(
                        c => 
                            selectMostCommonCharacter
                                ? c.GroupBy(ch => ch).OrderByDescending(g => g.Count()).First().Key
                                : c.GroupBy(ch => ch).OrderByDescending(g => g.Count()).Last().Key));
            
            return message;
        }

        private static List<string> SplitIntoColumns(string input)
        {
            var columns = new List<string>();

            using (var stringReader = new StringReader(input))
            {
                string row;

                while ((row = stringReader.ReadLine()) != null)
                {
                    row = row.Trim();

                    if (columns.Count < row.Length)
                    {
                        var lengthDifference = row.Length - columns.Count;
                        for (int i = 0; i < lengthDifference; i++)
                        {
                            columns.Add(string.Empty);
                        }
                    }

                    for (int i = 0; i < row.Length; i++)
                    {
                        columns[i] += row[i];
                    }
                }
            }

            return columns;
        }

        private static string LoadDataFromInputFile()
        {
            using (var streamReader = new StreamReader(Environment.CurrentDirectory + @"\..\..\Input.txt"))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
