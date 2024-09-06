#include "../../pc_impl.h"
#include <SDL/SDL.h>
#include <SDL/SDL_image.h>
#include <SDL/SDL_ttf.h>

SDL_Surface *screen = NULL;
char fullvm_will_close;
UInt8 *Buffer;
SDL_Event event;
uint32_t WIDTH;
uint32_t HEIGHT;
result EnterVideoMode()
{
    WIDTH = 800;
    HEIGHT = 600;
    fullvm_will_close = 0;
    Buffer = calloc(sizeof(UInt8), WIDTH * HEIGHT * 4);
    int i=0;
    for (size_t x = 0; x < WIDTH; x++)
    {
        for (size_t y = 0; y < HEIGHT; y++)
        {
            size_t index = y * WIDTH + x;
            Buffer[index * 4] = rand() % 255;
            Buffer[index * 4 + 1] = rand() % 255;
            Buffer[index * 4 + 2] = rand() % 255;
            Buffer[index * 4 + 3] = 255;
        }
    }

    if (SDL_Init(SDL_INIT_VIDEO) != 0)
    {
        printf("%s, failed to SDL_Init\n", __func__);
        return -1;
    }
    screen = SDL_SetVideoMode(800, 600, 32, SDL_HWSURFACE | SDL_DOUBLEBUF);
    if (screen == NULL)
    {
        printf("%s, failed to SDL_SetVideoMode\n", __func__);
        return -1;
    }
    return __cvm_result_ok;
}
void __sdl_event_update()
{

    while (SDL_PollEvent(&event))
    {
        switch (event.type)
        {
        case SDL_KEYDOWN:
        {
            SDLKey k = event.key.keysym.sym;
            printf("K=%d\n", k);
            switch (k)
            {
            // case KEY_EXIT:
            //     fullvm_will_close = 1;
            //     break;
            default:
                break;
            }
            // DrawUI();
        }
        break;
        case SDL_QUIT:
            fullvm_will_close = 1;
            break;
        default:
            break;
        }
    }
}
result VideoLoop()
{
    while (fullvm_will_close != 1)
    {
        if (SDL_MUSTLOCK(screen))
        {
            SDL_LockSurface(screen);
        }

        // Copy the pixel buffer to the screen surface
        memcpy(screen->pixels, Buffer, WIDTH * HEIGHT * 4);

        // Unlock the surface
        if (SDL_MUSTLOCK(screen))
        {
            SDL_UnlockSurface(screen);
        }
        SDL_Flip(screen);
        __sdl_event_update();
    }
}