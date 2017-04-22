using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Day5
{
    public class Day5Puzzles
    {
        static void Main()
        {
            var input = "wtnhxymk";

            string password1, password2;
            GetPasswords(input, out password1, out password2);
            
            Console.WriteLine(password1.ToLower() + "       " + password2);

            Console.ReadLine();
        }

        public static void GetPasswords(string input, out string password1, out string password2)
        {
            password1 = "";
            var password2Array = new char[8];

            using (var md5 = MD5.Create())
            {
                int attempt = 0;

                while (password1.Length < 8 || password2Array.Any(c => c == '\0'))
                {
                    var inputWithNumber = input + attempt++;
                    var hexadecimalHash = GetHexadecimalHash(md5, inputWithNumber);

                    if (DoesHashStartWithFiveZeroes(hexadecimalHash))
                    {
                        if (password1.Length < 8) password1 += hexadecimalHash[5];

                        int position;
                        if (int.TryParse(hexadecimalHash[5].ToString(), out position) && position < 8 && password2Array[position] == '\0')
                        {
                            password2Array[position] = hexadecimalHash[6];
                        }
                    }
                }
            }

            password2 = string.Concat(password2Array);
        }

        private static bool DoesHashStartWithFiveZeroes(string hexadecimalHash)
        {
            for (int i = 0; i < 5; i++)
            {
                if (hexadecimalHash[i] != '0') return false;
            }

            return true;
        }

        private static string GetHexadecimalHash(MD5 md5, string input)
        {
            var hexadecimalHash = "";
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            foreach (byte b in hash)
            {
                hexadecimalHash += b.ToString("x2");
            }
            return hexadecimalHash;
        }
    }
}
