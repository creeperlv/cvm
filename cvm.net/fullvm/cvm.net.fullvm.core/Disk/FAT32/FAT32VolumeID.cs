namespace cvm.net.fullvm.core.Disk.FAT32
{
	public struct FAT32VolumeID
	{
		public ushort BytesPerSector;
		public byte SectorsPerCluster;
		public ushort ReversedSectors;
		public byte FATCount;
		public uint FATSize;
		public uint RootDirFirstCluster;
		public ushort Signature;
	}
}
