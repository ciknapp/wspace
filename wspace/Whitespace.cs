using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wspace
{
    public static class Whitespace
    {
        private static int commandCount = 0;

        public static bool IsInitialized { get => isInitialized; }
        private static bool isInitialized = false;

        public  static WSFile WSFile { get => wSFile; }
        private static WSFile wSFile;

        public static void InitializeWhitespace(string name = "", string fileDir = "")
        {
            wSFile = new WSFile(name, fileDir);

            isInitialized = true;
        }

        public static void FinishProgram()
        {
            wSFile.Finish();

            isInitialized = false;
        }

        private static void AssertInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("Whitespace not initialized. Call InitializeWhitespace()");
            }
        }

        public static class Stack
        {
            public const string IMP = " ";

            public static void PushString(string str)
            {
                AssertInitialized();

                char[] reversed = str.ToCharArray();
                Array.Reverse(reversed);

                foreach(char character in reversed)
                {
                    PushCharacter(character);
                }
            }

            public static void PushCharacter(char character)
            {
                AssertInitialized();

                PushNumber(character);
            }

            public static void PushNumber(int number)
            {
                AssertInitialized();

                string command = " ";
                string parameter = string.Empty;

                char[] binaryCharArray = Convert.ToString(number, 2).ToCharArray();

                for (int bit = 0; bit < binaryCharArray.Length; bit++)
                {
                    char currentBit = binaryCharArray[bit];

                    parameter += currentBit == '1' ? "\t" : " ";
                }

                bool negative = number < 0;

                wSFile.FileContents += $"[{(++commandCount)}]{IMP}{command}{(negative ? "\t" : " ")}{parameter}\n";
            }

            internal static void DuplicateItem()
            {
                throw new NotImplementedException();
            }

            internal static void CopyNthItem()
            {
                throw new NotImplementedException();
            }

            internal static void SwapTopItems()
            {
                throw new NotImplementedException();
            }

            internal static void DiscardItem()
            {
                throw new NotImplementedException();
            }

            internal static void SlideNItems()
            {
                throw new NotImplementedException();
            }
        }

        public static class Arithmetic
        {
            public const string IMP = "\t ";

            internal static void Addition()
            {
                throw new NotImplementedException();
            }

            internal static void Division()
            {
                throw new NotImplementedException();
            }

            internal static void Modulo()
            {
                throw new NotImplementedException();
            }

            internal static void Multiplication()
            {
                throw new NotImplementedException();
            }

            internal static void Subtraction()
            {
                throw new NotImplementedException();
            }
        }

        public static class Heap
        {
            public const string IMP = "\t\t";

            internal static void Retrieve()
            {
                throw new NotImplementedException();
            }

            internal static void Store()
            {
                throw new NotImplementedException();
            }
        }

        public static class Flow
        {
            public const string IMP = "\n";

            internal static void CallSubroutine()
            {
                throw new NotImplementedException();
            }

            internal static void CreateLabel()
            {
                throw new NotImplementedException();
            }

            internal static void EndProgram()
            {
                throw new NotImplementedException();
            }

            internal static void EndSubroutine()
            {
                throw new NotImplementedException();
            }

            internal static void JumpToLabel()
            {
                throw new NotImplementedException();
            }

            internal static void JumpToLabelIfNegative()
            {
                throw new NotImplementedException();
            }

            internal static void JumpToLabelIfZero()
            {
                throw new NotImplementedException();
            }
        }

        public static class IO
        {
            public const string IMP = "\t\n";

            public static void OutputNumber(bool withNewLine = false)
            {
                AssertInitialized();

                string command = " \t";

                wSFile.FileContents += $"[{++commandCount}]{IMP}{command}";

                if (withNewLine)
                {
                    Stack.PushNumber(10);
                    OutputCharacter();
                }
            }

            public static void OutputCharacter(bool withNewLine = false)
            {
                AssertInitialized();

                string command = "  ";

                wSFile.FileContents += $"[{++commandCount}]{IMP}{command}";

                if (withNewLine)
                {
                    Stack.PushNumber(10);
                    OutputCharacter();
                }
            }

            public static void DisplayString(string str, bool withNewLine = false)
            {
                AssertInitialized();

                Stack.PushString(str);

                foreach(char _ in str)
                {
                    OutputCharacter();
                }

                if (withNewLine)
                {
                    Stack.PushNumber(10);
                    OutputCharacter();
                }
            }

            internal static void ReadCharacter()
            {
                throw new NotImplementedException();
            }

            internal static void ReadNumber()
            {
                throw new NotImplementedException();
            }
        }
    }
}
