using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler.core
{
	public static unsafe class MemoryAssemlerFunctions
	{
		public unsafe static bool Assemble_MCP(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.MCP:
					break;
				default:
					return false;
			}
			InstPtr.SetData(InstID.MCP);
			SegmentTraveler st = new(s);
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
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			if (!DataConversion.TryParseRegister(SrcSeg.content, result, out var Src))
			{
				result.AddError(new TypeMismatchError(SrcSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(TgtSeg.content, result, out var Tgt))
			{
				result.AddError(new TypeMismatchError(TgtSeg, TypeNames.Register));
				return false;
			}
			var LenSeg = st.Current;
			bool IsRegister = false;
			if (DataConversion.TryParseRegister(LenSeg.content, result, out var LenReg))
			{
				IsRegister = true;
				InstPtr.SetData(LenReg, 5);
			}
			else if (DataConversion.TryParse<UInt32>(LenSeg.content, result, out var Len))
			{
				IsRegister=false;
				InstPtr.SetData(Len, 5);
			}
			InstPtr.SetData(IsRegister ? 1 : 0, 2);
			InstPtr.SetData(Src, 3);
			InstPtr.SetData(Tgt, 4);
			return true;
		}
		public unsafe static bool Assemble_SDLD(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.SD:
				case InstID.LD:
					break;
				default:
					return false;
			}
			InstPtr.SetData(instID);

			SegmentTraveler st = new(s);
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
			var SrcSeg = st.Current;
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}
			var TgtSeg = st.Current;
			if (!DataConversion.TryParse<byte>(LenSeg.content, out var len))
			{
				result.AddError(new TypeMismatchError(LenSeg, TypeNames.Byte));
				return false;
			}
			InstPtr.SetData<byte>(len, 2);
			if (!DataConversion.TryParseRegister(SrcSeg.content, out var srcRegister))
			{
				result.AddError(new TypeMismatchError(SrcSeg, TypeNames.Register));
				return false;
			}
			if (!DataConversion.TryParseRegister(TgtSeg.content, out var tgtRegister))
			{
				result.AddError(new TypeMismatchError(TgtSeg, TypeNames.Register));
				return false;
			}
			InstPtr.SetData(srcRegister, 3);
			InstPtr.SetData(tgtRegister, 4);
			return true;
		}

	}
}
