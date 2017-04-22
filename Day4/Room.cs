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
}