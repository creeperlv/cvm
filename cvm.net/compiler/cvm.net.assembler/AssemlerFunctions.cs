﻿using cvm.net.compiler.core;
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
				{InstID.EXIT,Assemble_NoArgs },
				{InstID.NOP,Assemble_NoArgs },
				{InstID.ADD,Assemble_BasicMath },
				{InstID.SUB,Assemble_BasicMath },
				{InstID.MUL,Assemble_BasicMath },
				{InstID.DIV,Assemble_BasicMath },
				{InstID.LR_CALC,Assemble_LRCalc },
				};
		public unsafe static bool Assemble_LRCalc(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			if (instID != InstID.LR_CALC)
			{
				return false;
			}
			Instruction instruction = default;
			instruction.Set(InstID.LR_CALC, 0);
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
			instruction.Set(lrop, 2);
			instruction.Set(type, 3);

			if (DataConversion.TryParseRegister(TSeg.content, out var _T))
			{
				result.AddError(new TypeMismatchError(TSeg, TypeNames.Register));
				return false;
			}
			if (DataConversion.TryParseRegister(LSeg.content, out var _L))
			{
				result.AddError(new TypeMismatchError(LSeg, TypeNames.Register));
				return false;
			}
			if (DataConversion.TryParseRegister(RSeg.content, out var _R))
			{
				result.AddError(new TypeMismatchError(RSeg, TypeNames.Register));
				return false;
			}

			instruction.Set(_T, 4);
			instruction.Set(_L, 5);
			instruction.Set(_R, 6);
			((Instruction*)InstPtr)[0] = instruction;
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
			Instruction instruction = default;
			instruction.Set(instID);
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var current = st.Current;
			if (ISADefinition.CurrentDefinition.Types.TryGetValue(current.content.ToLower(), out var type))
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
				nint ptr = (nint)(&instruction);
				switch (type)
				{
					case BaseDataType.BU:
						{
							InstructionArgumentUtility.ParseAndSetArgument<byte>(R, result, 6, ptr, TypeNames.Byte);
						}
						break;
					case BaseDataType.I:
						{
							InstructionArgumentUtility.ParseAndSetArgument<int>(R, result, 6, ptr, TypeNames.Int);
						}
						break;
					case BaseDataType.BS:
						{
							InstructionArgumentUtility.ParseAndSetArgument<sbyte>(R, result, 6, ptr, TypeNames.SByte);
						}
						break;
					case BaseDataType.S:
						{
							InstructionArgumentUtility.ParseAndSetArgument<short>(R, result, 6, ptr, TypeNames.Short);
						}
						break;
					case BaseDataType.SU:
						{
							InstructionArgumentUtility.ParseAndSetArgument<ushort>(R, result, 6, ptr, TypeNames.UShort);
						}
						break;
					case BaseDataType.L:
						{
							InstructionArgumentUtility.ParseAndSetArgument<long>(R, result, 6, ptr, TypeNames.Long);
						}
						break;
					case BaseDataType.LU:
						{
							InstructionArgumentUtility.ParseAndSetArgument<ulong>(R, result, 6, ptr, TypeNames.ULong);
						}
						break;
					case BaseDataType.IU:
						{
							InstructionArgumentUtility.ParseAndSetArgument<uint>(R, result, 6, ptr, TypeNames.UInt);
						}
						break;
					case BaseDataType.F:
						{
							InstructionArgumentUtility.ParseAndSetArgument<float>(R, result, 6, ptr, TypeNames.Single);
						}
						break;
					case BaseDataType.D:
						{
							InstructionArgumentUtility.ParseAndSetArgument<double>(R, result, 6, ptr, TypeNames.Double);
						}
						break;
					default:
						break;
				}
			}
			((Instruction*)InstPtr)[0] = instruction;
			return true;
		}

		public unsafe static bool Assemble_NoArgs(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr instPtr, int PC)
		{
			Instruction inst = default;
			inst.Set(instID);
			((Instruction*)instPtr)[0] = inst;
			return true;
		}
	}
}
