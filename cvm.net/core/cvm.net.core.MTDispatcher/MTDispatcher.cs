using System;
using System.Collections.Generic;
using cvm.net.core.SerialDispatcher;
namespace cvm.net.core.MTDispatcher
{
    public class MTDispatcher : IDispatcher
    {
        public List<SDispatcher> Dispatchers = new List<SDispatcher>();

        public void Dispatch(CPUCore core)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CPUCore>? GetCPUCores()
        {
            foreach (var core in Dispatchers)
            {
                var lscpu = core.GetCPUCores();
                if (lscpu != null)
                    foreach (var cpu in lscpu)
                    {
                        yield return cpu;
                    }
            }
        }

        public void StartDispatcher(Machine machine)
        {
            throw new NotImplementedException();
        }
    }

}