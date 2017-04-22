using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class Bot
    {
        private List<int> values = new List<int>();

        public Bot(int botNumber, int value)
        {
            Number = botNumber;
            values.Add(value);
        }

        public int Number { get; private set; }

        public int? HighValue
        {
            get
            {
                if (values.Count != 2) return null;
                return values.Max();
            }
        }

        public int? LowValue
        {
            get
            {
                if (values.Count != 2) return null;
                return values.Min();
            }
        }

        public bool HasBothValues
        {
            get { return values.Count == 2; }
        }

        public void AddValue(int value)
        {
            if (values.Count >= 2) throw new InvalidOperationException("The bot already has both values");
            values.Add(value);
        }

        internal bool HasValue(int value)
        {
            return values.Contains(value);
        }
    }
}