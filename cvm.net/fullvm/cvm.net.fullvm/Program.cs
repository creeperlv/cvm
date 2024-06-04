using cvm.net.core;
using cvm.net.fullvm.core;

namespace cvm.net.fullvm;

class Program
{
	unsafe static void Main(string[] args)
	{
		Console.WriteLine($"LBABlock:\t{sizeof(LBABlock)}");
		Console.WriteLine($"SFSItem:\t{sizeof(SFSItem)}");
		Console.WriteLine($"Guid:\t{sizeof(Guid)}");
		Console.WriteLine($"Instruction:\t{sizeof(Instruction)}");
		Console.WriteLine($"PartationMetadata:\t{sizeof(PartationMetadata)}");
		Console.WriteLine($"SFSNodeBlock:\t{sizeof(SFSNodeBlock)}");
		Display d = new Display();
		d.Init(800, 600);
		DisplayManager.InitVideo();
		DisplayManager.AttachDisplay(d);
		DisplayManager.SetupVideoMode();
	}
}
