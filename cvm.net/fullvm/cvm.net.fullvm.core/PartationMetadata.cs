using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct PartationMetadata
	{
		public Guid PartType;
		public Guid PardID;
		public long FirstLBA;
		public long LastLBA;
		public ulong Attributes;
		public fixed byte Name[72];
	}
}
