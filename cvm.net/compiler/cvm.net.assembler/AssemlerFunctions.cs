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
				return false;
			}
			var current = st.Current;
			if (ISADefinition.CurrentDefinition.Types.TryGetValue(current.content, out var type))
			{
				instruction.Set(type, 2);
			}
			else
			{
				result.AddError(new UnknownBaseTypeError(current));
				return false;
			}
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			current = st.Current;
			var T = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			current = st.Current;
			var L = current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			current = st.Current;
			var R = current;
			bool IsRegister = false;
			if (R.content.StartsWith("$"))
			{
				IsRegister = true;
			}
			byte _T;
			byte _L;
			if (DataConversion.TryParseRegister(T.content, out _T))
			{

			}
			if (DataConversion.TryParseRegister(L.content, out _L))
			{

			}
			if (IsRegister)
			{
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
