using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace XnaGameLib
{
    public static class Vectors
    {
        private static Random _Random = new Random();

        public static Vector2 RandomVector2(float minMagnitude, float maxMagnitude)
        {
            Debug.Assert(minMagnitude >= 0);
            Debug.Assert(maxMagnitude >= 0);
            Debug.Assert(minMagnitude <= maxMagnitude);
            double angle = MathHelper.TwoPi * _Random.NextDouble();
            double magnitude = minMagnitude + _Random.NextDouble() * (maxMagnitude - minMagnitude);
            return new Vector2((float)(magnitude * Math.Cos(angle)), (float)(magnitude * Math.Sin(angle)));
        }

        public static Vector2 RandomVector2()
        {
            return RandomVector2(1, 1);
        }

        public static Vector2 Vector2FromAngle(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}

