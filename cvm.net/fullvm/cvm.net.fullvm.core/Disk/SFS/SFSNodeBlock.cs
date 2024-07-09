using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Disk.SFS
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SFSNodeBlock
    {
        public ulong NodeID;
        public int NodeCount;
        public fixed ulong Children[62];
    }
}
