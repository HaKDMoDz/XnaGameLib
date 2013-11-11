using System;
using System.Collections.Generic;

namespace XnaGameLib
{
    public static class Extensions
    {
        public static void Merge<K, V>(this Dictionary<K, V> dict1, Dictionary<K, V> dict2)
        {
            foreach (var item in dict2)
            {
                if (!dict1.ContainsKey(item.Key))
                {
                    dict1.Add(item.Key, item.Value);
                }
            }
        }

        public static float NextFloat(this Random random, float min, float max)
        {
            return min + (float)(random.NextDouble() * (max - min));
        }

        public static double NextDouble(this Random random, double min, double max)
        {
            return min + random.NextDouble() * (max - min);
        }
    }
}
