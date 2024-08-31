#include "../../core/base.h"
#include <stdio.h>
#include <stdlib.h>

void *Allocate(int size)
{
    return malloc((size_t)size);
}
void Free(void *ptr)
{
    free(ptr);
}
void *Resize(void *ptr, int Size)
{
    return realloc(ptr, (size_t)Size);
}