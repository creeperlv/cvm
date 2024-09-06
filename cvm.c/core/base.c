#include "base.h"
char CompEQBuffer(void *L, int LOffset, void *R, int ROffset, int Length)
{
    char *LPtr = L;
    LPtr += LOffset;
    char *RPtr = R;
    RPtr += ROffset;
    for (int i = 0; i < Length; i++)
    {
        if (LPtr[i] != RPtr[i])
            return 0;
    }
    return 1;
}