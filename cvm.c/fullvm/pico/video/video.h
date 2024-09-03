#if !defined(__fullvm_video)
#define __fullvm_video
#include "../../../headers/Video.h"
#include "../../../../core/base.h"
void FullVMInitVideoDevice();
typedef struct __fullvm_video_device
{
    int W;
    int H;
    int Bitdepth;
} VideoDevice;
result FullVMGetVideoDeviceInfo(VideoDevice* info);
#endif // __fullvm_video
