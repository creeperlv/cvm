using cvm.net.core;
using cvm.net.fullvm.core;

namespace cvm.net.fullvm;

class Program
{
	unsafe static void Main(string[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			string? item = args[i];
			switch (item)
			{
				case "--version":
					{
						Console.WriteLine($"CVM Inst Ver:{InstID.InstructionVersion}");
						return;
					}
				case "--data":
					{
						Console.WriteLine($"LBABlock:\t{sizeof(LBABlock)}");
						Console.WriteLine($"SFSItem:\t{sizeof(SFSItem)}");
						Console.WriteLine($"Guid:\t\t{sizeof(Guid)}");
						Console.WriteLine($"Instruction:\t{sizeof(Instruction)}");
						Console.WriteLine($"PartMetadata:\t{sizeof(PartationMetadata)}");
						Console.WriteLine($"SFSNodeBlock:\t{sizeof(SFSNodeBlock)}");
						return;
					}
				case "--disk":
					break;
				case "--create-disk":
					break;
				default:
					break;
			}
		}
		Display d = new Display();
		d.Init(800, 600);
		DisplayManager.InitVideo();
		DisplayManager.AttachDisplay(d);
		DisplayManager.SetupVideoMode();
	}
}
