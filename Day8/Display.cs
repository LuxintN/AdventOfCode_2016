using System;
using System.Linq;

namespace Day8
{
    public class Display
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Pixel[][] Pixels { get; private set; }

        public Display(int height, int width)
        {
            Height = height;
            Width = width;

            Pixels = new Pixel[Height][];

            for (int i = 0; i < Height; i++)
            {
                Pixels[i] = new Pixel[Width];

                for (int j = 0; j < Width; j++)
                {
                    Pixels[i][j] = new Pixel();
                }
            }
        }

        public void Rect(int rectHeight, int rectWidth)
        {
            for (int i = 0; i < rectHeight; i++)
            {
                for (int j = 0; j < rectWidth; j++)
                {
                    Pixels[i][j].IsOn = true;
                }
            }
        }

        public void RotateRow(int rowIndex, int pixelCount)
        {
            var newRow = new Pixel[Width];
            Pixels[rowIndex].CopyTo(newRow, 0);

            for (int i = 0; i < Width; i++)
            {
                int newX = i + pixelCount;
                newRow[newX < Width ? newX : newX % Width].IsOn = Pixels[rowIndex][i].IsOn;
            }

            Pixels[rowIndex] = newRow;
        }

        public void RotateColumn(int columnIndex, int pixelCount)
        {
            var newColumn = new Pixel[Height];
            for (int i = 0; i < Height; i++)
            {
                newColumn[i] = Pixels[i][columnIndex];
            }

            for (int i = 0; i < Height; i++)
            {
                int newY = i + pixelCount;
                newColumn[newY < Height ? newY : newY % Height].IsOn = Pixels[i][columnIndex].IsOn;
            }

            for (int i = 0; i < Height; i++)
            {
                Pixels[i][columnIndex] = newColumn[i];
            }
        }

        public void ExecuteCommand(string command)
        {
            command = command.Trim();

            if (command.StartsWith("rect ")) 
            { 
                var indexOfX = command.LastIndexOf("x");
                var indexOfSpace = command.IndexOf(" ");
                var rectHeight = int.Parse(command.Substring(indexOfX + 1, command.Length - (indexOfX + 1)));
                var rectWidth = int.Parse(command.Substring(indexOfSpace + 1, indexOfX - (indexOfSpace + 1)));
                Rect(rectHeight, rectWidth);
            }
            else if (command.StartsWith("rotate row "))
            {
                var indexOfEquals = command.IndexOf("=");
                var lastIndexOfSpace = command.LastIndexOf(" ");
                var rowIndex = int.Parse(command.Substring(indexOfEquals + 1, command.IndexOf(" by") - indexOfEquals));
                var pixelCount = int.Parse(command.Substring(lastIndexOfSpace + 1, command.Length - (lastIndexOfSpace + 1)));
                RotateRow(rowIndex, pixelCount);
            }
            else if (command.StartsWith("rotate column "))
            {
                var indexOfEquals = command.LastIndexOf("=");
                var lastIndexOfSpace = command.LastIndexOf(" ");
                var columnIndex = int.Parse(command.Substring(indexOfEquals + 1, command.IndexOf(" by") - indexOfEquals));
                var pixelCount = int.Parse(command.Substring(lastIndexOfSpace + 1, command.Length - (lastIndexOfSpace + 1)));
                RotateColumn(columnIndex, pixelCount);
            }
            else
            {
                throw new ArgumentException("Unknown command '" + command + "'");
            }
        }

        public int GetLitPixelsCount()
        {
            return Pixels.SelectMany(x => x).Count(x => x.IsOn);
        }

        public override string ToString()
        {
            var display = string.Empty;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    display += Pixels[i][j].IsOn ? "#" : " ";
                }

                display += "\n";
            }

            return display;
        }
    }
}
