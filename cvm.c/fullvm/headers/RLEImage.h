#if !defined(RLEImageHeader)
#define RLEImageHeader
#include "../../core/cvm.h"
#define FILE_HEADER_RLEIMAGE "RLEI"
typedef struct __RLEImage
{
    UInt8 IsInited;
    UInt32 Width;
    UInt32 Height;
    UInt32 BPP;
    UInt8 *Data;
} RLEImage;
result RLE_LoadImage_FromMem(RLEImage *image, void *data, size_t dataLength);
result RLE_LoadImage_FromFile(RLEImage *image, char *Path);
result RLE_UnloadImage(RLEImage *image);
#endif // RLEImageHeader
