using static Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;
namespace cvm.net.fullvm.core.Output
{
	public static unsafe class DisplayManager
    {
        static Display? currentDisplay;
        static Texture2D t2d;
        static DisplayMode stage;
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
            SetConfigFlags(ConfigFlags.ResizableWindow);
            InitWindow(800, 600, "CVM");
        }
        public static void SetupVideoMode()
        {
            var WHITE = Color.White;
            var RED = Color.Red;
            var ORANGE = Color.Orange;
            var GRAY = Color.Gray;
            while (!WindowShouldClose())
            {
                BeginDrawing();
                ClearBackground(Color.Black);
                var SW = GetScreenWidth();
                var SH = GetScreenHeight();
                var SHI = SH;
                switch (stage)
                {
                    case DisplayMode.VT:
                        {
                            DrawText("CVM BOOT", 10, 22, 10, WHITE);
                            DrawText("No Disk Image Loaded.", SW / 2 - 150, SHI / 2 + 50, 10, RED);
                            DrawText("Require a CVM Disk Image.", SW / 2 - 150, SHI / 2 + 65, 10, ORANGE);
                            DrawText("The image have to be parted with GPT.", SW / 2 - 150, SHI / 2 + 80, 10, ORANGE);
                            DrawText("M", SW / 2 - 50, SHI / 2 - 50, 40, GRAY);
                            DrawText("V", SW / 2 - 70, SHI / 2 - 30, 40, GRAY);
                            DrawText("C", SW / 2 - 60, SHI / 2 - 40, 40, WHITE);
                            DrawText("VM", SW / 2 - 15, SHI / 2 - 40, 40, WHITE);
                        }
                        break;
                    case DisplayMode.Graphics:
                        if (currentDisplay is not null)
                        {
                            UpdateTexture(t2d, currentDisplay.buffer);
                            float Scale = Math.Min(SW / 800.0f, SH / 600.0f);
                            DrawTextureEx(t2d, new Vector2((SW - 800.0f * Scale) / 2, (SH - 600.0f * Scale) / 2), 0, Scale, Color.White);
                        }
                        break;
                    default:
                        break;
                }
                DrawFPS(5, 5);
                EndDrawing();
            }
        }

    }
}
