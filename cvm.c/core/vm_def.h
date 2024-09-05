#ifndef __cvm_vm_def
#define __cvm_vm_def
#define CVM_I_NOP 0x0000
#define CVM_I_ADD 0x0001
#define CVM_I_SUB 0x0002
#define CVM_I_MUL 0x0003
#define CVM_I_DIV 0x0004
#define CVM_I_LR_CALC 0x0005
#define CVM_I_SELF_CALC 0x0007
#define CVM_I_SET 0x0009
#define CVM_I_CVT 0x000A
#define CVM_I_SD 0x0020
#define CVM_I_LD 0x0021
#define CVM_I_SH 0x0022
#define CVM_I_LG 0x0023
#define CVM_I_MCP 0x0024
#define CVM_I_REFS 0x0025
#define CVM_I_JMP 0x0010
#define CVM_I_JF 0x0011
#define CVM_I_JO 0x0012
#define CVM_I_COMP 0x0013
#define CVM_I_CALL 0x0014
#define CVM_I_RET 0x0015
#define CVM_I_PJMP 0x0016
#define CVM_I_RF 0x0017
#define CVM_I_RO 0x0018
#define CVM_I_INT 0x0019
#define CVM_I_INTG 0x001A
#define CVM_I_CALLS 0x001B
#define CVM_I_RGP 0x001C
#define CVM_I_TESTINT 0x001D
#define CVM_I_MALLOC 0x0030
#define CVM_I_CALLOC 0x0031
#define CVM_I_REALLOC 0x0032
#define CVM_I_FREE 0x0033
#define CVM_I_EXIT 0x0034
#define CVM_I_START 0x0035
#define CVM_I_RSM 0x0036
#define CVM_I_MMAP 0x0037
#define CVM_I_IN 0x0038
#define CVM_I_OUT 0x0039
#define CVM_I_OMAP 0x003A
#define CVM_I_IMAP 0x003B

#define CVM_I_SINT 0x00F0
#define CVM_I_RINT 0x00F1
#define CVM_I_GSINT 0x00F2
#define CVM_I_GRINT 0x00F3
#define CVM_I_GINFO 0x00F4
#define CVM_I_DUMP 0x00F5
#define CVM_I_ADV0 0xF000
#endif
