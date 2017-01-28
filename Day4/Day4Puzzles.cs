using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Day4
{
    public class Room
    {
        public string EncryptedName { get; set; }
        public int SectorId { get; set; }
        public string Checksum { get; set; }

        private const int AlphabetLetterCount = 26;

        public string GetDecryptedName()
        {
            string decryptedName = "";

            foreach (var character in EncryptedName)
            {
                if (character == '-')
                {
                    decryptedName += " ";
                }
                else
                {
                    var shiftedChar = SectorId % AlphabetLetterCount + character;
                    decryptedName += (char) (shiftedChar <= 'z' ? shiftedChar : shiftedChar - AlphabetLetterCount);
                }
            }
            
            return decryptedName;
        }
    }

    public class Day4Puzzles
    {
        static void Main(string[] args)
        {
            var rooms = LoadDataFromInputFile();

            Console.WriteLine(GetRealRoomsSectorSum(rooms));

            foreach (var room in rooms)
            {
                if (room.GetDecryptedName().Contains("northpole"))
                {
                    Console.WriteLine("North Pole room sector ID: " + room.SectorId);
                    break;
                }
            }

            //SaveDecryptedRoomNamesToFile(rooms); // just for fun :D

            Console.ReadLine();
        }

        public static int GetRealRoomsSectorSum(List<Room> rooms)
        {
            int sectorSum = 0;

            foreach (var room in rooms)
            {
                if (GetFiveMostCommonLetters(room.EncryptedName) == room.Checksum)
                {
                    sectorSum += room.SectorId;
                }
            }

            return sectorSum;
        }

        public static string GetFiveMostCommonLetters(string name)
        {
            return string.Concat(name.Replace("-", "")
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key) // if use counts are equal, sort in alphabetic order
                .Take(5)
                .Select(g => g.Key));
        }

        private static List<Room> LoadDataFromInputFile()
        {
            var rooms = new List<Room>();

            // aaaaa-bbb-z-y-x-123[abxyz]
            Regex encryptedNameRegex = new Regex(@"((\w+)(?:-))+"); // aaaaa-bbb-z-y-x-
            Regex sectorRegex = new Regex(@"\d+"); // 123
            Regex checksumRegex = new Regex(@"(?<=\[)\w+(?=\])"); // abxyz

            using (StreamReader streamReader = new StreamReader(Environment.CurrentDirectory + @"\..\..\Input.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();

                    if (line == null) throw new Exception("Empty line in input");

                    rooms.Add(new Room()
                    {
                        EncryptedName = encryptedNameRegex.Match(line).Value,
                        SectorId = int.Parse(sectorRegex.Match(line).Value),
                        Checksum = checksumRegex.Match(line).Value
                    });
                }
            }

            return rooms;
        }

        private static void SaveDecryptedRoomNamesToFile(List<Room> rooms)
        {
            using (var streamWriter = new StreamWriter(Environment.CurrentDirectory + @"\..\..\DecryptedRoomNames.txt"))
            {
                foreach (var room in rooms)
                {
                    streamWriter.WriteLine(room.GetDecryptedName());
                }
            }
        }
    }
}
