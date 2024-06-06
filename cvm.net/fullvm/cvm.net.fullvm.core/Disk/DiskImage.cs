using cvm.net.fullvm.core.Data;
using System.Runtime.InteropServices;

namespace cvm.net.fullvm.core.Disk
{
	public class DiskImage : IDisposable, IDisk
	{
		public required Stream FD;
		public long DataOffset;
		public DiskMeta metadata;
		public void ResetPos()
		{
			FD.Position = DataOffset;
		}
		public ulong DiskSize
		{
			get
			{
				unsafe
				{
					return metadata.LBABlockCount * (uint)sizeof(LBABlock);
				}
			}
		}
		public static unsafe void CreateNewImage(Stream stream, DiskMeta meta)
		{
			stream.Position = 0;
			byte[] bytes = [(byte)'C', (byte)'V', (byte)'M', (byte)'I', (byte)'M', (byte)'G'];
			stream.Write(bytes);
			Span<byte> byte4 = stackalloc byte[4];
			{
				fixed (byte* ptr = byte4)
				{
					((int*)ptr)[0] = sizeof(DiskMeta);
				}
			}
			stream.Write(byte4);
			Span<byte> metaDataBytes = stackalloc byte[sizeof(DiskMeta)];
			{
				fixed (byte* ptr = metaDataBytes)
				{
					((DiskMeta*)ptr)[0] = meta;
				}
			}
			stream.Write(metaDataBytes);
			stream.Flush();
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
			DataOffset = FD.Position;
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
			if (FD.Length < requiredLen) FD.SetLength(requiredLen);
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
		public UnmanagedVersion ImgVersion;
		public ulong LBABlockCount;
	}
	public enum LoadImageResult
	{
		Success, WrongHeader
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
