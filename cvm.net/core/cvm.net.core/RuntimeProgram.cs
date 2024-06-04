using System;
using System.Collections.Generic;
using static cvm.net.core.libc.StdLib;
namespace cvm.net.core
{
	public sealed unsafe class RuntimeProgram : IDisposable
	{
		public List<MemoryBlock> MemoryBlocks = new List<MemoryBlock>();
		public CVMModule module;
		public List<IDisposable?> Resources = new List<IDisposable?>();

		public void Dispose()
		{
			for (int i = 0; i < Resources.Count; i++)
			{
				IDisposable? res = Resources[i];
				res?.Dispose();
				Resources[i] = null;
			}
			Resources.Clear();
		}
	}
	public unsafe struct CVMModule:IDisposable
	{
		public Instruction* Instructions;
		public int InstructionCount;
		public int RefCount;

		public void Dispose()
		{
			free(Instructions);
		}
	}
}