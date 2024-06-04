using System.Runtime.InteropServices;
namespace cvm.net.core
{


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryBlock
    {
        public byte* HEAD;
        public int Length;
    }

}