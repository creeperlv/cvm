using cvm.net.fullvm.core.Data;

namespace cvm.net.fullvm.core.Disk
{
	public static class DiskDefinitions
	{
		public const int LBASectorSize = 512;
		public const ulong GPTHeader = 0x5452415020494645UL;
		public static readonly UnmanagedVersion CurrentCVMIMGVersion = new() { Major = 1, Minor = 0, Build = 0, Patch = 0 };
	}
}
