using System;
using static wspace.Whitespace;

namespace wspace
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWhitespace(name: "My Whitespace Code");

            IO.DisplayString("Hello, Lisa!");

            FinishProgram();
        }
    }
}
