using System.Collections.Generic;
using System.ComponentModel.Design;
namespace cvm.net.core
{

	public delegate void FuncCall(ExecuteContext core);
	public class Machine
	{
		public MachineMode WorkingMode;
		public List<RuntimeProgram> Programs = new List<RuntimeProgram>();
		public IDispatcher dispatcher;
		public Dictionary<int, int> ModuleCounts = new Dictionary<int, int>();
		public Dictionary<int, CVMBaseModule> LoadedModules = new Dictionary<int, CVMBaseModule>();
		public Dictionary<int, IODefinition> IOPorts = new Dictionary<int, IODefinition>();
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
		public void RaiseInterrupt(ExecuteContext context, uint Interrupt)
		{
			if (this.GlobalInterrupts.TryGetValue(Interrupt, out var frame))
			{

			}
		}
		public Dictionary<uint, GloabalCallframe> GlobalInterrupts = new Dictionary<uint, GloabalCallframe>();
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