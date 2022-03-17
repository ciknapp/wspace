using System;
using static wspace.Whitespace;

namespace wspace
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWhitespace("pushNumber");

            for(int i = 0; i < 5; i++)
            {
                Stack.PushNumber(100);
                IO.OutputNumber(true);
            }

            FinishProgram();
        }
    }
}
