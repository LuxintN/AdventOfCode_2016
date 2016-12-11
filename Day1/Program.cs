using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    enum Direction
    {
        North,
        East,
        South,
        West
    }

    internal class Location
    {
        public int North { get; set; }
        public int East { get; set; }
    }

    class Program
    {
        private static List<Location> locations;
        private static bool firstLocationVisitedTwiceFound;

        static void Main(string[] args)
        {
            var instructionStrings = new[]
            {
                //"R2, L3",
                //"R2, R2, R2",
                //"R5, L5, R5, R3",
                "L4, R2, R4, L5, L3, L1, R4, R5, R1, R3, L3, L2, L2, R5, R1, L1, L2, R2, R2, L5, R5, R5, L2, R1, R2, L2, L4, L1, R5, R2, R1, R1, L2, L3, R2, L5, L186, L5, L3, R3, L5, R4, R2, L5, R1, R4, L1, L3, R3, R1, L1, R4, R2, L1, L4, R5, L1, R50, L4, R3, R78, R4, R2, L4, R3, L4, R4, L1, R5, L4, R1, L2, R3, L2, R5, R5, L4, L1, L2, R185, L5, R2, R1, L3, R4, L5, R2, R4, L3, R4, L2, L5, R1, R2, L2, L1, L2, R2, L2, R1, L5, L3, L4, L3, L4, L2, L5, L5, R2, L3, L4, R4, R4, R5, L4, L2, R4, L5, R3, R1, L1, R3, L2, R2, R1, R5, L4, R5, L3, R2, R3, R1, R4, L4, R1, R3, L5, L1, L3, R2, R1, R4, L4, R3, L3, R3, R2, L3, L3, R4, L2, R4, L3, L4, R5, R1, L1, R5, R3, R1, R3, R4, L1, R4, R3, R1, L5, L5, L4, R4, R3, L2, R1, R5, L3, R4, R5, L4, L5, R2",
                //"R3, L2, R4, L2, L3, L5", // N2 E4
                //"L2, R2, L4, L3, L2, L7", // N2 E-4
                //"L4, L3, L1, L1, L4",  // N-2 E-4
                //"L2, L2, L3, R2, R6, R3, R5"  // N-1 E-2 
            };

            foreach (var instructionString in instructionStrings)
            {
                locations = new List<Location> { new Location() {North = 0, East = 0} };
                firstLocationVisitedTwiceFound = false;

                Direction currentDirection = Direction.North;
                int northBlockCount = 0, eastBlockCount = 0;

                var totalBlockCount = FollowInstructions(instructionString, currentDirection, ref northBlockCount, ref eastBlockCount);

                Console.WriteLine("North: " + northBlockCount + "   East: " + eastBlockCount + "     total bloks: " + totalBlockCount);
            }

            Console.ReadLine();
        }

        private static int FollowInstructions(string instructionString, Direction currentDirection, ref int northBlockCount, ref int eastBlockCount)
        {
            var instructions = instructionString
                    .Split(',', ' ')
                    .Where(i => i != "")
                    .Select(i => new {Side = i[0], BlockCount = int.Parse(i.Substring(1))})
                    .ToList();

            foreach (var instruction in instructions)
            {
                currentDirection = GetNextDirection(instruction.Side, currentDirection);
                MoveInSpecifiedDirection(currentDirection, instruction.BlockCount, ref northBlockCount, ref eastBlockCount);
            }

            return GetTotalBlockCount(northBlockCount, eastBlockCount);
        }

        private static Direction GetNextDirection(char side, Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.North:
                    return side == 'L' ? Direction.West : Direction.East;
                case Direction.East:
                    return side == 'L' ? Direction.North : Direction.South;
                case Direction.South:
                    return side == 'L' ? Direction.East : Direction.West;
                case Direction.West:
                    return side == 'L' ? Direction.South : Direction.North;
                default:
                    return currentDirection;
            }
        }
        
        private static void MoveInSpecifiedDirection(Direction currentDirection, int blockCount, ref int northBlockCount, ref int eastBlockCount)
        {
            for (int i = 0; i < blockCount; i++)
            {
                switch (currentDirection)
                {
                    case Direction.North:
                        northBlockCount++;
                        break;
                    case Direction.East:
                        eastBlockCount++;
                        break;
                    case Direction.South:
                        northBlockCount--;
                        break;
                    case Direction.West:
                        eastBlockCount--;
                        break;
                }

                if (!firstLocationVisitedTwiceFound)
                {
                    locations.Add(new Location() { North = northBlockCount, East = eastBlockCount });
                    CheckForLocationVisitedTwice(northBlockCount, eastBlockCount);
                }
            }
        }

        private static int GetTotalBlockCount(int northBlockCount, int eastBlockCount)
        {
            return Math.Abs(northBlockCount) + Math.Abs(eastBlockCount);
        }

        private static void CheckForLocationVisitedTwice(int northBlockCount, int eastBlockCount)
        {
            if (locations.Count < 4)
            {
                return;
            }

            for (int i = 0; i < locations.Count; i++)
            {
                for (int j = i+1; j < locations.Count; j++)
                {
                    if (locations[i].North == locations[j].North  &&  locations[i].East == locations[j].East)
                    {
                        Console.WriteLine("Found the first location visited twice - North: " + locations[j].North + " East: " + locations[j].East + "   total blocks: " + GetTotalBlockCount(northBlockCount, eastBlockCount));
                        firstLocationVisitedTwiceFound = true;
                        return;
                    }
                }
            }
        }

    }

}
