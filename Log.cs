using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball
{
    internal static class Log
    {
        static int nLogSize = 25;
        static List<string> GameLog = new List<string>(nLogSize);
        static int nLogIndex = 1;

        static string strClearString = new string(' ', 25);
        

        internal static void d(string s)
        {
            if (GameLog.Count >= nLogSize)
            {
                GameLog.RemoveAt(0);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(new string(' ', 30));
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Last play: " + s);

            GameLog.Add(nLogIndex.ToString() + ". " + s);
            nLogIndex++;
        }
    }
}
