using static Raylib_cs.Raylib;
using static cvm.net.core.libc.StdLib;
using Raylib_cs;
using System.ComponentModel;
using System.Numerics;
namespace cvm.net.fullvm.core
{
	public unsafe class Display : IDisposable
	{
		public int W;
		public int H;
		public byte* buffer;

		public void Dispose()
		{
			free(buffer);
		}

		public void Init(int W, int H)
		{
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
	}
	public enum BootStage
	{
		POST, UEFI, RUN
	}
	public static unsafe class DisplayManager
	{
		static Display? currentDisplay;
		static Texture2D t2d;
		static BootStage stage;
		static Image bg;
		public static void AttachDisplay(Display display)
		{
			UnloadImage(bg);
			UnloadTexture(t2d);
			currentDisplay = display;
			bg = GenImageColor(display.W, display.H, Color.Black);
			t2d = LoadTextureFromImage(bg);
		}
		public static void InitVideo()
		{
			SetConfigFlags(Raylib_cs.ConfigFlags.ResizableWindow);
			InitWindow(800, 600, "CVM");
		}
		public static void SetupVideoMode()
		{
			while (!WindowShouldClose())
			{
				BeginDrawing();
				ClearBackground(Color.Black);
				var SW = GetScreenWidth();
				var SH = GetScreenHeight();
				if (currentDisplay is not null)
				{
					UpdateTexture(t2d, currentDisplay.buffer);
					float Scale = Math.Min(SW / 800.0f, SH / 600.0f);
					DrawTextureEx(t2d, new Vector2((SW - 800.0f * Scale) / 2, (SH - 600.0f * Scale) / 2), 0, Scale, Color.White);
				}
				DrawFPS(5, 5);
				EndDrawing();
			}
		}

	}
}
