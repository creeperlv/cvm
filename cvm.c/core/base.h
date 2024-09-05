#ifndef __cvm_base__
#define __cvm_base__

#define __cvm_result_ok 0
#define __cvm_result_fail -1
#define __cvm_result_fail_malloc -2
#define __cvm_result_fail_calloc -3
#define __cvm_result_fail_realloc -4
typedef int result;

void *Allocate(int size);
void Free(void *ptr);
void *Resize(void *ptr, int newSize);
void Debug_Log(char *str);
void Debug_LogLine(char *str);
#endif