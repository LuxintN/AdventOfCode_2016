using System;
using System.Collections.Generic;

namespace Day10
{
    public static class DictionaryExtensions
    {
        public static T2 AddOrUpdate<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, Func<T2> createValue)
        {
            if (dictionary.ContainsKey(key)) dictionary[key] = createValue();
            else dictionary.Add(key, createValue());
            return dictionary[key];
        }
    }
}
