using System;
using System.Runtime.InteropServices;
using static cvm.net.core.libc.StdLib;
namespace cvm.net.core
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CVMBaseModule : IDisposable
	{
		public int Length;
		public int DataSegLength;
		public byte* DataSegment;
		public Instruction* Instructions;
		public int InstructionCount;
		public void StartToUse()
		{
		}

		public void Dispose()
		{
			free(Instructions);
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CVMModule:IDisposable 
	{
		public int LibCount;
		/*
		 * [StrLen][NameStr]
		 */
		public byte* LibTable;
		public int SymbolCount;
		/*
		 * [StrLen][NameStr][0x00:byte][PC:int, <0 = Undefined] 
		 */
		public byte* SymbolTable;
		public int GlobalID;
		public CVMBaseModule BaseModule;

		public void Dispose()
		{
			BaseModule.Dispose();
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct CVMExecutable
	{
		public int FormatVersion;
		public CVMBaseModule Module;
		public bool HasSignature;

	}
}