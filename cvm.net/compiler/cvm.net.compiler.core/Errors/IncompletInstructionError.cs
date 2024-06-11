using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.compiler.core.Errors
{
	public class IncompletInstructionError : AssemblerError
	{
		public IncompletInstructionError(Segment errorSegment) : base(errorSegment)
		{
		}

		public override string ToString()
		{
			return "Incomplete Instruction!";
		}
	}
	public class AssemblerError:Error
	{
		public Segment ErrorSegment;

		public AssemblerError(Segment errorSegment)
		{
			ErrorSegment = errorSegment;
		}
	}
}
