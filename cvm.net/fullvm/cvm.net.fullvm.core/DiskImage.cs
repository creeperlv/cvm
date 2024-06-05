using cvm.net.core.libc;
using System.IO.Hashing;
using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core
{
	public class DiskImage : IDisposable, IDisk
	{
		public required Stream FD;
		public int DataOffset;
		public DiskMeta metadata;
		public void ResetPos()
		{
			FD.Position = DataOffset;
		}
		public long DiskSize
		{
			get
			{
				unsafe
				{
					return metadata.LBABlockCount * sizeof(LBABlock);
				}
			}
		}

		public unsafe LoadImageResult LoadImageInfo()
		{
			FD.Position = 0;
			Span<byte> byte4 = stackalloc byte[4];
			if (FD.ReadByte() != 'C') return LoadImageResult.WrongHeader;
			if (FD.ReadByte() != 'V') return LoadImageResult.WrongHeader;
			if (FD.ReadByte() != 'M') return LoadImageResult.WrongHeader;
			if (FD.ReadByte() != 'I') return LoadImageResult.WrongHeader;
			if (FD.ReadByte() != 'M') return LoadImageResult.WrongHeader;
			if (FD.ReadByte() != 'G') return LoadImageResult.WrongHeader;
			DataOffset = 6;
			FD.Read(byte4);
			var HeaderLength = byte4.As<int>();
			DataOffset = (int)FD.Position;
			Span<byte> DiskMetaBuffer = stackalloc byte[sizeof(DiskMeta)];
			FD.Read(DiskMetaBuffer);
			metadata = DiskMetaBuffer.As<DiskMeta>();
			DataOffset += HeaderLength;
			return LoadImageResult.Success;
		}
		private GPTPartMgr LoadGPT()
		{
			return new GPTPartMgr(this);
		}
		public unsafe void SetPosToLBA(long LBABlockAddress)
		{
			FD.Position = DataOffset + LBABlockAddress * sizeof(LBABlock);
		}
		public unsafe LBABlock ReadLBA(long LBABlockAddress)
		{
			Span<byte> buf = stackalloc byte[sizeof(LBABlock)];
			FD.Position = DataOffset + LBABlockAddress * sizeof(LBABlock);
			FD.Read(buf);
			return buf.As<LBABlock>();
		}
		public void Flush()
		{
			lock (FD)
			{
				FD.Flush();
			}
		}
		public unsafe void WriteLBA(LBABlock Data, long LBABlockAddress)
		{
			var pos = DataOffset + LBABlockAddress * sizeof(LBABlock);
			var requiredLen = pos + sizeof(LBABlock);
			if (this.FD.Length < requiredLen) this.FD.SetLength(requiredLen);
			FD.Position = pos;
			Span<byte> data = new Span<byte>((byte*)&Data, sizeof(LBABlock));
			lock (FD)
			{
				FD.Write(data);
			}
		}
		public void Dispose() => FD.Dispose();
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct LBABlock
	{
		public fixed byte Data[512];
	}
	public struct DiskMeta
	{
		public uint LBABlockCount;
	}
	public enum LoadImageResult
	{
		Success, WrongHeader
	}
	public class GPTPartMgr
	{
		ulong HEADER = 0x5452415020494645UL;
		public List<DiskPart> Parts = [];
		DiskImage img;
		public Guid DiskID;
		public GPTPartMgr(DiskImage image)
		{
			img = image;
			Load();
		}
		int StartingLBA;
		int EntryCount;
		int Revision;
		long FirstLBA;
		long LastLBA;
		long StartPartHeaderLBA;
		int EntrySize;
		uint EntCRC32;
		unsafe void Load()
		{
			var headerLBA = img.ReadLBA(1);
			byte* ptr = (byte*)&headerLBA;
			if (headerLBA.As<LBABlock, ulong>(0) != HEADER)
			{
				throw new Exception("Not GPT Disk");
			}
			Revision = headerLBA.As<LBABlock, int>(0x08);
			var HeaderSize = headerLBA.As<LBABlock, int>(0x0C);
			var CRC = headerLBA.As<LBABlock, uint>(0x10);
			CString.memset(headerLBA.Data + 0x10, 0, sizeof(uint));
			var _crc = System.IO.Hashing.Crc32.HashToUInt32(new ReadOnlySpan<byte>(ptr, sizeof(LBABlock)));
			if (CRC != _crc)
			{
				throw new Exception("CRC failed on header!");
			}
			if (headerLBA.As<LBABlock, int>(0x14) != 0)
			{
				throw new Exception("Reserved 0x14 is not zero!");
			}
			DiskID = headerLBA.As<LBABlock, Guid>(56);
			FirstLBA = headerLBA.As<LBABlock, long>(0x28);
			LastLBA = headerLBA.As<LBABlock, long>(0x28);
			StartPartHeaderLBA = headerLBA.As<LBABlock, long>(0x48);
			EntryCount = headerLBA.As<LBABlock, int>(0x50);
			EntrySize = headerLBA.As<LBABlock, int>(0x54);
			EntCRC32 = headerLBA.As<LBABlock, uint>(0x58);
			Span<byte> Entries = stackalloc byte[EntryCount * sizeof(LBABlock)];
			fixed (byte* _ptr = Entries)
			{
				LBABlock* entPtr = (LBABlock*)ptr;
				for (int i = 0; i < EntryCount; i++)
				{
					entPtr[i] = img.ReadLBA(StartPartHeaderLBA + i);
				}
			}
			var _EntCRC32 = Crc32.HashToUInt32(Entries);
			if (EntCRC32 != _EntCRC32)
			{
				throw new Exception("CRC failed on enteries!");
			}
			fixed (byte* _ptr = Entries)
			{
				LBABlock* entPtr = (LBABlock*)ptr;
				for (int i = 0; i < EntryCount; i++)
				{
					DiskPart dp = new DiskPart(img, this);
					var data = entPtr + i;
					dp.SetMetadata(((PartationMetadata*)data)[0]);
				}
			}
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
	public class SFSNode
	{
		public ulong NodeID;
		public List<ulong> Children = [];
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SFSNodeBlock
	{
		public ulong NodeID;
		public int NodeCount;
		public fixed ulong Children[62];
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SFSItem
	{
		public ulong ID;
		public bool IsFolder;
		public bool IsLink;
		public uint StartLBA;
		public long Length;
		public fixed byte Name[472];
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
