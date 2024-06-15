using System;

namespace cvm.net.core
{
	public static class InstID
	{
		public static readonly Version InstructionVersion = new Version(1, 0, 0, 0);

		public const ushort NOP = 0x0000;
		//ADD [TYPE] [VALUE|REG] T L R
		//00 01 01 01 00 01 02 xx xx 00
		//00 01 02 03 04 05 06 07 .. 0F
		// ADD S $0 $1 $2
		//REG is determined by R.
		public const ushort ADD = 0x0001;
		public const ushort SUB = 0x0002;
		public const ushort MUL = 0x0003;
		public const ushort DIV = 0x0004;
		//LRCalc [OP] [Type] L R T
		//00 05 02 01 10 18 20
		//LRCalc pow int $X2 $X3 $X4
		public const ushort LR_CALC = 0x0005;
		//Self_calc [OP] [TYPE] L T
		//00 07 02 01 08 00
		//SelfCalc Exp Int32 $8 $0
		public const ushort SELF_CALC = 0x0007;
		//SET [TYPE] REG Data
		//00 09 03 10 FF FF FF FF
		//SET LONG $X2 0xFF_FF_FF_FF
		public const ushort SET = 0x0009;
		//CVT SRC_TYPE TGT_TYPE L R
		//00 20 05 02 01 02
		// CVT float int $1 $2
		public const ushort CVT = 0x0020;
		//SD LEN SRC_REG TGT_ADDR
		//00 21 10 02 01
		//SD 16 $2 $1
		public const ushort SD = 0x0020;
		public const ushort LD = 0x0021;
		//SH [L|R] Size Reg
		//00 22 00 10 10
		//SH L 16 $16
		public const ushort SH = 0x0022;
		//Logic Operations
		//LG [AND|OR|NOT|XOR|NOR] Size L R T
		//00 30 10 10 11 12
		public const ushort LG = 0x0023;
		//Memory Copy
		//MCP [0|1] $SRC $Target [$Length|Length]
		//00 24 01 10 12 10 00 [ 00 <-- Unused
		//MCP $0x10 $0x12 0x1000
		public const ushort MCP = 0x0024;
		//JMP [0|1] [Address|Register]
		//00 10 00 00 10 00
		//JMP 0 LBL (at 0x1000)
		//JMP 1 $R0
		public const ushort JMP = 0x0010;
		//Jump if flag
		//JF <FlagID> <PC>
		public const ushort JF = 0x0011;
		//Jump is overflow
		public const ushort JO = 0x0012;
		//COMP [LT|LE|EQ|NE|RT|RE] [B|S|I|L|S|D] L R
		public const ushort COMP = 0x0013;
		//CALL [0|1] [Address|Register]
		public const ushort CALL = 0x0014;
		public const ushort RET = 0x0015;
		//Parallel Jump
		//PJMP [IsRegister:0|1] [Address|Register]
		//Copy register file then run.
		public const ushort PJMP = 0x0016;
		//Reset Flag
		//RF <FlagID>
		public const ushort RF = 0x0017;
		public const ushort RO = 0x0018;
		//INT [0|1] [ID]
		//00 16 00 00 01
		//INT Static WRITE
		//Interrupt, VM/FW calls.
		public const ushort INT = 0x0019;
		//Interrupt Globally. Call to system calls.
		public const ushort INTG = 0x001A;
		//CALLM
		//Call Method in module.
		//CALLM [VALUE|REG] [($)MODULEID] [PCinModule]
		public const ushort CALLM = 0x001B;
		//Request Global Pointer
		public const ushort RGP = 0x001C;
		//malloc [0|1] [Size|Reg] Target
		//00 30 00 F0 F0 80
		//malloc 0xF0F0 $0x80
		public const ushort MALLOC = 0x0030;
		public const ushort CALLOC = 0x0031;
		public const ushort REALLOC = 0x0032;
		public const ushort FREE = 0x0033;
		// Cause INTG.
		public const ushort EXIT = 0x0034;
		//Start a new process
		//START [FILE|MEMORY] $PTR_TO_IMG $CONFIGURATION
		//00 35 00 10 20
		//CONFIGURATION:
		//|0|1|
		//|-|-|
		//|Kernel Mode|Allow Direct Execution of INT|
		// Cause INTG.
		public const ushort START = 0x0035;
		//Resize Stack Memory
		//RSM [int|reg] <length/$length>
		public const ushort RSM = 0x0036;
		//SINT [0|1] TGT PC
		//00 F0 00 10 00 10
		//SINT Static SOME_THING LBL (at 10)
		//SINT Reg SOME_THING $10
		//Set interrupt handler.
		public const ushort SINT = 0x00F0;
		//RINT [0|1] TGT
		//00 F1 00 10 00
		//RINT Static SOME_THING
		//00 F1 01 10
		//RINT Reg $0x10
		public const ushort RINT = 0x00F1;
		//GSINT [0|1] TGT PC
		//Globally Set Interrupt
		public const ushort GSINT = 0x00F2;
		//GRINT [0|1] TGT PC
		public const ushort GRINT = 0x00F3;
		//GINFO INFO_ID $RECIVER
		//Get Info.
		public const ushort GINFO = 0x00F4;
		//ADV OP L R T
		public const ushort ADV0 = 0xF000;


	}
}