using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day9
{
    public class Day9Puzzles
    {   
        static void Main(string[] args)
        {
            var input = LoadDataFromInputFile();

            string decompressedString;

            long decompressedStringLength = GetDecompressedStringLength(input, false, out decompressedString);
            Console.WriteLine(decompressedString);
            Console.WriteLine(decompressedStringLength);

            decompressedStringLength = GetDecompressedStringLength(input, true, out decompressedString);
            Console.WriteLine(decompressedString);
            Console.WriteLine(decompressedStringLength);

            Console.ReadLine();
        }

        /// <summary>
        /// Calculates the length of the decompressed string
        /// </summary>
        /// <param name="input">The input string to decompress</param>
        /// <param name="isRecursionEnabled">When true, goes through all markers recursively to calculate the decompressed string length, 
        /// otherwise treats the markers within the repeat sequence of other markers as regular text</param>
        /// <param name="decompressedString">The decomressed string. NOTE: always null if <paramref name="isRecursionEnabled"/> is set to true 
        /// as the result string may be too long to process</param>
        /// <returns>The length of the decompressed string</returns>
        public static long GetDecompressedStringLength(string input, bool isRecursionEnabled, out string decompressedString)
        {
            long decompressedStringLength = 0; 
            decompressedString = isRecursionEnabled ? null : string.Empty;

            int i = 0;
            bool isInsideMarker = false, isReadingSequenceLength = true, hasFinishedReadingMarker = false;
            string sequenceLength = string.Empty, repeatCount = string.Empty;

            while (i < input.Length)
            {
                if (!isInsideMarker)
                {
                    if (input[i] == '(')
                    {
                        isInsideMarker = true;
                    }
                    else
                    {
                        decompressedStringLength++;
                        
                        if (!isRecursionEnabled)
                        {
                            decompressedString += input[i];
                        }
                    }
                }
                else
                {
                    if (input[i] == 'x')
                    {
                        isReadingSequenceLength = false;
                    }
                    else if (input[i] == ')')
                    {
                        hasFinishedReadingMarker = true;
                    }
                    else if (isReadingSequenceLength)
                    {
                        sequenceLength += input[i];
                    }
                    else
                    {
                        repeatCount += input[i];
                    }
                }

                i++;

                if (hasFinishedReadingMarker)
                {
                    var sequenceToRepeat = input.Substring(i, int.Parse(sequenceLength));
                    i += sequenceToRepeat.Length;

                    if (isRecursionEnabled)
                    {
                        string decompressedSubstring;
                        decompressedStringLength += long.Parse(repeatCount) *
                                                    GetDecompressedStringLength(sequenceToRepeat, true, out decompressedSubstring);
                    }
                    else
                    {
                        var repeatedSequence = string.Concat(Enumerable.Repeat(sequenceToRepeat, int.Parse(repeatCount)));
                        decompressedString += repeatedSequence;
                        decompressedStringLength += repeatedSequence.Length;
                    }

                    sequenceLength = string.Empty;
                    repeatCount = string.Empty;
                    isInsideMarker = false;
                    isReadingSequenceLength = true;
                    hasFinishedReadingMarker = false;
                }
            }

            return decompressedStringLength;
        }

        private static string LoadDataFromInputFile()
        {
            string input;

            using (var streamReader = new StreamReader(Environment.CurrentDirectory + @"\..\..\Input.txt"))
            {
                input = streamReader.ReadToEnd();
            }

            return input;
        }
    }
}
