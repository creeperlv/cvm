using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PartitionMetadata
    {
        public Guid PartType;
        public Guid PartID;
        public long FirstLBA;
        public long LastLBA;
        public ulong Attributes;
        public fixed byte Name[72];
    }
}
