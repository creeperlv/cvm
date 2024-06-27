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
		public static UnmanagedVersion FromVersion(Version v)
		{
			return new UnmanagedVersion() { Major = v.Major, Minor = v.Minor, Build = v.Build, Patch = v.Revision };
		}
		public Version ToVersion() => new(Major, Minor, Build, Patch);
	}
}
