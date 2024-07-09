#include "panic.h"

__cvm_PanicHandler __cvm_handler = __cvm__null;

char *__cvm_GetPanicMsg(int id) {
  switch (id) {
  case __cvm_panic_malloc_id:
    return __cvm_panic_malloc_msg;
  default:
    return __cvm_panic_generic_msg;
  }
}

void __cvm_SetPanicHandler(__cvm_PanicHandler handler) {
  __cvm_handler = handler;
}

void __cvm_Panic(int ID) {
  if (__cvm_handler == __cvm__null)
    return;
  __cvm_handler(ID);
}
