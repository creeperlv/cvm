using cvm.net.compiler.core;
using cvm.net.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler
{

	public static unsafe class AssemlerFunctions
	{
		public static Dictionary<uint, Func<ushort, Segment, OperationResult<CVMObject>, IntPtr, int, bool>> AssembleFunctions =
			new(){
				{InstID.EXIT,Assemble_NoArgs },
				{InstID.NOP,Assemble_NoArgs },
				{InstID.ADD,MathAssemlerFunctions.Assemble_BasicMath },
				{InstID.SUB,MathAssemlerFunctions.Assemble_BasicMath },
				{InstID.MUL,MathAssemlerFunctions.Assemble_BasicMath },
				{InstID.DIV,MathAssemlerFunctions.Assemble_BasicMath },
				{InstID.LR_CALC,MathAssemlerFunctions.Assemble_LRCalc },
				};
		public unsafe static bool Assemble_NoArgs(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr instPtr, int PC)
		{
			Instruction inst = default;
			inst.Set(instID);
			((Instruction*)instPtr)[0] = inst;
			return true;
		}
	}
}
