#if !defined(__cvm_fullvm_Video)
#define __cvm_fullvm_Video
#include "../../core/base.h"
result EnterVideoMode(int ScreenMode);
void ForceUse32Bit();
void *GetBuffer();
void SetChar(int W, int H, char C);
#define __cvm_full_video_mode__terminal 0x00
#define __cvm_full_video_mode__direct 0x01
typedef struct __cvm_fullvm_ScreenInfo
{
    int W;
    int H;
    int ColorDepth;
} CVMScreenInfo;

#endif // __cvm_fullvm_Video
