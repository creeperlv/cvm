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
	public class UnknownBaseTypeError(Segment errorSegment) : AssemblerError(errorSegment)
	{
		public override string ToString()
		{
			return "Unknown Base Type!";
		}
	}
	public class TypeMismatchError : AssemblerError
	{
		private string targetType;
		public TypeMismatchError(Segment errorSegment, string TargetType) : base(errorSegment) => targetType = TargetType;
		public override string ToString()
		{
			return $"Expect {targetType}!";
		}
	}
	public class AssemblerError : Error
	{
		public Segment ErrorSegment;

		public AssemblerError(Segment errorSegment)
		{
			ErrorSegment = errorSegment;
		}
	}
}
