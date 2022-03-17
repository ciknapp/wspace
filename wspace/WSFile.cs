using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wspace
{
    public class WSFile
    {
        public const string Extension = "ws";

        public const string GeneratedCodeFolderName = "Generated Whitespace Code";

        public WSFile(string name = "", string fileDir = "")
        {
            if (string.IsNullOrEmpty(name.Trim()))
            {
                Name = $"Whitespace{DateTime.Now.ToString("HHmmssyyyyMMdd")}.{Extension}";
            }
            else
            {
                Name = $"{name}.{Extension}";
            }

            if (string.IsNullOrEmpty(fileDir.Trim()))
            {
                if(Directory.Exists($"{Directory.GetCurrentDirectory()}\\{GeneratedCodeFolderName}") == false)
                {
                    Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\{GeneratedCodeFolderName}");
                }

                FullFileDir = $"{Directory.GetCurrentDirectory()}\\{GeneratedCodeFolderName}\\{name}";
            }
            else
            {
                FullFileDir = $"{fileDir}\\{name}";
            }
        }

        internal void Finish()
        {
            FileContents += "\n\n\n";

            File.WriteAllText(FullFileDir, FileContents);
        }

        public string FileContents { get; set; }

        public string FullFileDir { get; }

        public string Name { get; }
    }
}
