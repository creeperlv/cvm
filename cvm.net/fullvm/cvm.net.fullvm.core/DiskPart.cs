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
	public class DiskPart : IDisk
	{
		public DiskImage img;
		public GPTPartMgr mgr;
		public PartationMetadata metadata;
		public DiskPart(DiskImage img, GPTPartMgr mgr)
		{
			this.img = img;
			this.mgr = mgr;
		}
		public unsafe void SetMetadata(PartationMetadata metadata)
		{
			this.metadata = metadata;
			Offset = img.DataOffset + metadata.FirstLBA * sizeof(LBABlock);
			PartLength = (metadata.LastLBA - metadata.FirstLBA) * sizeof(LBABlock);
		}
		public long Offset;
		internal long PartLength;
		long __LBAAddress = 0;
		public bool CanRead => img.FD.CanRead;
		public bool CanSeek => img.FD.CanSeek;
		public bool CanWrite => img.FD.CanWrite;
		public long Length => PartLength;

		public long Position
		{
			get
			{
				return __LBAAddress;
			}
			set => __LBAAddress = value;
		}

		public void Flush()
		{
			lock (img.FD)
			{
				img.FD.Flush();
			}
		}


		public void SetLength(long value)
		{
			mgr.ResizePart(this, value);
			this.PartLength = value;
		}


		public void SetPosToLBA(long LBABlockAddress)
		{
			__LBAAddress = LBABlockAddress;
		}

		public void WriteLBA(LBABlock Data, long LBABlockAddress)
		{
			img.WriteLBA(Data, Offset + LBABlockAddress);
		}

		public LBABlock ReadLBA(long LBABlockAddress)
		{
			return img.ReadLBA(Offset + LBABlockAddress);
		}
	}
}
