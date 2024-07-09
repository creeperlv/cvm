using cvm.net.core.libc;
using System.IO.Hashing;

namespace cvm.net.fullvm.core.Disk
{
	public class GPTPartMgr
	{
		public List<DiskPart> Parts = [];
		DiskImage img;
		public GPTHeader header;
		public GPTPartMgr(DiskImage image)
		{
			img = image;
		}
		public unsafe void Load()
		{
			var headerLBA = img.ReadLBA(1);
			byte* ptr = (byte*)&headerLBA;
			header = headerLBA.As<LBABlock, GPTHeader>();
			if (header.HEADER != DiskDefinitions.GPTHeader)
			{
				throw new Exception("Not GPT Disk");
			}
			CString.memset(headerLBA.Data + 0x10, 0, sizeof(uint));
			var _crc = Crc32.HashToUInt32(new ReadOnlySpan<byte>(ptr, sizeof(LBABlock)));
			if (header.HEADER_CRC != _crc)
			{
				throw new Exception("CRC failed on header!");
			}
			if (header.Reversed != 0)
			{
				throw new Exception("Reserved Field is not zero!");
			}
			if (header.PartCount > 0)
			{

				Span<LBABlock> Entries = stackalloc LBABlock[header.PartCount];

				{
					for (int i = 0; i < header.PartCount; i++)
					{
						Entries[i] = img.ReadLBA(header.StartingLBAOfPartEnt + i);
					}
				}
				fixed (LBABlock* blkPtr = Entries)
				{
					var _EntCRC32 = Crc32.HashToUInt32(new Span<byte>((byte*)blkPtr, (int)header.PartCount * sizeof(LBABlock)));
					if (header.EnterListCRC != _EntCRC32)
					{
						throw new Exception("CRC failed on enteries!");
						//Console.WriteLine("CRC failed on enteries!");
					}
				}
				{
					fixed (LBABlock* blkPtr = Entries)
					{

						for (int i = 0; i < header.PartCount; i++)
						{
							DiskPart dp = new DiskPart(img, this);
							var data = blkPtr + i;
							dp.SetMetadata(((PartitionMetadata*)data)[0]);
							this.Parts.Add(dp);
						}
					}
				}
			}
		}
		public unsafe void WriteTableMeta()
		{
			var partCount = Parts.Count;
			this.header.StartingLBAOfPartEnt = 2;
			Span<LBABlock> Entries = stackalloc LBABlock[partCount];
			this.header.PartCount = partCount;
			{
				fixed (LBABlock* ptr = Entries)
				{
					for (int i = 0; i < partCount; i++)
					{
						var ptr_ = ptr + i;
						((PartitionMetadata*)ptr_)[0] = Parts[i].metadata;
					}
					Span<byte> bytes = new Span<byte>((byte*)ptr, partCount * sizeof(LBABlock));
					var _crc = Crc32.HashToUInt32(bytes);
					header.EnterListCRC = _crc;
				}
			}
			{
				this.header.HEADER = DiskDefinitions.GPTHeader;
				LBABlock headerBlock = new LBABlock();
				var ptr_ = &headerBlock;
				header.HEADER_CRC = 0;
				((GPTHeader*)ptr_)[0] = header;
				var _crc = Crc32.HashToUInt32(new ReadOnlySpan<byte>(ptr_, sizeof(LBABlock)));
				header.HEADER_CRC = _crc;
				((GPTHeader*)ptr_)[0] = header;
				img.WriteLBA(headerBlock, 1);
			}
			for (int i = 0; i < partCount; i++)
			{
				img.WriteLBA(Entries[i], header.StartingLBAOfPartEnt + i);
			}
			img.Flush();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="part"></param>
		/// <param name="newSize"></param>
		public void ResizePart(DiskPart part, long newSize)
		{

		}
	}
}
