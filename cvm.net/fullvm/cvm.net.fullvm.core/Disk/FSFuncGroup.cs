namespace cvm.net.fullvm.core.Disk
{
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
}
