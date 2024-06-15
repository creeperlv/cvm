using System.Runtime.InteropServices;
namespace cvm.net.core
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct MemoryBlock
	{
		public byte* HEAD;
		//Length Of the Memory Block
		public int Length;
		//Is Read Only
		public bool IsRO;
		//Is Referenced ReadOnly
		public bool IsRefRO;
	}

}