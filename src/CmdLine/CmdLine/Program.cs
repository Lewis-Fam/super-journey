using System;
using LewisFam.CmdLine;

namespace CmdLine
{
    class Program : BaseLewisFamConsole
    {
        static void Main(string[] args)
        {
            SetColor(ConsoleColor.DarkMagenta, ConsoleColor.White);
            Console.WriteLine(HelloWorld);
        }

        static void JsonSample()
        {
        }
    }
}
