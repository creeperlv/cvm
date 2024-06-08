using cvm.net.core;

namespace cvm.net.fullvm.core.Disk
{
	public class RootFS : IFileSystem
    {
        public void Mkdir(string path)
        {
            throw new NotImplementedException();
        }

        public FSItem Open(string path, string mode)
        {
            throw new NotImplementedException();
        }

        public FSItem OpenCVM(Machine machine, RuntimeProgram program, MemoryPtr basePath, MemoryPtr path, MemoryPtr Mode)
        {
            throw new NotImplementedException();
        }

        public void Rmdir(string path, bool Recursive)
        {
            throw new NotImplementedException();
        }
    }
}
