using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvm.net.compiler.core.AssemblerFunctions
{
	public static unsafe class IOAssemblerFunctions
	{
		public unsafe static bool Assemble_IO(ushort instID, Segment s, OperationResult<CVMObject> result, nint InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.IN:
				case InstID.OUT:
					break;
				default:
					return false;
			}
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var RegSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var PortSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var LenSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}

			if (!DataConversion.TryParseRegister(RegSeg.content, result, out var _Reg))
			{
				result.AddError(new TypeMismatchError(RegSeg, TypeNames.Register));
				return false;
			}
			InstPtr.Set(_Reg, 4);

			InstPtr.Set((byte)0, 3);
			if (DataConversion.TryParseRegister(PortSeg.content, result, out var __Prot))
			{
				InstPtr.SetOr((byte)10, 3);
				InstPtr.Set((UInt32)__Prot, 5);
			}
			else
			{
				InstructionArgumentUtility.ParseAndSetArgument<ushort>(PortSeg, result, 5, InstPtr, TypeNames.UInt);
			}
			if (DataConversion.TryParseRegister(LenSeg.content, result, out var __Length))
			{
				InstPtr.SetOr((byte)1, 3);
				InstPtr.Set((UInt32)__Length, 5);
			}
			else
			{
				InstructionArgumentUtility.ParseAndSetArgument<ushort>(LenSeg, result, 7, InstPtr, TypeNames.UInt);
			}
			return true;
		}
	}
}
