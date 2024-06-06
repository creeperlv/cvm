using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Disk
{
	[StructLayout(LayoutKind.Sequential)]
	public struct GPTHeader
	{
		public ulong HEADER;
		public int Revision;
		public int HeaderSize;
		public uint HEADER_CRC;
		public int Reversed;
		public long CurrentLBA;
		public long BackupLBA;
		public long FirstLBA;
		public long LastLBA;
		public Guid DiskID;
		public long StartingLBAOfPartEnt;
		public int PartCount;
		public int PartSize;
		public uint EnterListCRC;
	}

}
