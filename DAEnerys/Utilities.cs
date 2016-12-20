using OpenTK;
using System;

namespace DAEnerys
{
    public static class Utilities
    {
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;

            return value;
        }

        public static int CountCharacters(string input, char inputCharacter)
        {
            int count = 0;
            char[] chars = input.ToCharArray();
            foreach (char character in chars)
            {
                if (character == inputCharacter)
                    count++;
            }
            return count;
        }

        public static Vector2 CalculateBezierPoint(Vector2 p0, Vector2 p1, Vector2 c0, Vector2 c1, float blend)
        {
            // first stage, linear interpolate point pairs: [p0, c0], [c0, c1], [c1, p1]
            Vector2 p0c0 = Vector2.Lerp(p0, c0, blend);
            Vector2 c0c1 = Vector2.Lerp(c0, c1, blend);
            Vector2 c1p1 = Vector2.Lerp(c1, p1, blend);

            // second stage, reduce to two points
            Vector2 l = Vector2.Lerp(p0c0, c0c1, blend);
            Vector2 r = Vector2.Lerp(c0c1, c1p1, blend);

            // final stage, reduce to result point and return
            return Vector2.Lerp(l, r, blend);
        }

        public static float Lerp(float a, float b, float f)
        {
            return (a * (1.0f - f)) + (b * f);
        }
        public static float LerpAngleDegrees(float a, float b, float f)
        {
            float difference = Math.Abs(b - a);
            if (difference > 180)
            {
                // We need to add on to one of the values.
                if (b > a)
                {
                    // We'll add it on to start...
                    a += 360;
                }
                else
                {
                    // Add it on to end.
                    b += 360;
                }
            }

            // Interpolate it.
            float value = (a + ((b - a) * f));

            // Wrap it..
            float rangeZero = 360;

            if (value >= 0 && value <= 360)
                return value;

            return (value % rangeZero);
        }

        public static uint SwapEndian(uint val)
        {
            return ((val & 0xff) << 24) |
                    ((val & 0xff00) << 8) |
                    ((val >> 8) & 0xff00) |
                    ((val >> 24) & 0xff);
        }

        public static int SwapEndian(int val)
        {
            return ((val & 0xff) << 24) |
                    ((val & 0xff00) << 8) |
                    ((val >> 8) & 0xff00) |
                    ((val >> 24) & 0xff);
        }
    }
}
