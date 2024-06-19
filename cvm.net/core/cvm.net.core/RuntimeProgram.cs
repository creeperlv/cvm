using System;
using System.Collections.Generic;
namespace cvm.net.core
{
	public sealed unsafe class RuntimeProgram : IDisposable
	{
		public List<MemoryBlock> MemoryBlocks = new List<MemoryBlock>();
		public List<int> GlobalDataMemoryIDs = new List<int>();
		public List<CVMModule> LoadedModule = new List<CVMModule>();
		public List<IDisposable?> Resources = new List<IDisposable?>();
		public Machine machine;
		public List<ExecuteContext> ExecuteContexts = new List<ExecuteContext>();
		public bool IsKernelMode;
		public RuntimeProgram(Machine machine)
		{
			this.machine = machine;
		}
		public void Exit()
		{
			lock (ExecuteContexts)
			{
				foreach (var item in ExecuteContexts)
				{
					item.WillRun = false;
				}
			}
		}
		public void Dispose()
		{
			foreach (var item in this.LoadedModule)
			{
				machine.ReleaseModule(item.GlobalID);
			}
			for (int i = 0; i < Resources.Count; i++)
			{
				IDisposable? res = Resources[i];
				res?.Dispose();
				Resources[i] = null;
			}
			Resources.Clear();
		}
	}
}