using System;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    public class Day8Puzzles
    {
        static void Main(string[] args)
        {
            var commands = LoadDataFromInputFile();

            var display = new Display(6, 50);

            foreach (var command in commands)
            {
                display.ExecuteCommand(command);
            }

            Console.WriteLine(display.GetLitPixelsCount());
            Console.WriteLine(display);

            Console.ReadLine();
        }

        private static List<string> LoadDataFromInputFile()
        {
            var lines = new List<string>();

            using (var streamReader = new StreamReader(Environment.CurrentDirectory + @"\..\..\Input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }

            return lines;
        }
    }
}
