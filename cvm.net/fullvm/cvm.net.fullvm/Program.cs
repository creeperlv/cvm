using cvm.net.core;
using cvm.net.core.libc;
using cvm.net.fullvm.core;
using cvm.net.fullvm.core.Disk;
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
		Console.WriteLine("\tLaunch the virutal machine.");
		Console.WriteLine("--sectors <sector-count>");
		Console.WriteLine("\tThe sector count the disk will have. (The size of a sector is 512B)");
	}
	static void PrintHexDigit(int i)
	{
		if (i < 10)
		{
			Console.Write((char)('0' + i));
		}
		else
		{
			Console.Write((char)('A' + i - 0xA));
		}
	}
	public static void PrintHEX(byte b)
	{
		PrintHexDigit(b / 0x10);
		PrintHexDigit(b & 0x0F);
		Console.Write(' ');
	}
	unsafe static void Main(string[] args)
	{
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

					Display d = new Display();
					d.Init(800, 600);
					DisplayManager.InitVideo();
					DisplayManager.AttachDisplay(d);
					DisplayManager.SetupVideoMode();
				}
				break;
			case Operation.ShowDataStructureInfo:
				{
					{
						LBABlock blk = new LBABlock();
						{
							var ptr = &blk;
							GPTHeader header = new GPTHeader();
							header.EnterListCRC = uint.MaxValue;
							((GPTHeader*)ptr)[0] = header;
							for (int i = 0; i < DiskDefinitions.LBASectorSize; i++)
							{
								PrintHEX(blk.Data[i]);
								if ((i + 1) % 0xF == 0 && i != 0)
								{
									Console.WriteLine();
								}
							}
						}
					}
					Console.WriteLine($"LBABlock:\t{sizeof(LBABlock)}");
					Console.WriteLine($"SFSItem:\t{sizeof(SFSItem)}");
					Console.WriteLine($"Guid:\t\t{sizeof(Guid)}");
					Console.WriteLine($"GPTHeader:\t\t{sizeof(GPTHeader)}");
					Console.WriteLine($"Instruction:\t{sizeof(Instruction)}");
					Console.WriteLine($"PartMetadata:\t{sizeof(PartationMetadata)}");
					Console.WriteLine($"SFSNodeBlock:\t{sizeof(SFSNodeBlock)}");
					return;
				}
			default:
				break;
		}
	}
}
