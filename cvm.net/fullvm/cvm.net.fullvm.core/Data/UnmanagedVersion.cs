using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Data
{
	[StructLayout(LayoutKind.Sequential)]
	public struct UnmanagedVersion
	{
		public int Major;
		public int Minor;
		public int Build;
		public int Patch;
	}
}
