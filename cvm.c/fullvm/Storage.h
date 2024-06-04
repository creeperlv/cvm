#ifndef __cvm_fullvm_Storage
#define __cvm_fullvm_Storage
#include "../core/cvm.h"

#define __cvm_fullvm_Storage_EFI_GUID_L 0xC12A7328F81F11D2L
#define __cvm_fullvm_Storage_EFI_GUID_R 0xBA4B00A0C93EC93BL

typedef struct __cvm_fullvm_diskimg{
    
}_cvm_fullvm_diskimg;
typedef struct __cvm_fullvm_GPT{
    
}_cvm_fullvm_GPT;
typedef struct __cvm_fullvm_LBABlock{
    UInt8 Data[512];
}cvmLBABlock;
typedef struct __cvm_fullvm_GPT_Entery{
    cvmGuid PartType;
    cvmGuid PartID;
    UInt64 FirstLBA;
    UInt64 LastLBA;
    UInt64 Attributes;
    UInt8 Name[72];
}cvmGPTEntery;
typedef struct __cvm_fullvm_GPT_Header
{
    UInt64 Header;
    Int32 Revision;
    Int32 HeaderSize;
    UInt32 CRC32;
    Int32 Reserved0x14;
    UInt64 CurrentLBA;
    UInt64 BackupLBA;
    UInt64 FirstLBA;
    UInt64 LastLBA;
    cvmGuid DiskID;
    UInt64 EnteriesStartingLBA;
    Int32 EnteryCount;
    Int32 EnterySize;
    UInt32 EnteryTableCRC32;
    Int8 ReservedBlank[420];
}cvmGPTHeader;

#endif 