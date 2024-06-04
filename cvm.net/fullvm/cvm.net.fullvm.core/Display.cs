using static Raylib_cs.Raylib;
namespace cvm.net.fullvm.core
{
	public unsafe class Display
	{
		byte* buffer;
		public void Init()
		{
		}
	}
	public static unsafe class DisplayManager
	{
		static Display? currentDisplay;
		public static void SetupVideoMode()
		{
			SetConfigFlags(Raylib_cs.ConfigFlags.ResizableWindow);
			InitWindow(800, 600, "CVM");
			while (true)
			{
			}
		}

	}
}
