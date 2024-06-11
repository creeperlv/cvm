using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler
{
	public static unsafe class AssemlerFunctions
	{
		public static Dictionary<uint, Func<Segment, OperationResult<CVMObject>, IntPtr, int, bool>> AssembleFunctions =
			new(){
				{InstID.EXIT,Assemble_Exit },
				{InstID.ADD,Assemble_Exit },
				};
		public unsafe static bool Assemble_Add(Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			Instruction instruction = default;
			instruction.Set(InstID.ADD);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
			}
			((Instruction*)InstPtr)[0] = instruction;
			return true;
		}
		public unsafe static bool Assemble_Exit(Segment s, OperationResult<CVMObject> result, IntPtr instPtr, int PC)
		{
			Instruction inst = default;
			inst.Set(InstID.EXIT);
			((Instruction*)instPtr)[0] = inst;
			return true;
		}
	}
}
