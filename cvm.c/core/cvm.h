#ifndef __cvm_cvm
#define __cvm_cvm
#include "panic.h"
#include <stdint.h>
#include <stdlib.h>
typedef int8_t Int8;
typedef uint8_t UInt8;
typedef int16_t Int16;
typedef uint16_t UInt16;
typedef int32_t Int32;
typedef uint32_t UInt32;
typedef int64_t Int64;
typedef uint64_t UInt64;
typedef uint32_t UInt32;
typedef UInt32 CVMRESULT;
#define __CVM_SUCCESS 0
#define __CVM_FAIL -1
#define __CVM_GUID_VER_MASK 0b0100111111111111
#define __CVM_GUID_VAR_MASK 0b10111111111111111111111111111111
#define __TRUE_32Bit_0 0x01
#define __TRUE_32Bit_1 0x02
#define __TRUE_32Bit_2 0x04
#define __TRUE_32Bit_3 0x08
#define __TRUE_32Bit_4 0x10
#define __TRUE_32Bit_5 0x20
#define __TRUE_32Bit_6 0x40
#define __TRUE_32Bit_7 0x80
typedef struct __cvm_guid {
  UInt32 random_A_L;
  UInt16 random_A_R;
  UInt16 Ver_RandomB;
  UInt32 Var_RandomC_L;
  UInt32 RandomC_R;
} cvmGuid;
typedef struct __cvm_guid2 {
  UInt64 L;
  UInt64 R;
} cvmGuid2;
typedef struct __cvm_callframe {
  UInt32 id;
  UInt32 pc;
} __cvm_callframe;

typedef struct __cvm_list {
  void *HEAD;
  UInt32 Size;
  UInt32 Count;
  UInt32 TSize;
  UInt64 TypeID;
} _cvm_list;

typedef struct __cvm_instruction {
  UInt64 D0;
  UInt64 D1;
} cvmInstruction;
typedef struct __cvm_module {
  int Length;
  int DataSegLength;
  void *DataSegment;
  int LibCount;
  void *LibTable;
  int SymbolTableCount;
  void *SymbolTable;
  int InstCount;
  cvmInstruction *Instructions;
  int ModuleID;
} cvmModule;

typedef struct __cvm_rt_sym_def {
  int ID;
  int PC;
} cvmRTSymbolDefinition;

typedef struct __cvm_rt_module {
  int LoadedSymbolCount;
  cvmRTSymbolDefinition *LoadedSymbols;
  int InstCount;
  cvmInstruction *Instructions;
  int ModuleID;
} cvmRTModule;

typedef struct __cvm_memory_block {
  void *Ptr;
  int Length;
  // IsRO=__TRUE_32Bit_1
  // IsReadRO=__TRUE_32Bit_0
  UInt32 IsRO_IsRefRO;
} cvmMemoryBlock;
typedef struct __cvm_cpu_core {
  struct __cvm_machine *machine;
  _cvm_list Callstack;
  UInt64 Reg[16];
  UInt64 FReg[16];
} _cvm_cpu_core;
typedef void (*FuncCall)(_cvm_cpu_core *cpu);
typedef struct __cvm_table {
  int Keys;
  void *Values;
  UInt32 Size;
  UInt32 Count;
} _cvm_table;
typedef struct __cvm_machine {
  _cvm_cpu_core *Cores;
  UInt32 CoreCount;
} _cvm_machine;

#endif
