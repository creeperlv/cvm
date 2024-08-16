using System;
using System.Runtime.InteropServices;
namespace cvm.net.core
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Instruction
	{
		public UInt64 D0;
		public UInt64 d0 { get => D0; set => D0 = value; }
	}

}