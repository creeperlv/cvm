using System.Runtime.InteropServices;
namespace cvm.net.core
{
	[StructLayout(LayoutKind.Sequential)]
    public struct GlobalMemoryPtr
    {
        public int RTID;
        public int MEMID;
        public int Offsed;
    }
}