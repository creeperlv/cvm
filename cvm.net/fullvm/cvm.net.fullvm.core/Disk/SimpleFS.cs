using cvm.net.core;

namespace cvm.net.fullvm.core.Disk
{
    public class SimpleFS
    {
        IDisk disk;

        public SimpleFS(IDisk part)
        {
            disk = part;
            Load();
        }
        public SFSNode? rootNode;
        public Dictionary<ulong, SFSItem> items = [];
        public Dictionary<ulong, SFSNode> nodes = [];
        private unsafe void Load()
        {
            int offset = 0;
            var Data = disk.ReadLBA(offset + 0);
            offset++;
            int ItemCount = Data.As<LBABlock, int>(0);
            int NodeCount = Data.As<LBABlock, int>(0);
            for (int i = 0; i < ItemCount; i++)
            {
                Data = disk.ReadLBA(offset);
                SFSItem item = Data.As<LBABlock, SFSItem>(0);
                items.Add(item.ID, item);
                offset++;
            }
            for (int i = 0; i < NodeCount; i++)
            {
                Data = disk.ReadLBA(offset);
                SFSNodeBlock item = Data.As<LBABlock, SFSNodeBlock>(0);
                SFSNode node;
                if (nodes.ContainsKey(item.NodeID))
                {
                    node = nodes[item.NodeID];
                }
                else
                {
                    node = new SFSNode();
                    nodes.Add(item.NodeID, node);
                }
                for (int __i = 0; __i < item.NodeCount; __i++)
                {
                    node.Children.Add(item.Children[i]);
                }
                offset++;
            }

        }
    }
    public class RootFS : IFileSystem
    {
        public void Mkdir(string path)
        {
            throw new NotImplementedException();
        }

        public AbstractFile Open(string path, string mode)
        {
            throw new NotImplementedException();
        }

        public AbstractFile OpenCVM(Machine machine, RuntimeProgram program, MemoryPtr basePath, MemoryPtr path, MemoryPtr Mode)
        {
            throw new NotImplementedException();
        }

        public void Rmdir(string path, bool Recursive)
        {
            throw new NotImplementedException();
        }
    }
    public unsafe interface IFileSystem
    {
        AbstractFile Open(string path, string mode);
        void Mkdir(string path);
        void Rmdir(string path, bool Recursive);
        AbstractFile OpenCVM(Machine machine, RuntimeProgram program, MemoryPtr basePath, MemoryPtr path, MemoryPtr Mode);

    }
    public class FSFuncGroup(Action flush,
                          Action dispose,
                          Action<long> setLength,
                          Func<byte[], int, int, int> write,
                          Func<nint, int, int, int> writePtr,
                          Func<byte[], int, int, int> read,
                          Func<nint, int, int, int> readPtr,
                          Func<long> fileLength)
    {
        public bool CanRead;
        public bool CanWrite;
        public bool CanSeek;
        public Action Flush = flush;
        public Action Dispose = dispose;
        public Action<long> SetLength = setLength;
        public Func<long> FileLength = fileLength;
        public Func<byte[], int, int, int> Write = write;
        public Func<nint, int, int, int> WritePtr = writePtr;
        public Func<byte[], int, int, int> Read = read;
        public Func<nint, int, int, int> ReadPtr = readPtr;
    }
    public class AbstractFile : Stream
    {
        FSFuncGroup fsfuncs;
        public AbstractFile(FSFuncGroup FSFuncs)
        {
            fsfuncs = FSFuncs;
        }

        public override bool CanRead => fsfuncs.CanRead;

        public override bool CanSeek => fsfuncs.CanSeek;

        public override bool CanWrite => fsfuncs.CanWrite;

        public override long Length => fsfuncs.FileLength();
        long __position = 0;
        public override long Position { get => __position; set => __position = value; }
        protected override void Dispose(bool disposing)
        {
            fsfuncs.Dispose();
        }
        public override void Flush()
        {
            fsfuncs.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return fsfuncs.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            fsfuncs.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            fsfuncs.Write(buffer, offset, count);
        }
    }
}
