#ifndef __cvm_vm_def
#define  __cvm_vm_def
#define  CVM_I_NOP 0x0000
#define  CVM_I_ADD 0x0001
#define  CVM_I_SUB 0x0002
#define  CVM_I_MUL 0x0003
#define  CVM_I_DIV 0x0004
#define LR_CALC 0x0005
#define SELF_CALC 0x0007
#define SET 0x0009
#define CVT 0x000A
#define SD 0x0020
#define LD 0x0021
#define SH 0x0022
#define LG 0x0023
#define MCP 0x0024
#define REFS 0x0025
#define JMP 0x0010
#define JF 0x0011
#define JO 0x0012
#define COMP 0x0013
#define CALL 0x0014
#define RET 0x0015
#define PJMP 0x0016
#define RF 0x0017
#define RO 0x0018
#define INT 0x0019
#define INTG 0x001A
#define CALLS 0x001B
#define RGP 0x001C
#define TESTINT 0x001D
#define MALLOC 0x0030
#define CALLOC 0x0031
#define REALLOC 0x0032
#define FREE 0x0033
#define EXIT 0x0034
#define START 0x0035
#define RSM 0x0036
#define SINT 0x00F0
#define RINT 0x00F1
#define GSINT 0x00F2
#define GRINT 0x00F3
#define GINFO 0x00F4
#define DUMP 0x00F5
#define ADV0 0xF000
#endif
