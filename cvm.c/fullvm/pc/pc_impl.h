#ifndef __cvm_fullvm__
#define __cvm_fullvm__
#include "../../core/cvm.h"
#include "./storage/Storage.h"
#include "./win32/minimal_posix.h"
#include <stdio.h>
#ifndef _WIN32 
#include <unistd.h>
#define __cvm__stdout STDOUT_FILENO
#define __cvm__stdin STDIN_FILENO
#else
#define __cvm__stdin stdin
#define __cvm__stdout stdout
#endif // __WIN32

result EnterVideoMode();
result VideoLoop();
#endif
