using cvm.net.compiler.core.Errors;
using cvm.net.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.compiler.core
{
	public static class InstructionArgumentUtility
	{
		public static bool ParseAndSetArgument<T>(Segment R, OperationResult<CVMObject> result, int Offset, nint inst, string TypeName) where T : unmanaged, IParsable<T>
		{

			if (!DataConversion.TryParse(R.content, result, out T RData))
			{
				result.AddError(new TypeMismatchError(R, TypeName));
				return false;
			}
			inst.SetData(RData, Offset);
			return true;
		}
	}
}
