using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System.Data;

namespace cvm.net.assembler.core
{
	public static unsafe class RegisterAssemlerFunctions
	{

		public unsafe static bool Assemble_SH(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.SH:
					break;
				default:
					return false;
			}
			InstPtr.SetData(InstID.SH);

			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var DirectionSeg = st.Current;
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
			var TargetSeg = st.Current;

			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var LengthSeg = st.Current;

			if (ISADefinition.CurrentDefinition.SHOps.TryGetValue(DirectionSeg.content.ToLower(), out var op))
			{
				result.AddError(new UnknownOperationError(InstructionNames.SH, TypeSeg));
				return false;
			}
			InstPtr.Set(op, 2);
			if (ISADefinition.CurrentDefinition.Types.TryGetValue(TypeSeg.content.ToLower(), out var type))
			{
				switch (type)
				{
					case BaseDataType.I:
					case BaseDataType.IU:
					case BaseDataType.L:
					case BaseDataType.LU:
						InstPtr.Set(type, 3);
						break;
					default:
						result.AddError(new UnsupportedBaseTypeError(TypeSeg));
						return false;
				}
			}
			else
			{
				result.AddError(new UnknownBaseTypeError(TypeSeg));
				return false;
			}

			if (!DataConversion.TryParseRegister(TargetSeg.content, result, out var register))
			{
				result.AddError(new TypeMismatchError(TargetSeg, TypeNames.Register));
				return false;
			}
			InstPtr.Set(register, 4);
			InstructionArgumentUtility.ParseAndSetArgument<int>(LengthSeg, result, 5, InstPtr, TypeNames.Int);
			return true;
		}
		public unsafe static bool Assemble_SET(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			int Offset = 4;
			switch (instID)
			{
				case InstID.SET:
					break;
				default:
					return false;
			}
			InstPtr.SetData(InstID.SET);
			SegmentTraveler st = new(s);
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
			var TargetSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var DataSeg = st.Current;

			if (ISADefinition.CurrentDefinition.Types.TryGetValue(TypeSeg.content.ToLower(), out var type))
			{
				InstPtr.Set(type, 2);
			}
			else
			{
				result.AddError(new UnknownBaseTypeError(TypeSeg));
				return false;
			}

			if (!DataConversion.TryParseRegister(TargetSeg.content, result, out var register))
			{
				result.AddError(new TypeMismatchError(TargetSeg, TypeNames.Register));
				return false;
			}

			InstPtr.Set(register, 3);
			switch (type)
			{
				case BaseDataType.BU:
					{
						InstructionArgumentUtility.ParseAndSetArgument<byte>(DataSeg, result, Offset, InstPtr, TypeNames.Byte);
					}
					break;
				case BaseDataType.I:
					{
						InstructionArgumentUtility.ParseAndSetArgument<int>(DataSeg, result, Offset, InstPtr, TypeNames.Int);
					}
					break;
				case BaseDataType.BS:
					{
						InstructionArgumentUtility.ParseAndSetArgument<sbyte>(DataSeg, result, Offset, InstPtr, TypeNames.SByte);
					}
					break;
				case BaseDataType.S:
					{
						InstructionArgumentUtility.ParseAndSetArgument<short>(DataSeg, result, Offset, InstPtr, TypeNames.Short);
					}
					break;
				case BaseDataType.SU:
					{
						InstructionArgumentUtility.ParseAndSetArgument<ushort>(DataSeg, result, Offset, InstPtr, TypeNames.UShort);
					}
					break;
				case BaseDataType.L:
					{
						InstructionArgumentUtility.ParseAndSetArgument<long>(DataSeg, result, Offset, InstPtr, TypeNames.Long);
					}
					break;
				case BaseDataType.LU:
					{
						InstructionArgumentUtility.ParseAndSetArgument<ulong>(DataSeg, result, Offset, InstPtr, TypeNames.ULong);
					}
					break;
				case BaseDataType.IU:
					{
						InstructionArgumentUtility.ParseAndSetArgument<uint>(DataSeg, result, Offset, InstPtr, TypeNames.UInt);
					}
					break;
				case BaseDataType.F:
					{
						InstructionArgumentUtility.ParseAndSetArgument<float>(DataSeg, result, Offset, InstPtr, TypeNames.Single);
					}
					break;
				case BaseDataType.D:
					{
						InstructionArgumentUtility.ParseAndSetArgument<double>(DataSeg, result, Offset, InstPtr, TypeNames.Double);
					}
					break;
				default:
					break;
			}
			return true;
		}

	}
}
