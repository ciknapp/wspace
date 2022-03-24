using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wspace
{
    public static class Whitespace
    {
        private static int commandCount = -1;

        public static bool IsInitialized { get => isInitialized; }
        private static bool isInitialized = false;

        public  static WSFile WSFile { get => wSFile; }
        private static WSFile wSFile;

        private static Stack<int> SimStack = new Stack<int>();

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

        public static void Parse(string code)
        {
            int commandNum = 0;
            string category;
            string imp;
            string command;
            string parameter;

            code = Regex.Replace(code, "[^ \t\n]", "");

            for(int i = 0; i < code.Length; i++)
            {
                category = imp = command = parameter = string.Empty;

                if(code[i] == ' ')
                {
                    // Stack
                    category = "Stack";
                    imp = $"{code[i]}";

                    i++;

                    // Find command
                    if(code[i] == ' ')
                    {
                        // Push number
                        command = "Push Number";

                        i++;

                        // Get the parameter (read til newline)
                        do
                        {
                            parameter += code[i].ToString();
                            i++;
                        } while (code[i] != '\n');

                        // Successful parse, move on to next command.
                        Console.WriteLine($"{category} {command} [{parameter}]");
                        continue;
                    }
                    else if(code[i] == '\n')
                    {
                        switch(code[i + 1])
                        {
                            case ' ':
                                // Duplicate
                                break;
                            case '\t':
                                // Swap
                                break;
                            case '\n':
                                // Discard
                                break;
                        }
                    }
                    else if(code[i] == '\t')
                    {

                    }
                }
                else if(code[i] == '\t')
                {
                    if(code[i + 1] == ' ')
                    {
                        // Math
                        category = "Math";
                        imp = $"{code[i]}{code[i + 1]}";
                    }
                    else if(code[i + 1] == '\t')
                    {
                        // Heap
                        category = "Heap";
                        imp = $"{code[i]}{code[i + 1]}";
                    }
                    else if(code[i + 1] == '\n')
                    {
                        // IO
                        category = "IO";
                        imp = $"{code[i]}{code[i + 1]}";
                    }
                }
                else if(code[i] == '\n')
                {
                    // Flow control
                    category = "Flow";
                    imp = $"{code[i]}";
                }
                else
                {
                    // Not whitespace
                }
            }

            Console.WriteLine("No syntax errors in file.");
        }

        /// <summary>
        /// Holds methods related to Stack Management.
        /// </summary>
        public static class Stack
        {
            public const string IMP = " ";
            public const string CMD_PUSHNUM = " ";
            public const string CMD_DUPLICATE = "\n ";
            public const string CMD_COPYNTH = "\t ";

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

                string parameter = getWsBitString(number);

                bool negative = number < 0;

                SimStack.Push(number);

                wSFile.FileContents += $"[PushNumber][{(++commandCount)}]{IMP}{CMD_PUSHNUM}{(negative ? "\t" : " ")}{parameter}\n";
            }

            internal static void DuplicateItem()
            {
                assertInitialized();

                if(SimStack.TryPeek(out int stackNumber))
                {
                    SimStack.Push(stackNumber);
                }
                else
                {
                    throw new Exception("Tried to duplicate, but there was no number on the stack to duplicate.");
                }

                WSFile.FileContents += $"[DuplicateItem][{++commandCount}]{IMP}{CMD_DUPLICATE}";
            }

            internal static void CopyNthItem(int locationInStack)
            {
                assertInitialized();

                string parameter = getWsBitString(locationInStack);

                WSFile.FileContents += $"[CopyNthItem][{++commandCount}]{IMP}{CMD_COPYNTH}{parameter}\n";
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
                else if(SimStack.Count < 2)
                {
                    throw new Exception($"Tried to add the top two items on the stack, but there were not enough items. Only [{SimStack.Count}] item{(SimStack.Count == 0 ? "s" : "")} on the stack.");
                }
                else
                {
                    left = SimStack.Pop();
                    right = SimStack.Pop();

                    SimStack.Push(left ?? 0 + right ?? 0);

                    WSFile.FileContents += $"[Addition][{++commandCount}]{IMP}{command}";
                }
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

                if(SimStack.Count < 2)
                {
                    throw new Exception("Not enough items on the stack to subtract.");
                }
                else
                {
                    SimStack.Push(SimStack.Pop() - SimStack.Pop());
                    WSFile.FileContents += $"[Subtraction][{++commandCount}]{IMP}{command}";
                }
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

                WSFile.FileContents += $"[CreateLabel]{IMP}{command}{parameter}\n";
            }

            internal static void EndProgram()
            {
                assertInitialized();

                string command = "\n\n";

                WSFile.FileContents += $"[EndProgram]{IMP}{command}";
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

                WSFile.FileContents += $"[JumpToLabel][{++commandCount}]{IMP}{command}{parameter}\n";
            }

            internal static void JumpToLabelIfNegative(string labelName)
            {
                assertInitialized();

                string command = "\t\t";
                string parameter = generateLabelString(labelName);

                if(SimStack.Count > 0)
                {
                    WSFile.FileContents += $"[JumpToLabelIfNegative][{++commandCount}]{IMP}{command}{parameter}\n";
                }
                else
                {
                    throw new Exception("No number on the stack to check if negative. Have you forgotten to duplicate the stack?");
                }
            }

            internal static void JumpToLabelIfZero(string labelName)
            {
                assertInitialized();

                string command = "\t ";
                string parameter = generateLabelString(labelName);

                if(SimStack.Count > 0)
                {
                    SimStack.Pop();
                    WSFile.FileContents += $"[JumpToLabelIfZero][{++commandCount}]{IMP}{command}{parameter}\n";
                }
                else
                {
                    throw new Exception("No number on the stack to check if zero. Have you forgotten to duplicate the stack?");
                }
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

                if(SimStack.Count > 0)
                {
                    SimStack.Pop();
                    wSFile.FileContents += $"[OutputNumber][{++commandCount}]{IMP}{command}";

                    if (withNewLine)
                    {
                        Stack.PushNumber(10);
                        OutputCharacter(false);
                    }
                }
                else
                {
                    throw new Exception("No number on the stack to output.");
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

                if(SimStack.Count > 0)
                {
                    wSFile.FileContents += $"[OutputCharacter][{++commandCount}]{IMP}{command}";

                    if (withNewLine)
                    {
                        Stack.PushNumber(10);
                        OutputCharacter(false);
                    }
                }
                else
                {
                    throw new Exception("No character on the stack to output.");
                }
            }

            /// <summary>
            /// Pushes a string to the stack and then displays it.
            /// </summary>
            /// <param name="str">String to display</param>
            /// <param name="withNewLine">If true, a new line is printed after the string. Defaults to true</param>
            internal static void DisplayString(string str, bool withNewLine = true)
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
