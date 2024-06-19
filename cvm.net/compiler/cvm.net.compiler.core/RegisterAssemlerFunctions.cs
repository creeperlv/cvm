using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler.core
{
	public static unsafe class RegisterAssemlerFunctions
	{
		public unsafe static bool Assemble_SET(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			switch (instID)
			{
				case InstID.SET:
					break;
				default:
					return true;
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

			return true;
		}

	}
}
