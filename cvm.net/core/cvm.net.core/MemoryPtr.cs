using System.Runtime.InteropServices;
namespace cvm.net.core
{

    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryPtr
    {
        public int ID;
        public int Offset;
    }

}