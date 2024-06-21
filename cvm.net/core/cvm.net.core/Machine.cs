﻿using System.Collections.Generic;
using System.ComponentModel.Design;
namespace cvm.net.core
{

	public delegate void FuncCall(ExecuteContext core);
	public class Machine
	{
		public List<RuntimeProgram> Programs = new List<RuntimeProgram>();
		public IDispatcher dispatcher;
		public Dictionary<int, int> ModuleCounts = new Dictionary<int, int>();
		public Dictionary<int, CVMModule> LoadedModules = new Dictionary<int, CVMModule>();
		public Machine(IDispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
		}
		public void UseModule(int ID)
		{
			if (LoadedModules.ContainsKey(ID))
			{
				if (ModuleCounts.ContainsKey(ID)) { ModuleCounts[ID]++; }
				else
				{
					ModuleCounts.Add(ID, 1);
				}
			}
			else
			{

			}
		}
		public void ReleaseModule(int ID)
		{
			if (LoadedModules.ContainsKey(ID))
			{
				if (ModuleCounts.ContainsKey(ID))
				{
					ModuleCounts[ID]--;
					if (ModuleCounts[ID] <= 0)
					{
						LoadedModules[ID].Dispose();
						LoadedModules.Remove(ID);
					}
				}
				else
				{
					LoadedModules[ID].Dispose();
					LoadedModules.Remove(ID);
				}
			}
			else
			{
				if (!ModuleCounts.ContainsKey(ID))
				{
					ModuleCounts.Remove(ID);
				}
			}
		}
		public Dictionary<uint, Callframe> GlobalInterrupts = new Dictionary<uint, Callframe>();
		public Dictionary<int, FuncCall> Calls = new Dictionary<int, FuncCall>();
		//public List<ExecuteContext> Contexts = new List<ExecuteContext>();
	}
	public interface IDispatcher
	{
		void StartDispatcher(Machine machine);
		void Dispatch(ExecuteContext core);
		IEnumerable<ExecuteContext>? GetCPUCores();
	}
}