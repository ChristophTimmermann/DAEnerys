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
            foreach(char character in chars)
            {
                if (character == inputCharacter)
                    count++;
            }
            return count;
        }
    }
}
