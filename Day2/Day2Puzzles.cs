using System;
using System.Collections.Generic;

namespace Day2
{
    class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
    
    public class Day2Puzzles
    {
        public static readonly List<char[][]> Keypads = new List<char[][]>
        {
            new[]
            {
                new[] {'1', '2', '3'},
                new[] {'4', '5', '6'},
                new[] {'7', '8', '9'}
            },
            new[]
            {
                new[] {' ', ' ', '1', ' ', ' '},
                new[] {' ', '2', '3', '4', ' '},
                new[] {'5', '6', '7', '8', '9'},
                new[] {' ', 'A', 'B', 'C', ' '},
                new[] {' ', ' ', 'D', ' ', ' '}
            }
        };

        static void Main(string[] args)
        {
            var instructions = new[]
            {
                "RDLRUUULRRDLRLLRLDDUDLULULDDULUDRRUURLRLLUULDURRULLRULDRRDLLULLRLLDRLDDRRRRLLRLURRRDRDULRDUULDDDULURUDDRRRUULUDRLLUUURLUDRUUUDRDUULLRLLUDDRURRDDDRDLUUURLRLLUDRURDUDUULDDLLRDURULLLURLDURLUUULDULDDULULLLRRUDLRUURDRDLLURLUDULDUUUURRLDLUDRULUDLDLLDRLDDDRRLLDUDLLRRDDDRLUDURLLLDRUDDLDDRRLUDRRDUDLRRLULDULURULDULUULDRLLDRUUDDRLLUDRULLRRRLRDLRLUDLRULDRDLRDRLRULUDUURRUUULLDDDDUDDLDDDDRRULRDLRDDULLDLDLLDLLDLLDRRDDDRDDLRRDDDRLLLLURRDLRRLDRURDDURDULDDRUURUDUDDDRDRDDRLRRLRULLDRLDLURLRLRUDURRRDLLLUDRLRDLLDDDLLUDRLDRRUUDUUDULDULLRDLUDUURLDDRUDR",
                "URULDDLDDUDLLURLUUUUUULUDRRRDDUDURDRUURLLDRURLUULUDRDRLLDRLDULRULUURUURRLRRDRUUUDLLLLRUDDLRDLLDUDLLRRURURRRUDLRLRLLRULRLRLRDLRLLRRUDDRLRUDULDURDLDLLLRDRURURRULLLDLLRRDRLLDUUDLRUUDDURLLLDUUDLRDDURRDRRULLDRLRDULRRLLRLLLLUDDDRDRULRRULLRRUUDULRRRUDLLUUURDUDLLLURRDDUDLDLRLURDDRRRULRRUDRDRDULURULRUDULRRRLRUDLDDDDRUULURDRRDUDLULLRUDDRRRLUDLRURUURDLDURRDUUULUURRDULLURLRUUUUULULLDRURULDURDDRRUDLRLRRLLLLDDUURRULLURURRLLDRRDDUUDLLUURRDRLLLLRLUDUUUDLRLRRLDURDRURLRLRULURLDULLLRRUUUDLLRRDUUULULDLLDLRRRDUDDLRULLULLULLULRU",
                "DURUUDULRRLULLLDDUDDLRRDURURRRDDRRURDRURDRLULDUDUDUULULDDUURDDULRDUDUDRRURDRDDRLDRDRLDULDDULRULLDULURLUUDUDULRDDRRLURLLRRDLLDLDURULUDUDULDRLLRRRUDRRDDDRRDRUUURLDLURDLRLLDUULLRULLDDDDRULRRLRDLDLRLUURUUULRDUURURLRUDRDDDRRLLRLLDLRULUULULRUDLUDULDLRDDDDDRURDRLRDULRRULRDURDDRRUDRUDLUDLDLRUDLDDRUUULULUULUUUDUULDRRLDUDRRDDLRUULURLRLULRURDDLLULLURLUDLULRLRRDDDDDRLURURURDRURRLLLLURLDDURLLURDULURUUDLURUURDLUUULLLLLRRDUDLLDLUUDURRRURRUUUDRULDDLURUDDRRRDRDULURURLLDULLRDDDRRLLRRRDRLUDURRDLLLLDDDDLUUURDDDDDDLURRURLLLUURRUDLRLRRRURULDRRLULD",
                "LLUUURRDUUDRRLDLRUDUDRLRDLLRDLLDRUULLURLRRLLUDRLDDDLLLRRRUDULDLLLDRLURDRLRRLURUDULLRULLLURRRRRDDDLULURUDLDUDULRRLUDDURRLULRRRDDUULRURRUULUURDRLLLDDRDDLRRULRDRDRLRURULDULRRDRLDRLLDRDURUUULDLLLRDRRRLRDLLUDRDRLURUURDLRDURRLUDRUDLURDRURLRDLULDURDDURUUDRLULLRLRLDDUDLLUUUURLRLRDRLRRRURLRULDULLLLDLRRRULLUUDLDURUUUDLULULRUDDLLDLDLRLDDUDURDRLLRRLRRDDUDRRRURDLRLUUURDULDLURULUDULRRLDUDLDDDUUDRDUULLDDRLRLLRLLLLURDDRURLDDULLULURLRDUDRDDURLLLUDLLLLLUDRDRDLURRDLUDDLDLLDDLUDRRDDLULRUURDRULDDDLLRLDRULURLRURRDDDRLUUDUDRLRRUDDLRDLDULULDDUDURRRURULRDDDUUDULLULDDRDUDRRDRDRDLRRDURURRRRURULLLRRLR",
                "URLULLLDRDDULRRLRLUULDRUUULDRRLLDDDLDUULLDRLULRRDRRDDDRRDLRRLLDDRDULLRRLLUDUDDLDRDRLRDLRDRDDUUDRLLRLULLULRDRDDLDDDRLURRLRRDLUDLDDDLRDLDLLULDDRRDRRRULRUUDUULDLRRURRLLDRDRRDDDURUDRURLUDDDDDDLLRLURULURUURDDUDRLDRDRLUUUULURRRRDRDULRDDDDRDLLULRURLLRDULLUUDULULLLLRDRLLRRRLLRUDUUUULDDRULUDDDRRRULUDURRLLDURRDULUDRUDDRUURURURLRDULURDDDLURRDLDDLRUDUUDULLURURDLDURRDRDDDLRRDLLULUDDDRDLDRDRRDRURRDUDRUURLRDDUUDLURRLDRRDLUDRDLURUDLLRRDUURDUDLUDRRL"
            }; 

            foreach (var keypad in Keypads)
            {
                Console.WriteLine(GetCode(instructions, keypad));
            }
            
            Console.ReadLine();
        }

        public static string GetCode(string[] instructions, char[][] keypad)
        {
            string code = "";
            var position = GetStartPoisition(keypad); 

            foreach (var instruction in instructions)
            {
                foreach (var command in instruction)
                {
                    switch (command)
                    {
                        case 'U':
                            MoveUp(position, keypad);
                            break;
                        case 'D':
                            MoveDown(position, keypad);
                            break;
                        case 'L':
                            MoveLeft(position, keypad);
                            break;
                        case 'R':
                            MoveRight(position, keypad);
                            break;
                    }
                }

                code += keypad[position.Row][position.Column];
            }

            return code;
        }

        private static Position GetStartPoisition(char[][] keypad)
        {
            for (int i = 0; i < keypad.Length; i++)
            {
                for (int j = 0; j < keypad[0].Length; j++)
                {
                    if (keypad[i][j] == '5') return new Position() {Row = i, Column = j};
                }
            }

            throw new Exception("Could not find '5' on the keypad");
        }

        private static void MoveUp(Position position, char[][] keypad)
        {
            if (position.Row > 0 && keypad[position.Row - 1][position.Column] != ' ') 
                position.Row -= 1;
        }

        private static void MoveDown(Position position, char[][] keypad)
        {
            if (position.Row < keypad.Length - 1 && keypad[position.Row + 1][position.Column] != ' ') 
                position.Row += 1;
        }

        private static void MoveLeft(Position position, char[][] keypad)
        {
            if (position.Column > 0 && keypad[position.Row][position.Column - 1] != ' ') 
                position.Column -= 1;
        }

        private static void MoveRight(Position position, char[][] keypad)
        {
            if (position.Column < keypad[position.Row].Length - 1 && keypad[position.Row][position.Column + 1] != ' ') 
                position.Column += 1;
        }

    }
}
