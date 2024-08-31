#ifndef __cvm_base__
#define __cvm_base__

void *Allocate(int size);
void Free(void *ptr);
void *Resize(void *ptr, int newSize);
void Debug_Log(char *str);
void Debug_LogLine(char *str);
#endif