#ifndef __cvm_fullvm_Storage_pc
#define __cvm_fullvm_Storage_pc
#include "../../../core/cvm.h"
#include "../../headers/Storage.h"
#include <stdio.h>
typedef struct __cvm_fullvm_diskimg{
    char* File;
}_cvm_fullvm_diskimg;
typedef _cvm_fullvm_diskimg* CVMDiskImg;

CVMRESULT LoadDisk(char* name, CVMDiskImg* Disk);
#endif 
