using cvm.net.fullvm.core.Data;
using System;

namespace cvm.net.fullvm.core.Disk
{
	public static class DiskDefinitions
	{
		public const int LBASectorSize = 512;
		public const int GPTPartationNameMaxLen = 72;
		public const ulong GPTHeader = 0x5452415020494645UL;
		public static readonly UnmanagedVersion CurrentCVMIMGVersion = new() { Major = 1, Minor = 0, Build = 0, Patch = 0 };
		public static Dictionary<PartationType, Guid> PartationTypeIDs = new() {
			{PartationType.EFI,new Guid(0xC12A7328 , 0xF81F,0x11D2,0xBA,0x4B,0x00,0xA0,0xC9,0x3E,0xC9,0x3B) },
			{PartationType.RootSystem,new Guid(0xe75e66d3, 0xc1bc, 0x43d6,  0x8c, 0xd2, 0x20, 0x1b, 0xff, 0xbe, 0xc9, 0x9e ) }
			};


		public static Dictionary<string, PartationType> PartTypeNames = new() {
			{ "EFI", PartationType.EFI },
			{ "ROOTFS", PartationType.RootSystem }
		};
	}
	public enum PartationType
	{
		EFI, RootSystem
	}
}
