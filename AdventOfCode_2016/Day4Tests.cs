using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using Day4;

namespace UnitTests
{
    // aaaaa-bbb-z-y-x-123[abxyz]
    // a-b-c-d-e-f-g-h-987[abcde]
    // not-a-real-room-404[oarel]
    // totally-real-room-200[decoy]
    
    public class Day4Tests
    {
        static List<Room> rooms = new List<Room>()
        {
            new Room() // real room
            {
                EncryptedName = "aaaaa-bbb-z-y-x-",
                SectorId = 123,
                Checksum = "abxyz"
            },
            new Room() // real room
            {
                EncryptedName = "a-b-c-d-e-f-g-h-",
                SectorId = 987,
                Checksum = "abcde"
            },
            new Room() // real room
            {
                EncryptedName = "not-a-real-room-",
                SectorId = 404,
                Checksum = "oarel"
            },
            new Room() // decoy
            {
                EncryptedName = "totally-real-room-",
                SectorId = 200,
                Checksum = "decoy"
            },
            new Room() // decoy
            {
                EncryptedName = "qzmt-zixmtkozy-ivhz-",
                SectorId = 343,
                Checksum = ""
            }
        };
        
        public static IEnumerable GetFiveMostCommonLettersTestsCases
        {
            get
            {
                yield return new TestCaseData(rooms[0], true);
                yield return new TestCaseData(rooms[1], true);
                yield return new TestCaseData(rooms[2], true);
                yield return new TestCaseData(rooms[3], false);
                yield return new TestCaseData(rooms[4], false);
            }
        }

        [Test, TestCaseSource("GetFiveMostCommonLettersTestsCases")]
        public void GetFiveMostCommonLetters_ShouldReturnCorrectValue(Room room, bool isRoomRealExpectedValue)
        {
            var fiveMostCommonLetters = Day4Puzzles.GetFiveMostCommonLetters(room.EncryptedName);
            Assert.AreEqual(isRoomRealExpectedValue, fiveMostCommonLetters == room.Checksum);
        }

        [Test]
        public void GetRealRoomsSectorSum_ShouldReturnCorrectValue()
        {
            var realRoomsSectorSum = Day4Puzzles.GetRealRoomsSectorSum(rooms);
            Assert.AreEqual(rooms[0].SectorId + rooms[1].SectorId + rooms[2].SectorId, realRoomsSectorSum);
        }

        [Test]
        public void Room_GetDecryptedName_ShouldReturnCorrectValue()
        {
            Assert.AreEqual("very encrypted name ", rooms[4].GetDecryptedName());
        }

    }
}
