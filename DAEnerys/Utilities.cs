using OpenTK;
using System;

namespace Extensions
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
        public static float LerpAngleRadians(float a, float b, float f)
        {
            float difference = Math.Abs(b - a);
            if (difference > 180f / 180 * (float)Math.PI)
            {
                // We need to add on to one of the values.
                if (b > a)
                {
                    // We'll add it on to start...
                    a += 360f / 180 * (float)Math.PI;
                }
                else
                {
                    // Add it on to end.
                    b += 360f / 180 * (float)Math.PI;
                }
            }

            // Interpolate it.
            float value = (a + ((b - a) * f));

            // Wrap it..
            float rangeZero = 360f / 180 * (float)Math.PI;

            if (value >= 0 && value <= 360f / 180 * (float)Math.PI)
                return value;

            return (value % rangeZero);
        }

        public static Vector3 ToEulerAngles(this Quaternion q)
        {
            // Store the Euler angles in radians
            Vector3 pitchYawRoll = new Vector3();

            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;

            // If quaternion is normalised the unit is one, otherwise it is the correction factor
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;

            if (test > 0.499f * unit)
            {
                // Singularity at north pole
                pitchYawRoll.Y = 2f * (float)Math.Atan2(q.X, q.W);  // Yaw
                pitchYawRoll.X = (float)Math.PI * 0.5f;                         // Pitch
                pitchYawRoll.Z = 0f;                                // Roll
                return pitchYawRoll;
            }
            else if (test < -0.499f * unit)
            {
                // Singularity at south pole
                pitchYawRoll.Y = -2f * (float)Math.Atan2(q.X, q.W); // Yaw
                pitchYawRoll.X = -(float)Math.PI * 0.5f;                        // Pitch
                pitchYawRoll.Z = 0f;                                // Roll
                return pitchYawRoll;
            }

            pitchYawRoll.Y = (float)Math.Atan2(2 * q.Y * q.W - 2 * q.X * q.Z, sqx - sqy - sqz + sqw);       // Yaw
            pitchYawRoll.X = (float)Math.Asin(2 * test / unit);                                             // Pitch
            pitchYawRoll.Z = (float)Math.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z, -sqx + sqy - sqz + sqw);      // Roll

            return pitchYawRoll;
        }

        public static Vector3 Matrix4ToEuler(this Matrix4 R)
        {
            float sy = (float)Math.Sqrt(R[0, 0] * R[0, 0] + R[1, 0] * R[1, 0]);

            bool singular = sy < 1e-6; // If

            float x, y, z;
            if (!singular)
            {
                x = (float)Math.Atan2(R[2, 1], R[2, 2]);
                y = (float)Math.Atan2(-R[2, 0], sy);
                z = (float)Math.Atan2(R[1, 0], R[0, 0]);
            }
            else
            {
                x = (float)Math.Atan2(-R[1, 2], R[1, 1]);
                y = (float)Math.Atan2(-R[2, 0], sy);
                z = 0;
            }
            return new Vector3(x, y, z);
        }

        public static float SmoothStepChange(float x0, float y0, float yt, float t, float k)
        {
            float f = x0 - y0 + (yt - y0) / (k * t);
            return yt - (yt - y0) / (k * t) + f * (float)Math.Exp(-k * t);
        }

        public static Quaternion QuaternionFromEulerAngles(float yaw, float pitch, float roll)
        {
            // Heading = Yaw
            // Attitude = Pitch
            // Bank = Roll

            yaw *= 0.5f;
            pitch *= 0.5f;
            roll *= 0.5f;

            // Assuming the angles are in radians.
            float c1 = (float)Math.Cos(yaw);
            float s1 = (float)Math.Sin(yaw);
            float c2 = (float)Math.Cos(pitch);
            float s2 = (float)Math.Sin(pitch);
            float c3 = (float)Math.Cos(roll);
            float s3 = (float)Math.Sin(roll);
            float c1c2 = c1 * c2;
            float s1s2 = s1 * s2;

            Quaternion quaternion = new Quaternion();
            quaternion.W = c1c2 * c3 - s1s2 * s3;
            quaternion.X = c1c2 * s3 + s1s2 * c3;
            quaternion.Y = s1 * c2 * c3 + c1 * s2 * s3;
            quaternion.Z = c1 * s2 * c3 - s1 * c2 * s3;

            return quaternion;
        }
    }
}
