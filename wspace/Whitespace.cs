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

        /// <summary>
        /// Creates a WSFile to write the finished Whitespace code into. Extension will be ".ws".
        /// </summary>
        /// <param name="name">Name of the file (not including extension)</param>
        /// <param name="fileDir">Directory of the file</param>
        internal static void InitializeWhitespace(string name = "", string fileDir = "")
        {
            wSFile = new WSFile(name, fileDir);

            isInitialized = true;
        }

        /// <summary>
        /// Writes the End Program code to the WSFile content and saves the file. Overwrites existing file with same name.
        /// </summary>
        internal static void WriteCodeFile()
        {
            wSFile.Finish();

            isInitialized = false;
        }

        /// <summary>
        /// Ensures that the WSFile has been initialized.
        /// </summary>
        private static void assertInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("Whitespace not initialized. Call InitializeWhitespace()");
            }
        }

        private static string getWsBitString(int number)
        {
            string bitString = string.Empty;

            char[] binaryCharArray = Convert.ToString(number, 2).ToCharArray();

            for (int bit = 0; bit < binaryCharArray.Length; bit++)
            {
                char currentBit = binaryCharArray[bit];

                bitString += currentBit == '1' ? "\t" : " ";
            }

            return bitString;
        }

        /// <summary>
        /// Holds methods related to Stack Management.
        /// </summary>
        public static class Stack
        {
            public const string IMP = " ";

            /// <summary>
            /// Reverses a string and pushes each character onto the stack. Not meant to be used independently. DisplayString is the better option.
            /// </summary>
            /// <param name="str">String to push</param>
            internal static void PushString(string str)
            {
                assertInitialized();

                char[] reversed = str.ToCharArray();
                Array.Reverse(reversed);

                foreach(char character in reversed)
                {
                    PushCharacter(character);
                }
            }

            /// <summary>
            /// Pushes a character onto the stack.
            /// </summary>
            /// <param name="character">Character to push</param>
            internal static void PushCharacter(char character)
            {
                assertInitialized();

                PushNumber(character);
            }

            /// <summary>
            /// Pushes an integer number onto the stack.
            /// </summary>
            /// <param name="number">Integer number to push onto the stack</param>
            internal static void PushNumber(int number)
            {
                assertInitialized();

                string command = " ";
                string parameter = getWsBitString(number);

                bool negative = number < 0;

                wSFile.FileContents += $"[{(++commandCount)}]{IMP}{command}{(negative ? "\t" : " ")}{parameter}\n";
            }

            internal static void DuplicateItem()
            {
                assertInitialized();

                string command = "\n ";

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}";
            }

            internal static void CopyNthItem(int locationInStack)
            {
                assertInitialized();

                string command = "\t ";
                string parameter = getWsBitString(locationInStack);

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}{parameter}\n";
            }

            internal static void SwapTopItems()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void DiscardItem()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void SlideNItems(int numberOfItems)
            {
                assertInitialized();

                throw new NotImplementedException();
            }
        }

        public static class Arithmetic
        {
            public const string IMP = "\t ";

            internal static void Addition(int? left = null, int? right = null)
            {
                assertInitialized();

                string command = "  ";

                if(left != null && right != null)
                {
                    Stack.PushNumber(right ?? 0);
                    Stack.PushNumber(left ?? 0);
                }

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}";
            }

            internal static void Division()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void Modulo()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void Multiplication()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void Subtraction()
            {
                assertInitialized();

                string command = " \t";

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}";
            }
        }

        public static class Heap
        {
            public const string IMP = "\t\t";

            internal static void Retrieve(int address)
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void Store(int address, int value)
            {
                assertInitialized();

                throw new NotImplementedException();
            }
        }

        public static class Flow
        {
            public const string IMP = "\n";

            public static Dictionary<string, string> Labels = new Dictionary<string, string>();

            private static string generateLabelString(string labelName)
            {
                char[] nameCharArray = labelName.ToCharArray();
                string nameBinaryString = string.Empty;

                foreach (char character in nameCharArray)
                {
                    string binary = Convert.ToString(character, 2);

                    foreach (char bit in binary)
                    {
                        nameBinaryString += $"{(bit == '1' ? '\t' : ' ')}";
                    }
                }

                return nameBinaryString;
            }

            internal static void CallSubroutine(string labelName)
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void CreateLabel(string labelName)
            {
                assertInitialized();

                string command = "  ";
                string parameter = generateLabelString(labelName);

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}{parameter}\n";
            }

            internal static void EndProgram()
            {
                assertInitialized();

                string command = "\n\n";

                WSFile.FileContents += $"[END]{IMP}{command}";
            }

            internal static void EndSubroutine()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void JumpToLabel(string labelName)
            {
                assertInitialized();

                string command = " \n";
                string parameter = generateLabelString(labelName);

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}{parameter}\n";
            }

            internal static void JumpToLabelIfNegative(string labelName)
            {
                assertInitialized();

                string command = "\t\t";
                string parameter = generateLabelString(labelName);

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}{parameter}\n";
            }

            internal static void JumpToLabelIfZero(string labelName)
            {
                assertInitialized();

                string command = "\t ";
                string parameter = generateLabelString(labelName);

                WSFile.FileContents += $"[{++commandCount}]{IMP}{command}{parameter}\n";
            }
        }

        public static class IO
        {
            public const string IMP = "\t\n";
            
            /// <summary>
            /// Displays a number from the top of the stack through stdout. Pops the top number off of the stack.
            /// </summary>
            /// <param name="withNewLine">If true, prints a new line after printing the number</param>
            internal static void OutputNumber(bool withNewLine = true)
            {
                assertInitialized();

                string command = " \t";

                wSFile.FileContents += $"[{++commandCount}]{IMP}{command}";

                if (withNewLine)
                {
                    Stack.PushNumber(10);
                    OutputCharacter(false);
                }
            }
            
            /// <summary>
            /// Displays a character from the number off the top of the stack. Pops the number off the stack.
            /// </summary>
            /// <param name="withNewLine">If true, prints a new line after printing the character</param>
            internal static void OutputCharacter(bool withNewLine = true)
            {
                assertInitialized();

                string command = "  ";

                wSFile.FileContents += $"[{++commandCount}]{IMP}{command}";

                if (withNewLine)
                {
                    Stack.PushNumber(10);
                    OutputCharacter(false);
                }
            }

            /// <summary>
            /// Pushes a string to the stack and then displays it.
            /// </summary>
            /// <param name="str">String to display</param>
            /// <param name="withNewLine">If true, a new line is printed after the string. Defaults to false</param>
            internal static void DisplayString(string str, bool withNewLine = false)
            {
                assertInitialized();

                Stack.PushString(str);

                foreach(char _ in str)
                {
                    OutputCharacter(false);
                }

                if (withNewLine)
                {
                    Stack.PushNumber(10);
                    OutputCharacter(false);
                }
            }

            internal static void ReadCharacter()
            {
                assertInitialized();

                throw new NotImplementedException();
            }

            internal static void ReadNumber()
            {
                assertInitialized();

                throw new NotImplementedException();
            }
        }
    }
}
