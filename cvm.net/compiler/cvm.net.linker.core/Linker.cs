using cvm.net.compiler.core;
using cvm.net.core;
using LibCLCC.NET.Operations;

namespace cvm.net.linker.core
{
	public class Linker
	{
		public OperationResult<bool> Combine(CVMObject L, CVMObject R)
		{
			OperationResult<bool> result = false;

			result.Result = true;
			return result;
		}
		public void FinalizeBinary(CVMObject obj, Ptr<CVMModule> module)
		{

		}
		public void SignBinary(IntPtr Binary, int Length, Stream output)
		{

		}
	}
}
