using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = LoadDataFromInputFile();

            Console.WriteLine(GetPossibleTriangleCountByRows(input));  // 869
            Console.WriteLine(GetPossibleTriangleCountByColumns(input)); //1544
            
            Console.ReadLine();
        }

        private static List<List<int>> LoadDataFromInputFile()
        {
            var input = new List<List<int>>();

            using (var streamReader = new StreamReader(Environment.CurrentDirectory + @"\..\..\Input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();

                    if (line != null)
                    {
                        var triangle =
                            line.Split(' ').Where(i => !string.IsNullOrWhiteSpace(i)).Select(int.Parse).ToList();

                        if (triangle.Count != 3)
                            throw new InvalidOperationException("Incorrect number of sides - expected '3' but was '" +
                                                                triangle.Count + "'");

                        input.Add(triangle);
                    }
                }
            }

            return input;
        }

        private static int GetPossibleTriangleCountByRows(List<List<int>> input)
        {
            return input.Count(IsPossibleTriangle);
        }

        private static int GetPossibleTriangleCountByColumns(List<List<int>> input)
        {
            int possibleTriangles = 0;

            for (int column = 0; column < 3; column++)
            {
                possibleTriangles += GetNextTriangle(input, column).Count(IsPossibleTriangle);
            }

            return possibleTriangles;
        }

        private static IEnumerable<List<int>> GetNextTriangle(List<List<int>> input, int column)
        {
            for (int i = 0; i < input.Count - 2; i += 3)
            {
                yield return new List<int>()
                {
                    input[i][column], 
                    input[i+1][column], 
                    input[i+2][column]
                };
            }
        }
        
        private static bool IsPossibleTriangle(List<int> triangle)
        {
            return triangle[0] + triangle[1] > triangle[2]
                   && triangle[0] + triangle[2] > triangle[1]
                   && triangle[1] + triangle[2] > triangle[0];
        }

    }
}
