using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectUO.Framework
{
    public static class MathHelper
    {
        public static int Clamp(int value, int min, int max)
        {
            value = Math.Max(min, value);
            value = Math.Min(max, value);

            return value;
        }

        public static long Clamp(long value, long min, long max)
        {
            value = Math.Max(min, value);
            value = Math.Min(max, value);

            return value;
        }

        public static double Clamp(double value, double min, double max)
        {
            value = Math.Max(min, value);
            value = Math.Min(max, value);

            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            value = Math.Max(min, value);
            value = Math.Min(max, value);

            return value;
        }

        public static byte Clamp(byte value, byte min, byte max)
        {
            value = (byte)Math.Max(min, value);
            value = (byte)Math.Min(max, value);

            return value;
        }
    }
}
