#include "Storage.h"
CVMRESULT LoadDisk(char* name, CVMDiskImg* disk){
	FILE* fd=fopen(name,"rwb");
	if(fd == NULL){
		__cvm_Panic(__cvm_panic_fopen_id);
	}
	if(disk == NULL){
		return __CVM_FAIL;
	}
	return __CVM_SUCCESS;
}
