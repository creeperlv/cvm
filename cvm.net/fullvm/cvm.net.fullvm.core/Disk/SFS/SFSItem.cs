using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Disk.SFS
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SFSItem
	{
		public Guid ID;
		public bool IsFolder;
		public bool IsLink;
		public ulong StartLBA;
		public long Length;
		public fixed byte Name[456];
		public long CreateTime;
		public long ModifyTime;
		public static SFSItem ReadNode(Stream stream)
		{
			var pos = stream.Position;
			Span<byte> Buffer = stackalloc byte[sizeof(SFSItem)];
			stream.Read(Buffer);
			return Buffer.As<SFSItem>();
		}
	}
}
