using System.Collections.Generic;
using System.IO;

namespace Utilities
{
    public static class FileHelper
    {
        public static List<string> ReadLines(string filePath)
        {
            var lines = new List<string>();

            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }

            return lines;
        }

        public static string ReadToEnd(string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
