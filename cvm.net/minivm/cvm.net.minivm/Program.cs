using cvm.net.core;
using cvm.net.core.MTDispatcher;

namespace cvm.net.minivm;

class Program
{
    static void Main(string[] args)
    {
        Machine machine = new Machine(new MTDispatcher());
    }
}
