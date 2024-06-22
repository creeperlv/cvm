using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler.core
{
	public static unsafe class LogicAssemlerFunctions
	{
		public unsafe static bool Assemble_LG(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.LG:
					break;
				default:
					return false;
			}
			InstPtr.SetData(InstID.LG);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var OpSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var TypeSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var LSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var RSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var TSeg = st.Current;
			if (!ISADefinition.CurrentDefinition.LogicOps.TryGetValue(OpSeg.content.ToLower(), out var op))
			{
				result.AddError(new UnknownOperationError(InstructionNames.LG, OpSeg));
				return false;
			}

			if (!ISADefinition.CurrentDefinition.Types.TryGetValue(TypeSeg.content.ToLower(), out var type))
			{
				result.AddError(new UnknownBaseTypeError(TypeSeg));
				return false;
			}
			if (!DataConversion.TryParseRegister(LSeg.content, result, out var L))
			{
				result.AddError(new TypeMismatchError(LSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(RSeg.content, result, out var R))
			{
				result.AddError(new TypeMismatchError(RSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(TSeg.content, result, out var T))
			{
				result.AddError(new TypeMismatchError(TSeg, TypeNames.Register));
				return false;
			}
			InstPtr.SetData(op, 2);
			InstPtr.SetData(type, 3);
			InstPtr.SetData(L, 4);
			InstPtr.SetData(R, 5);
			InstPtr.SetData(T, 6);
			return true;
		}

	}
}
