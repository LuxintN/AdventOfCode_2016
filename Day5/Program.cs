using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "wtnhxymk"; // "abc"

            string password1;
            char[] password2;

            GetPasswords(input, out password1, out password2);
            
            Console.WriteLine(password1.ToLower() + "       " + string.Concat(password2));

            Console.ReadLine();
        }

        private static void GetPasswords(string input, out string password1, out char[] password2)
        {
            password1 = "";
            password2 = new char[8];

            using (var md5 = MD5.Create())
            {
                int attempt = 0;

                while (password1.Length < 8 || password2.Any(c => c == '\0'))
                {
                    var inputWithNumber = input + attempt++; // "abc3231929"; 
                    var hexadecimalHash = GetHexadecimalHash(md5, inputWithNumber);

                    if (DoesHashStartWithFiveZeroes(hexadecimalHash))
                    {
                        if (password1.Length < 8) password1 += hexadecimalHash[5];

                        int position;
                        if (int.TryParse(hexadecimalHash[5].ToString(), out position) && position < 8 && password2[position] == '\0')
                        {
                            password2[position] = hexadecimalHash[6];
                        }
                    }
                }
            }
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
