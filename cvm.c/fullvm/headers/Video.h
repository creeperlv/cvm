#if !defined(__cvm_fullvm_Video)
#define __cvm_fullvm_Video

void EnterVideoMode(int ScreenMode);
#define __cvm_full_video_mode__terminal 0x00
#define __cvm_full_video_mode__buffer 0x01
typedef struct __cvm_fullvm_ScreenInfo{
    int W;
    int H;
    int ColorDepth;
} CVMScreenInfo;

#endif // __cvm_fullvm_Video
