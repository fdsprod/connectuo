using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectUO.Framework.Diagnostics
{
    public sealed class ConsoleTraceListener : TraceListener
    {
        protected override void OnTraceReceived(TraceMessage message)
        {
            ConsoleColor color = ConsoleColor.Gray;

            switch(message.Type)
            {
                case TraceLevels.Info: color = ConsoleColor.White; break;
                case TraceLevels.Warning: color = ConsoleColor.Yellow; break;
                case TraceLevels.Error:
                case TraceLevels.Fatal: color = ConsoleColor.Red; break;
                default: break;
            }

            ConsoleHelper.PushColor(color);
            Console.WriteLine(message);
            ConsoleHelper.PopColor();
        }
    }
}
