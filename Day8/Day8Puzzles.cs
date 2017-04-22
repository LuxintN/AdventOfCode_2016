using System;
using Utilities;

namespace Day8
{
    public class Day8Puzzles
    {
        static void Main()
        {
            var commands = FileHelper.ReadLines(Environment.CurrentDirectory + @"\..\..\Input.txt");

            var display = new Display(6, 50);

            foreach (var command in commands)
            {
                display.ExecuteCommand(command);
            }

            Console.WriteLine(display.GetLitPixelsCount());
            Console.WriteLine(display);

            Console.ReadLine();
        }
    }
}
