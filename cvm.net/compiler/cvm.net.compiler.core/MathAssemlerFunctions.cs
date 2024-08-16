using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler.core
{
	public static unsafe class MathAssemlerFunctions
	{
		public unsafe static bool Assemble_LRCalc(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			if (instID != InstID.LR_CALC)
			{
				return false;
			}
			InstPtr.Set(InstID.LR_CALC, 0);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var OPSeg = st.Current;

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
			var TSeg = st.Current;

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

			if (!ISADefinition.CurrentDefinition.LROps.TryGetValue(OPSeg.content.ToLower(), out var lrop))
			{
				result.AddError(new UnknownLRCalcOperationError(OPSeg));
				return false;
			}
			if (!ISADefinition.CurrentDefinition.Types.TryGetValue(TypeSeg.content.ToLower(), out var type))
			{
				result.AddError(new UnknownBaseTypeError(TypeSeg));
				return false;
			}
			InstPtr.Set(lrop, 2);
			InstPtr.Set(type, 3);

			if (!DataConversion.TryParseRegister(TSeg.content, result, out var _T))
			{
				result.AddError(new TypeMismatchError(TSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(LSeg.content, result, out var _L))
			{
				result.AddError(new TypeMismatchError(LSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(RSeg.content, result, out var _R))
			{
				result.AddError(new TypeMismatchError(RSeg, TypeNames.Register));
				return false;
			}

			InstPtr.Set(_T, 4);
			InstPtr.Set(_L, 5);
			InstPtr.Set(_R, 6);
			return true;
		}
		public unsafe static bool Assemble_SCalc(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			if (instID != InstID.SELF_CALC) return false;
			InstPtr.SetData(instID);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var OPSeg = st.Current;
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
			var SrcSeg = st.Current;

			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var TgtSeg = st.Current;

			if (!ISADefinition.CurrentDefinition.SCalcOps.TryGetValue(OPSeg.content.ToLower(), out var op))
			{
				result.AddError(new UnknownOperationError(InstructionNames.SCALC, OPSeg));
				return false;
			}
			if (!ISADefinition.CurrentDefinition.Types.TryGetValue(OPSeg.content.ToLower(), out var type))
			{
				result.AddError(new UnknownBaseTypeError(TypeSeg));
				return false;
			}
			if (!DataConversion.TryParseRegister(SrcSeg.content, out var src))
			{
				return false;
			}
			if (!DataConversion.TryParseRegister(TgtSeg.content, out var tgt))
			{
				return false;
			}
			InstPtr.SetData(op, 2);
			InstPtr.SetData(type, 3);
			InstPtr.SetData(src, 4);
			InstPtr.SetData(tgt, 5);
			return true;
		}
		public unsafe static bool Assemble_CVT(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			if (instID != InstID.CVT)
			{
				return false;
			}
			InstPtr.Set(InstID.CVT, 0);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var SrcTypeSeg = st.Current;

			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var TargetTypeSeg = st.Current;

			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var SrcSeg = st.Current;

			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var TargetSeg = st.Current;

			if (!ISADefinition.CurrentDefinition.Types.TryGetValue(SrcTypeSeg.content.ToLower(), out var lrop))
			{
				result.AddError(new UnknownBaseTypeError(SrcTypeSeg));
				return false;
			}
			if (!ISADefinition.CurrentDefinition.Types.TryGetValue(TargetTypeSeg.content.ToLower(), out var type))
			{
				result.AddError(new UnknownBaseTypeError(TargetTypeSeg));
				return false;
			}
			InstPtr.Set(lrop, 2);
			InstPtr.Set(type, 3);

			if (!DataConversion.TryParseRegister(SrcSeg.content, result, out var _T))
			{
				result.AddError(new TypeMismatchError(SrcSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(TargetSeg.content, result, out var _L))
			{
				result.AddError(new TypeMismatchError(TargetSeg, TypeNames.Register));
				return false;
			}
			InstPtr.Set(_T, 4);
			InstPtr.Set(_L, 5);
			return true;
		}
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
			InstPtr.Set(instID);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var current = st.Current;
			if (ISADefinition.CurrentDefinition.Types.TryGetValue(current.content.ToLower(), out var type))
			{
				InstPtr.Set(type, 2);
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
			byte _T;
			byte _L;
			if (!DataConversion.TryParseRegister(T.content, result, out _T))
			{
				result.AddError(new TypeMismatchError(T, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(L.content, result, out _L))
			{
				result.AddError(new TypeMismatchError(L, TypeNames.Register));
				return false;
			}
			InstPtr.Set(_T, 3);
			InstPtr.Set(_L, 4);
			{

				byte _R;

				if (!DataConversion.TryParseRegister(R.content, result, out _R))
				{
					result.AddError(new TypeMismatchError(R, TypeNames.Register));
					return false;
				}
				InstPtr.Set(_R, 5);
			}
			//((Instruction*)InstPtr)[0] = InstPtr;
			return true;
		}
	}
}
