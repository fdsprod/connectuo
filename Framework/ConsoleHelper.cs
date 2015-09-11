using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectUO.Framework
{
    public static class ConsoleHelper
    {
        private static Stack<ConsoleColor> _consoleColors = new Stack<ConsoleColor>();

        /// <summary>
        /// Pushes the color to the console
        /// </summary>
        public static void PushColor(ConsoleColor color)
        {
            try
            {
                _consoleColors.Push(Console.ForegroundColor);
                Console.ForegroundColor = color;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Pops the color of the console to the previous value.
        /// </summary>
        public static ConsoleColor PopColor()
        {
            try
            {
                Console.ForegroundColor = _consoleColors.Pop();
            }
            catch
            {

            }

            return Console.ForegroundColor;
        }
    }
}
