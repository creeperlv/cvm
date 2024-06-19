using System;
using System.Runtime.InteropServices;
using static cvm.net.core.libc.StdLib;
namespace cvm.net.core
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CVMModule : IDisposable
	{
		public byte* DataSegment;
		public int Length;
		public Instruction* Instructions;
		public int InstructionCount;
		public int GlobalID;
		public void StartToUse()
		{
		}

		public void Dispose()
		{
			free(Instructions);
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CVMExecutable
	{
		public int FormatVersion;
		public CVMModule Module;
		public bool HasSignature;

	}
}