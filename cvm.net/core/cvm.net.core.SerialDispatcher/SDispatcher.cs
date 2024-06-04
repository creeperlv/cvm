
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace cvm.net.core.SerialDispatcher
{


	public class SDispatcher : IDispatcher
	{
		bool __run = true;
		public bool StartInThread = false;
		List<CPUCore> Cores = new List<CPUCore>();
		public void Dispatch(CPUCore core)
		{
			Monitor.PulseAll(this);

		}
		public void Stop()
		{
			__run = false;
		}
		bool __IsPlaying = true;
		public void Pause()
		{
			__IsPlaying = false;
		}
		public void Resume()
		{
			__IsPlaying = true;
			Monitor.PulseAll(this);
		}
		public IEnumerable<CPUCore>? GetCPUCores()
		{
			return Cores;
		}
		void Loop()
		{
			while (__run)
			{
				if (Cores.Count == 0 || !__IsPlaying)
				{
					Monitor.Wait(this);
					continue;
				}
				for (int i = 0; i < Cores.Count; i++)
				{
					CPUCore core = Cores[i];
					CVM.Execute(core, this);
				}
			}
		}
		public void StartDispatcher(Machine machine)
		{
			if (StartInThread) { Task.Run(() => Loop()); } else Loop();
		}
	}

}