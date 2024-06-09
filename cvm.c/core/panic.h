#ifndef __cvm__panic
#define __cvm__panic

#define __cvm__null ((void *)0)
#define __cvm_panic_malloc_id 0x0001
#define __cvm_panic_malloc_msg "malloc() failed!"

#define __cvm_panic_calloc_id 0x0002
#define __cvm_panic_calloc_msg "calloc() failed!"
#define __cvm_panic_fopen_id 0x0003
#define __cvm_panic_fopen_msg "fopen() failed!"

#define __cvm_panic_generic_id 0x0000
#define __cvm_panic_generic_msg "Generic CVM panic!"

typedef void (*__cvm_PanicHandler)(int);

char *__cvm_GetPanicMsg(int id);

void __cvm_SetPanicHandler(__cvm_PanicHandler handler);

void __cvm_Panic(int ID);

#endif
