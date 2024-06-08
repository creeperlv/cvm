using cvm.net.core;

namespace cvm.net.fullvm.core.Disk
{
	public unsafe interface IFileSystem
    {
		FSItem Open(string path, string mode);
        void Mkdir(string path);
        void Rmdir(string path, bool Recursive);
		FSItem OpenCVM(Machine machine, RuntimeProgram program, MemoryPtr basePath, MemoryPtr path, MemoryPtr Mode);

    }
}
