namespace cvm.net.fullvm.core.Disk
{
    public class DiskPart : IDisk
    {
        public DiskImage img;
        public GPTPartMgr mgr;
        public PartationMetadata metadata;
        public DiskPart(DiskImage img, GPTPartMgr mgr)
        {
            this.img = img;
            this.mgr = mgr;
        }
        public unsafe void SetMetadata(PartationMetadata metadata)
        {
            this.metadata = metadata;
            Offset = img.DataOffset + metadata.FirstLBA * sizeof(LBABlock);
            PartLength = (metadata.LastLBA - metadata.FirstLBA) * sizeof(LBABlock);
        }
        public long Offset;
        internal long PartLength;
        long __LBAAddress = 0;
        public bool CanRead => img.FD.CanRead;
        public bool CanSeek => img.FD.CanSeek;
        public bool CanWrite => img.FD.CanWrite;
        public long Length => PartLength;

        public long Position
        {
            get
            {
                return __LBAAddress;
            }
            set => __LBAAddress = value;
        }

        public void Flush()
        {
            lock (img.FD)
            {
                img.FD.Flush();
            }
        }


        public void SetLength(long value)
        {
            mgr.ResizePart(this, value);
            PartLength = value;
        }


        public void SetPosToLBA(long LBABlockAddress)
        {
            __LBAAddress = LBABlockAddress;
        }

        public void WriteLBA(LBABlock Data, long LBABlockAddress)
        {
            img.WriteLBA(Data, Offset + LBABlockAddress);
        }

        public LBABlock ReadLBA(long LBABlockAddress)
        {
            return img.ReadLBA(Offset + LBABlockAddress);
        }
    }
}
