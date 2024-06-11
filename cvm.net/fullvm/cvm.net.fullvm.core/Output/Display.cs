using static cvm.net.core.libc.StdLib;
namespace cvm.net.fullvm.core.Output
{
	public unsafe class Display : IDisposable
	{
		public int W;
		public int H;
		public byte* buffer;
		public DisplayMode mode;
		public void Dispose()
		{
			free(buffer);
		}

		public void Init(int W, int H)
		{
			mode = DisplayMode.Graphics;
			this.W = W;
			this.H = H;
			buffer = (byte*)malloc(W * H * 4);
			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					var id = (y * W + x) * 4;
					buffer[id + 0] = (byte)((y * W + x) % 0xFF);
					buffer[id + 1] = (byte)((y * W + x) % 0xFF);
					buffer[id + 2] = (byte)((y * W + x) % 0xFF);
					buffer[id + 3] = 0xFF;
				}
			}
		}
		public void InitAsVT(int W, int H)
		{
			this.W = W;
			this.H = H;
			mode = DisplayMode.VT;
			buffer = (byte*)malloc(W * H * sizeof(VTCharacter));
			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					var id = (y * W + x) * sizeof(VTCharacter);
					((VTCharacter*)(buffer + id))[0] = default;
				}
			}
		}
	}
	public enum DisplayMode
	{
		VT, Graphics
	}
}
