#if !defined(SFI)
#define SFI
#define DEVICE_TYPE_STORAGE 0
#define DEVICE_TYPE_DISP 1
typedef struct __sfi_io_operation
{
    int Device;
    int Length;
} SFIIOOperation;
typedef struct __sfi_device
{
    int Type;
    char Name[16];
    int Location;
} SFIDevice;
typedef struct __sfi_information
{
    int ProtocolMainVersion;
    int ProtocolMinVersion;
    char Name[16];
} SFIInfo;
#endif // SFI
