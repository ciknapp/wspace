using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wspace
{
    static class WSCommands
    {
        public enum Categories
        {
            Stack,
            IO,
            Flow,
            Math,
            Heap
        }

        public static Dictionary<Categories, string> IMPs = new Dictionary<Categories, string>() { 
            { Categories.Stack, Whitespace.Stack.IMP },
            { Categories.IO,    Whitespace.IO.IMP},
            { Categories.Flow,  Whitespace.Flow.IMP},
            { Categories.Math,  Whitespace.Arithmetic.IMP},
            { Categories.Heap,  Whitespace.Heap.IMP}
        };

        public enum StackCommand
        {
            PushNumber,
            Duplicate,
            CopyNth,
            Swap,
            Discard,
            SlideN
        }
    }
}
