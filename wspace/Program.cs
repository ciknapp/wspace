using System;
using static wspace.Whitespace;

namespace wspace
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWhitespace("pushString");

            IO.DisplayString("Hello, world!");

            FinishProgram();
        }
    }
}
