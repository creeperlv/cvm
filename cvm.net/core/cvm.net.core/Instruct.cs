using System;
using System.Runtime.InteropServices;
namespace cvm.net.core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Instruction
    {
        public UInt64 D0;
        public UInt64 D1;
    }

}