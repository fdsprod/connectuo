using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConnectUO.Framework.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToDebugString(this byte[] buffer)
        {
            return buffer.ToDebugString(buffer.Length);
        }

        public static string ToDebugString(this byte[] buffer, int length)
        {
            StringBuilder builder = new StringBuilder();
            MemoryStream stream = new MemoryStream(buffer);

            stream.ToDebugString(length, builder);

            return builder.ToString();
        }

        public static void ToDebugString(this byte[] buffer, int length, StringBuilder builder)
        {
            MemoryStream stream = new MemoryStream(buffer);
            stream.ToDebugString(length, builder);
        }

        public static void ToDebugString(this byte[] buffer, int length, StreamWriter writer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            stream.ToDebugString(length, writer);
        }
    }
}
