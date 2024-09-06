#include "../headers/RLEImage.h"
#include <stdio.h>
result RLE_LoadImage_FromMem(RLEImage *image, void *data, size_t dataLength){
    if(!CompEQBuffer(FILE_HEADER_RLEIMAGE,0,data,0,4)){
        return __cvm_result_fail;
    }
    return __cvm_result_ok;
}
result RLE_LoadImage_FromFile(RLEImage *image, char *Path){
    if(image->IsInited){
        RLE_UnloadImage(image);
    }
    return __cvm_result_ok;
}
result RLE_UnloadImage(RLEImage *image){
    return __cvm_result_ok;
}