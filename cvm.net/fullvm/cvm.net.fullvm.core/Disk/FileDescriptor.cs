using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Disk
{
	[StructLayout(LayoutKind.Sequential)]
    public struct FileDescriptor
    {
        public ulong ID;
        public ulong FilesystemID;
    }
}
