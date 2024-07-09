using System;
using System.Collections.Generic;
using cvm.net.core.SerialDispatcher;
namespace cvm.net.core.MTDispatcher
{
    public class MTDispatcher : IDispatcher
    {
        public List<SDispatcher> Dispatchers = new List<SDispatcher>();

        public void Dispatch(ExecuteContext core)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExecuteContext>? GetCPUCores()
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