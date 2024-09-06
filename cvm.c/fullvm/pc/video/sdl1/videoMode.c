#include "../../pc_impl.h"
#include <SDL/SDL.h>
#include <SDL/SDL_image.h>
#include <SDL/SDL_ttf.h>

SDL_Surface *screen = NULL;


UInt8 *Buffer;
result EnterVideoMode()
{
    Buffer = calloc(sizeof(UInt8), 800 * 600 * 4);
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