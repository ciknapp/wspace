using System;
using System.Collections.Generic;
using static wspace.Whitespace;

namespace wspace
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeWhitespace(name: "My Whitespace Code");

            //string IF0T = "IF0T";
            //string IF0F = "IF0F";
            //string IF0E = "IF0E";

            //Stack.PushNumber(2); // A
            //Stack.PushNumber(1); // B
            //Arithmetic.Subtraction(); // B - A

            //Flow.JumpToLabelIfNegative(IF0T); // IF A > B
            //Flow.JumpToLabel(IF0F); // ELSE A <= B

            //Flow.CreateLabel(IF0T);
            //IO.DisplayString("A was greater than B.");
            //Flow.JumpToLabel(IF0E);

            //Flow.CreateLabel(IF0F);
            //IO.DisplayString("Oh no, it was false.... :(");
            //Flow.JumpToLabel(IF0E);

            //Flow.CreateLabel(IF0E);

            // Comparing two characters next.
            string l4 = "IF1 A == B";
            string l5 = "IF1 A < B";
            string l6 = "IF1 A > B";
            string l7 = "IF1 END";

            Stack.PushCharacter('V'); // B
            Stack.PushCharacter('W'); // A
            Arithmetic.Subtraction(); /* If A==B, result is 0.
                                       * If A is alphabetically before B, result will be positive.
                                       * If A is alphabetically after B, result will be negative. */


            // If A == B
            Stack.DuplicateItem();
            Flow.JumpToLabelIfZero(l4);
            // IF A after B
            Flow.JumpToLabelIfNegative(l6);
            // IF A before B
            Flow.JumpToLabel(l5);

            // IF1 A == B
            Flow.CreateLabel(l4);
            IO.DisplayString("The two characters were the same.");
            Flow.JumpToLabel(l7);

            // IF1 A < B
            Flow.CreateLabel(l5);
            IO.DisplayString("The first character came before the second character.");
            Flow.JumpToLabel(l7);

            // IF1 A > B
            Flow.CreateLabel(l6);
            IO.DisplayString("The first character came after the second character.");
            Flow.JumpToLabel(l7);

            // IF1 END
            Flow.CreateLabel(l7);

            Flow.EndProgram();

            WriteCodeFile();
        }
    }
}
