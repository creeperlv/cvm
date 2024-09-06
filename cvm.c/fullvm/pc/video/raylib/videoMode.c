#include "../../pc_impl.h"
#include <raylib.h>

UInt8 *Buffer;
RenderTexture2D RBuffer;
float FMin(float L, float R)
{
    return L < R ? L : R;
}
Texture2D t2d;
int Stage = 0;
result EnterVideoMode()
{
    Buffer = calloc(sizeof(UInt8), 800 * 600 * 4);
    SetConfigFlags(FLAG_WINDOW_RESIZABLE);
    InitWindow(800, 600, "CVM");

    RBuffer = LoadRenderTexture(800, 600);
    Image img = GenImageColor(800, 600, BLACK);
    t2d = LoadTextureFromImage(img);
    return __cvm_result_ok;
}

result VideoLoop()
{
    while (!WindowShouldClose())
    {
        BeginDrawing();
        ClearBackground(BLACK);
        float SW = GetScreenWidth();
        float SH = GetScreenHeight();
        // for (size_t x = 0; x < 800; x++)
        // {
        //     for (size_t y = 0; y < 600; y++)
        //     {
        //         size_t id = y * 800 + x;
        //         Buffer[id * 4 + 0] = rand() % 0xFF;
        //         Buffer[id * 4 + 1] = rand() % 0xFF;
        //         Buffer[id * 4 + 2] = rand() % 0xFF;
        //         Buffer[id * 4 + 3] = 0xFF;
        //     }
        // }
        switch (Stage)
        {
        case 0:
        {
            int SHI = GetScreenHeight();
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
        case 1:
        {
            UpdateTexture(t2d, Buffer);
            float Scale = FMin(SW / 800.0f, SH / 600.0f);
            DrawTextureEx(t2d, (Vector2){(SW - 800.0f * Scale) / 2, (SH - 600.0f * Scale) / 2}, 0, Scale, WHITE);
        }
        break;
        default:
            break;
        }
        DrawFPS(10, 5);
        EndDrawing();
    }
    return __cvm_result_ok;
}