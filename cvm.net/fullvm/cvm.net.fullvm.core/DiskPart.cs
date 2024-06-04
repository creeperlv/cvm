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
	public class DiskPart : Stream
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
		long __pos = 0;
		public override bool CanRead => img.FD.CanRead;

		public override bool CanSeek => img.FD.CanSeek;

		public override bool CanWrite => img.FD.CanWrite;

		public override long Length => PartLength;

		public override long Position
		{
			get
			{
				return __pos;
			}
			set => __pos = value;
		}

		public override void Flush()
		{
			lock (img.FD)
			{
				img.FD.Flush();
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			lock (img.FD)
			{

				img.FD.Position = Offset + __pos;
				return img.FD.Read(buffer, offset, count);
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin:
					__pos = offset;
					return __pos;
				case SeekOrigin.Current:
					__pos += offset;
					return __pos;
				case SeekOrigin.End:
					return __pos = PartLength - offset;
				default:
					return 0;
			}
		}

		public override void SetLength(long value)
		{
			mgr.ResizePart(this, value);
			this.PartLength = value;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			lock (img.FD)
			{
				img.FD.Position = Offset + __pos;
				img.FD.Write(buffer, offset, count);
			}
		}
	}
}
