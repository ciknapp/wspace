using System;
using static wspace.Whitespace;

namespace wspace
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWhitespace(name: "My Whitespace Code");

            Stack.PushNumber(1);
            Stack.PushNumber(1);
            Arithmetic.Subtraction();

            Flow.JumpToLabelIfZero("if true");
            Flow.JumpToLabel("else");

            Flow.CreateLabel("if true");
            IO.DisplayString("True!!!");
            Flow.JumpToLabel("end if");

            Flow.CreateLabel("else");
            IO.DisplayString("Oh no, it was false.... :(");
            Flow.JumpToLabel("end if");

            Flow.CreateLabel("end if");

            Flow.EndProgram();

            WriteCodeFile();
        }
    }
}
