using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wspace
{
    public static class Whitespace
    {
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
        }

        public static class Stack
        {
            public static void PushNumber(int number)
            {
                char[] binaryString = Convert.ToString(number, 2).ToCharArray();

                for(int bit = 0; bit < binaryString.Length; bit++)
                {
                    char currentBit = binaryString[bit];

                    binaryString[bit] = currentBit == '1' ? '\t' : ' ';
                }

                bool negative = number < 0;

                wSFile.FileContents += $"  {(negative ? "\t" : " ")}{binaryString}";
            }
        }
    }
}
