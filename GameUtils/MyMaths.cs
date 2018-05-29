using System;
using Microsoft.Xna.Framework;

namespace Gahame.GameUtils
{

    // A bit of matheroni since the other Maths class doesnt provide equal opportunities

    public static class MyMaths
    {
        // Hmmm?
        static Random random = new Random();

        // What have I become? The inline G O D
        public static float Approach(float value, float target, float speed)
        {
            return value == target ? target : value < target ? value + speed > target ? target : value + speed : value - speed < target ? target : value - speed;
        }

        // Lerp
        public static float Lerp(float value, float target, float amount)
        {
            return value + (target - value) * amount;
        }

        // Round float to int
        public static int Round(float value)
        {
            return (int)(value + .5f);
        }

        // Round Vector to int
        public static Vector2 Round(Vector2 vector)
        {
            return new Vector2(Round(vector.X), Round(vector.Y));
        }

        // Clamp between values
        public static float Clamp(float value, float min, float max)
        {
            return (value >= min && value <= max) ? value : (value > max) ? max : min;
        }

        // Hehe
        public static float RandomInRange(float min, float max)
        {
            return min + (float)(random.NextDouble() * (max - min));
        }

        // this is distance times distance
        public static float DistanceCubed(Vector2 p1, Vector2 p2)
        {
            return (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
        }

        // Normalize sick
        public static Vector2 Normalize(float x, float y)
        {
            float hyp = (float)(Math.Sqrt(x * x + y * y));
            if (hyp == 0) return Vector2.Zero;
            return new Vector2(x / hyp, y / hyp);
        }
    }
}
