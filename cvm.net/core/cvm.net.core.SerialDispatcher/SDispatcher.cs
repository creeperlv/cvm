
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace cvm.net.core.SerialDispatcher
{


	public class SDispatcher : IDispatcher
	{
		bool __run = true;
		public bool StartInThread = false;
		List<ExecuteContext> Cores = new List<ExecuteContext>();
		public void Dispatch(ExecuteContext core)
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
		public IEnumerable<ExecuteContext>? GetCPUCores()
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
				for (int i = Cores.Count - 1; i >= 0; i--)
				{
					ExecuteContext core = Cores[i];
					if (core.WillRun)
						CVM.Execute(core, this);
					else Cores.RemoveAt(i);
				}
			}
		}
		public void StartDispatcher(Machine machine)
		{
			if (StartInThread) { Task.Run(() => Loop()); } else Loop();
		}
	}

}