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
		public static Dictionary<uint, Func<ushort, Segment, OperationResult<CVMObject>, IntPtr, int, bool>> AssembleFunctions =
			new(){
				{InstID.EXIT,Assemble_Exit },
				{InstID.ADD,Assemble_BasicMath },
				};
		public unsafe static bool Assemble_BasicMath(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.ADD:
				case InstID.SUB:
				case InstID.MUL:
				case InstID.DIV:
					break;
				default:
					return false;
			}
			Instruction instruction = default;
			instruction.Set(instID);
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
			if (R.content.StartsWith('$'))
			{
				IsRegister = true;
			}
			byte _T;
			byte _L;
			if (DataConversion.TryParseRegister(T.content, out _T))
			{
				result.AddError(new TypeMismatchError(T, TypeNames.Register));
				return false;
			}
			if (DataConversion.TryParseRegister(L.content, out _L))
			{
				result.AddError(new TypeMismatchError(L, TypeNames.Register));
				return false;
			}
			instruction.Set((byte)(IsRegister ? 1 : 0), 3);
			instruction.Set(_T, 4);
			instruction.Set(_L, 5);
			if (IsRegister)
			{
				byte _R;

				if (DataConversion.TryParseRegister(R.content, out _R))
				{
					result.AddError(new TypeMismatchError(R, TypeNames.Register));
					return false;
				}
				instruction.Set(_R, 6);
			}
			else
			{
				switch (type)
				{
					case BaseDataType.BU:
						{
						}
						break;
					default:
						break;
				}
			}
			((Instruction*)InstPtr)[0] = instruction;
			return true;
		}

		public unsafe static bool Assemble_Exit(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr instPtr, int PC)
		{
			if (instID != InstID.EXIT)
			{
				return false;
			}
			Instruction inst = default;
			inst.Set(InstID.EXIT);
			((Instruction*)instPtr)[0] = inst;
			return true;
		}
	}
}
