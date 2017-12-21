﻿using System;
using Microsoft.Xna.Framework;

namespace Gahame.GameUtils
{

    // A bit of matheroni since the other Maths class doesnt provide equal opportunities

    public static class MyMaths
    {
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

        // Normalize sick
        public static Vector2 Normalize(float x, float y)
        {
            float hyp = (float)(Math.Sqrt(x * x + y * y));
            return new Vector2(x / hyp, y / hyp);
        }

    }
}
