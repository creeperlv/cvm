using cvm.net.core;
using cvm.net.core.libc;
using cvm.net.fullvm.core;
using cvm.net.fullvm.core.Disk;
using cvm.net.fullvm.core.Disk.SFS;
using cvm.net.fullvm.core.Firmware;
using cvm.net.fullvm.core.Output;
using System.Diagnostics;
using System.Drawing;

namespace cvm.net.fullvm;

class Program
{
	public static void PrintHelp()
	{
		Console.WriteLine("fullvm <options>");
		Console.WriteLine("Options:");
		Console.WriteLine("--version");
		Console.WriteLine("\tShow version information.");
		Console.WriteLine("--disk <file>");
		Console.WriteLine("\tSpecify disk image to load.");
		Console.WriteLine("--launch");
		Console.WriteLine("\tLaunch the virtual machine.");
	}
	unsafe static void Main(string[] args)
	{
		var stdin = Console.OpenStandardInput();

		using var reader = new StreamReader(stdin);
		CLIOptions options = new CLIOptions();
		for (int i = 0; i < args.Length; i++)
		{
			string? item = args[i];
			switch (item)
			{
				case "--version":
					options.Operation = Operation.Version;
					break;
				case "--data":
					options.Operation = Operation.ShowDataStructureInfo;
					break;
				case "-l":
				case "--launch":
					{
						options.Operation = Operation.Launch;
					}
					break;
				case "--disk":
					{
						i++;
						var name = args[i];
						options.Append(OptionNames.Disks, name);
					}
					break;
				case "--sectors":
					{
						i++;
						var value = args[i];
						options.Set(OptionNames.Sectors, value);
					}
					break;
				default:
					break;
			}
		}
		switch (options.Operation)
		{
			case Operation.Help:
				PrintHelp();
				return;
			case Operation.Version:
				{
					Console.WriteLine($"CVM Inst Ver:{InstID.InstructionVersion}");
					return;
				}
			case Operation.Launch:
				{
					Terminal.currentUsingTerminal = new ConsoleBasedVT();
					var imgFiles = options.GetList(OptionNames.Disks);
					FullVMFirmware fw = new FullVMFirmware();
					foreach (var item in imgFiles)
					{
						DiskImage diskImage = new DiskImage() { FD = File.Open(item, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite) };
						fw.imgs.Add(diskImage);
					}
					fw.Init(false);
					//Display d = new Display();
					//d.Init(800, 600);
					//DisplayManager.InitVideo();
					//DisplayManager.AttachDisplay(d);
					//DisplayManager.SetupVideoMode();
				}
				break;
			case Operation.ShowDataStructureInfo:
				{
					Console.WriteLine($"LBABlock:\t{sizeof(LBABlock)}");
					Console.WriteLine($"SFSItem:\t{sizeof(SFSItem)}");
					Console.WriteLine($"Guid:\t\t{sizeof(Guid)}");
					Console.WriteLine($"GPTHeader:\t\t{sizeof(GPTHeader)}");
					Console.WriteLine($"Instruction:\t{sizeof(Instruction)}");
					Console.WriteLine($"PartMetadata:\t{sizeof(PartitionMetadata)}");
					Console.WriteLine($"SFSNodeBlock:\t{sizeof(SFSNodeBlock)}");
					return;
				}
			default:
				break;
		}
	}
}
