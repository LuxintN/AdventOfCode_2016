using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4
{
    class Room
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
                    var shiftedChar = SectorId%AlphabetLetterCount + character;
                    decryptedName += (char) (shiftedChar <= 'z' ? shiftedChar : shiftedChar - AlphabetLetterCount);
                }
            }
            
            return decryptedName;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var rooms = LoadTestData();

            var rooms = LoadDataFromInputFile();

            int sectorSum = 0;

            foreach (var room in rooms)
            {
                var firstFiveMostCommonChars = string.Concat(room.EncryptedName.Replace("-", "")
                    .GroupBy(c => c)
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key) // if use counts are equal, sort in alphabetic order
                    .Take(5)
                    .Select(g => g.Key));

                if (firstFiveMostCommonChars == room.Checksum) sectorSum += room.SectorId;
            }

            Console.WriteLine(sectorSum);

            foreach (var room in rooms)
            {
                if (room.GetDecryptedName().Contains("northpole"))
                {
                    Console.WriteLine("North Pole room sector ID: " + room.SectorId);
                    break;
                }
            }

            SaveDecryptedRoomNamesToFile(rooms);

            Console.ReadLine();
        }

        private static List<Room> LoadTestData()
        {
            //// aaaaa-bbb-z-y-x-123[abxyz]
            //// a-b-c-d-e-f-g-h-987[abcde]
            //// not-a-real-room-404[oarel]

            return new List<Room>()
            {
                //new Room()
                //{
                //    EncryptedName = "aaaaa-bbb-z-y-x-",
                //    SectorId = 123,
                //    Checksum = "abxyz"
                //},
                //new Room()
                //{
                //    EncryptedName = "a-b-c-d-e-f-g-h-",
                //    SectorId = 987,
                //    Checksum = "abcde"
                //},
                //new Room()
                //{
                //    EncryptedName = "not-a-real-room-",
                //    SectorId = 404,
                //    Checksum = "oarel"
                //},
                new Room()
                {
                    EncryptedName = "qzmt-zixmtkozy-ivhz-", // "very encrypted name"
                    SectorId = 343,
                    Checksum = ""
                }
            };
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
