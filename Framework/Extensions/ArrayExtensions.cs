using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework.Extensions
{
    public static class ArrayExtensions
    {
        public static void Fill<T>(this T[] array) where T : new()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new T();
            }
        }

        public static void Fill<T>(this T[,] array) where T : new()
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = new T();
                }
            }
        }
    }
}
