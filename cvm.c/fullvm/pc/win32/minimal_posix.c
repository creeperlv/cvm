#ifdef _WIN32
#include <stdio.h>
void write(FILE * FILENO, void *buf, size_t size)
{
    fwrite(buf, sizeof(char), size, FILENO);
}
#endif