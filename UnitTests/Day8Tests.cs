using System.Linq;
using Day8;
using NUnit.Framework;

namespace UnitTests
{
    public class Day8Tests
    {
        private Display display;

        [SetUp]
        public void Setup()
        {
            display = new Display(6, 50);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(3, 2)]
        [TestCase(6, 6)]
        public void Display_Rect_TurnsOnCorrectPixels(int rectHeight, int rectWidth)
        {
            // Arrange
            // Act
            display.Rect(rectHeight, rectWidth);

            // Assert
            for (int i = 0; i < display.Height; i++)
            {
                for (int j = 0; j < display.Width; j++)
                {
                    Assert.AreEqual(i < rectHeight && j < rectWidth, display.Pixels[i][j].IsOn);
                }
            }
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(2, 47)]
        [TestCase(2, 48)]
        [TestCase(2, 49)]
        [TestCase(2, 50)]
        [TestCase(2, 100)]
        [TestCase(21, 40)]
        [TestCase(50, 30)]
        [TestCase(50, 300)]
        public void Display_RotateRow_PerformsRotationCorrectly(int rectWidth, int pixelCount)
        {
            // Arrange
            display.Rect(1, rectWidth);
            
            // Act
            display.RotateRow(0, pixelCount);

            // Assert
            for (int i = 0; i < display.Width; i++)
            {
                Assert.AreEqual(
                    rectWidth + pixelCount <= display.Width && pixelCount <= i && i < rectWidth + pixelCount
                    || rectWidth + pixelCount > display.Width && (pixelCount % display.Width <= i && i < pixelCount % display.Width + rectWidth 
                                                                  || i < rectWidth - (display.Width - pixelCount % display.Width)),
                    display.Pixels[0][i].IsOn);
            } 

            Assert.AreEqual(rectWidth, display.Pixels[0].Count(x => x.IsOn));
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(2, 5)]
        [TestCase(2, 6)]
        [TestCase(2, 7)]
        [TestCase(2, 8)]
        [TestCase(6, 2)]
        [TestCase(2, 40)]
        [TestCase(3, 40)]
        [TestCase(4, 40)]
        [TestCase(5, 40)]
        [TestCase(6, 40)]
        public void Display_RotateColumn_PerformsRotationCorrectly(int rectHeight, int pixelCount)
        {
            // Arrange
            display.Rect(rectHeight, 1);

            // Act
            display.RotateColumn(0, pixelCount);

            // Assert
            for (int i = 0; i < display.Height; i++)
            {
                Assert.AreEqual(
                    rectHeight + pixelCount <= display.Height && pixelCount <= i && i < rectHeight + pixelCount
                    || rectHeight + pixelCount > display.Height && (pixelCount % display.Height <= i && i < pixelCount % display.Height + rectHeight
                                                                  || i < rectHeight - (display.Height - pixelCount % display.Height)),
                    display.Pixels[i][0].IsOn);
            }

            Assert.AreEqual(rectHeight, display.Pixels.Count(x => x[0].IsOn));
        }

        [Test]
        [TestCase("rect 4x1", 4, 1)]
        [TestCase("rect 14x1", 14, 1)]
        public void Display_ExecuteCommand_Rect_ExecutesRectCommandWithCorrectParameters(string command, int rectWidth, int rectHeight)
        {
            // Arrange
            var testDisplay = new Display(6, 50);
            testDisplay.Rect(rectHeight, rectWidth);

            // Act 
            display.ExecuteCommand(command);

            // Assert
            for (int i = 0; i < testDisplay.Height; i++)
            {
                for (int j = 0; j < testDisplay.Width; j++)
                {
                    Assert.AreEqual(testDisplay.Pixels[i][j].IsOn, display.Pixels[i][j].IsOn);
                }
            }
        }

        [Test]
        [TestCase("rotate row y=1 by 1", 1, 1)]
        [TestCase("rotate row y=2 by 16", 2, 16)]
        public void Display_ExecuteCommand_RotateRow_ExecutesRectCommandWithCorrectParameters(string command, int rectWidth, int pixelCount)
        {
            // Arrange
            var testDisplay = new Display(6, 50);
            testDisplay.RotateRow(rectWidth, pixelCount);

            // Act 
            display.ExecuteCommand(command);

            // Assert
            for (int i = 0; i < testDisplay.Height; i++)
            {
                for (int j = 0; j < testDisplay.Width; j++)
                {
                    Assert.AreEqual(testDisplay.Pixels[i][j].IsOn, display.Pixels[i][j].IsOn);
                }
            }
        }

        [Test]
        [TestCase("rotate column x=10 by 1", 10, 1)]
        [TestCase("rotate column x=10 by 13", 10, 13)]
        public void Display_ExecuteCommand_RotateColumn_ExecutesRectCommandWithCorrectParameters(string command, int rectHeight, int pixelCount)
        {
            // Arrange
            var testDisplay = new Display(6, 50);
            testDisplay.RotateColumn(rectHeight, pixelCount);
            
            // Act 
            display.ExecuteCommand(command);

            // Assert
            for (int i = 0; i < testDisplay.Height; i++)
            {
                for (int j = 0; j < testDisplay.Width; j++)
                {
                    Assert.AreEqual(testDisplay.Pixels[i][j].IsOn, display.Pixels[i][j].IsOn);
                }
            }
        }

        [Test]
        [TestCase("rect 1x1", 1)]
        [TestCase("rect 4x3", 12)]
        [TestCase("rect 14x2", 28)]
        public void Display_GetLitPixelsCount_ReturnsCorrectValue(string command, int expectesLitPixelsCount)
        {
            // Arrange
            display.ExecuteCommand(command);

            // Act
            // Assert
            Assert.AreEqual(expectesLitPixelsCount, display.GetLitPixelsCount());
        }
    }
}
