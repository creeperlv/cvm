using System;

namespace cvm.net.core
{
	public static class InstID
	{
		public static readonly Version InstructionVersion = new Version(1, 0, 0, 0);

		public const uint NOP = 0x0000;
		//ADD [TYPE] [VALUE|REG][VALUE|REG] L R T
		//00 01 01 01 00 00 01 02
		//00 01 02 03 04 05 06 07
		// ADD S reg $0 $1 $2
		public const uint ADD = 0x0001;
		public const uint SUB = 0x0002;
		public const uint MUL = 0x0003;
		public const uint DIV = 0x0004;
		//LRCalc [OP] [Type] L R T
		//00 05 02 01 10 18 20
		//LRCalc pow int $X2 $X3 $X4
		public const uint LR_CALC = 0x0005;
		//Self_calc [OP] [TYPE] L T
		//00 07 02 01 08 00
		//SelfCalc Exp Int32 $8 $0
		public const uint SELF_CALC = 0x0007;
		//SET [TYPE] REG Data
		//00 09 03 10 FF FF FF FF
		//SET LONG $X2 0xFF_FF_FF_FF
		public const uint SET = 0x0009;
		//CVT SRC_TYPE TGT_TYPE L R
		//00 20 05 02 01 02
		// CVT float int $1 $2
		public const uint CVT = 0x0020;
		//SD LEN SRC_REG TGT_ADDR
		//00 21 10 02 01
		//SD 16 $2 $1
		public const uint SD = 0x0020;
		public const uint LD = 0x0021;
		//SH [L|R] Size Reg
		//00 22 00 10 10
		//SH L 16 $16
		public const uint SH = 0x0022;
		//Logic Operations
		//LG [AND|OR|NOT|XOR|NOR] Size L R T
		//00 30 10 10 11 12
		public const uint LG = 0x0023;
		//JMP [0|1] [Address|Register]
		//00 10 00 00 10 00
		//JMP 0 LBL (at 0x1000)
		//JMP 1 $R0
		public const uint JMP = 0x0010;
		//Jump if flag
		//JF 
		public const uint JF = 0x0011;
		//Jump is overflow
		public const uint JO = 0x0012;
		//COMP [LT|LE|EQ|NE|RT|RE] [B|S|I|L|S|D] L R
		public const uint COMP = 0x0013;
		//CALL [0|1] [Address|Register]
		public const uint CALL = 0x0014;
		public const uint RET = 0x0015;
		//Parallel Jump
		//PJMP [IsRegister:0|1] [Address|Register]
		//Copy register file then run.
		public const uint PJMP = 0x0016;
		//Reset Flag
		public const uint RF = 0x0017;
		public const uint RO = 0x0018;
		//INT [0|1] [ID]
		//00 16 00 00 01
		//INT Static WRITE
		//Interrupt, VM/FW calls.
		public const uint INT = 0x0019;
		//Interrupt Globally. Call to system calls.
		public const uint INTG = 0x001A;
		//malloc [0|1] [Size|Reg] Target
		//00 30 00 F0 F0 80
		//malloc 0xF0F0 $0x80
		public const uint MALLOC = 0x0030;
		public const uint CALLOC = 0x0031;
		public const uint REALLOC = 0x0032;
		public const uint FREE = 0x0033;
		// Cause INTG.
		public const uint EXIT = 0x0034;
		//Start a new process
		//START [FILE|MEMORY] $PTR_TO_IMG $CONFIGURATION
		//00 35 00 10 20
		//CONFIGURATION:
		//|0|1|
		//|-|-|
		//|Kernel Mode|Allow Direct Execution of INT|
		// Cause INTG.
		public const uint START = 0x0035;
		//SINT [0|1] TGT PC
		//00 F0 00 10 00 10
		//SINT Static SOME_THING LBL (at 10)
		//SINT Reg SOME_THING $10
		//Set interrupt handler.
		public const uint SINT = 0x00F0;
		//RINT [0|1] TGT
		//00 F1 00 10 00
		//RINT Static SOME_THING
		//00 F1 01 10
		//RINT Reg $0x10
		public const uint RINT = 0x00F1;
		//GSINT [0|1] TGT PC
		//Globally Set Interrupt
		public const uint GSINT = 0x00F2;
		//GRINT [0|1] TGT PC
		public const uint GRINT = 0x00F3;

	}
}