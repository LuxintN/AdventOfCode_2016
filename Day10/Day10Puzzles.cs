using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utilities;

namespace Day10
{
    public class Day10Puzzles
    {
        private static readonly Dictionary<int, Bot> botsWithAtLeastOneValue = new Dictionary<int, Bot>();
        private static readonly HashSet<Bot> botsWithTwoValues = new HashSet<Bot>();
        private static readonly Dictionary<int, int> outputs = new Dictionary<int, int>();

        static void Main()
        {
            var commands = FileHelper.ReadLines(Environment.CurrentDirectory + @"\..\..\input.txt");

            var botNumber = GetBotResponsibleForComparingValues(commands, 61, 17, true);
            Console.WriteLine(botNumber);
            Console.WriteLine(outputs[0] * outputs[1] * outputs[2]);

            Console.ReadLine();
        }

        public static int GetBotResponsibleForComparingValues(List<string> commands, int highValue, int lowValue, bool checkIfFirstThreeOutputsHaveValues)
        {
            int botNumber = -1;

            while (botNumber == -1 || checkIfFirstThreeOutputsHaveValues && !ThreeOutputsHaveValues())
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    var matches =
                        Regex.Matches(commands[i], @"(value|bot|output) \d+")
                            .Cast<Match>()
                            .Select(x => x.Value.Split(' '))
                            .ToList();

                    if (matches.Count == 2)
                    {
                        GiveValueToBot(matches);
                    }
                    else if (matches.Count == 3)
                    {
                        TakeValuesAwayFromBot(matches);
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not parse the input line");
                    }

                    var targetBot = botsWithTwoValues.FirstOrDefault(b => b.HighValue == highValue && b.LowValue == lowValue);
                    if (targetBot != null) botNumber = targetBot.Number; 
                }
            }

            return botNumber;
        }

        private static bool ThreeOutputsHaveValues()
        {
            int output0;
            int output1;
            int output2;

            return outputs.TryGetValue(0, out output0)
                && outputs.TryGetValue(1, out output1)
                && outputs.TryGetValue(2, out output2);
        }

        private static void GiveValueToBot(List<string[]> matches)
        {
            var value = int.Parse(matches.Find(x => x.First()[0] == 'v').Last());
            var botNumber = int.Parse(matches.Find(x => x.First()[0] == 'b').Last());

            CreateBotOrGiveValue(botNumber, value);
        }

        private static void CreateBotOrGiveValue(int botNumber, int value)
        {
            Bot bot;
            if (botsWithAtLeastOneValue.TryGetValue(botNumber, out bot))
            {
                if (!bot.HasValue(value)) bot.AddValue(value);
                if (bot.HasBothValues) botsWithTwoValues.Add(bot);
            }
            else
            {
                botsWithAtLeastOneValue.Add(botNumber, new Bot(botNumber, value));
            }
        }

        private static void TakeValuesAwayFromBot(List<string[]> matches)
        {
            var botNumber = int.Parse(matches[0].Last());

            Bot bot;
            if (botsWithAtLeastOneValue.TryGetValue(botNumber, out bot))
            {
                if (botsWithTwoValues.Contains(bot))
                {
                    if (bot.LowValue.HasValue) GiveValueToReceiver(matches[1], bot.LowValue.Value);
                    if (bot.HighValue.HasValue) GiveValueToReceiver(matches[2], bot.HighValue.Value);

                    botsWithTwoValues.Remove(bot);
                    botsWithAtLeastOneValue.Remove(botNumber);
                }
            }
        }

        private static void GiveValueToReceiver(string[] match, int value)
        {
            var receiverNumber = int.Parse(match.Last());

            if (match.First().StartsWith("b"))
            {
                CreateBotOrGiveValue(receiverNumber, value);
            }
            else
            {
                outputs.AddOrUpdate(receiverNumber, () => value);
            }
        }
    }
}
