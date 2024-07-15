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
	public class UnexpectedEndError : AssemblerError
	{
		public UnexpectedEndError(Segment errorSegment) : base(errorSegment)
		{
		}

		public override string ToString()
		{
			return "Unexpected End!";
		}
	}
	public class UnknownDataProcessMethodError : AssemblerError
	{
		public UnknownDataProcessMethodError(Segment errorSegment) : base(errorSegment)
		{
		}

		public override string ToString()
		{
			return $"Unknown Data Process Method: \"{ErrorSegment.content}\"";
		}
	}
	public class UnimplementedDataProcessMethodError : AssemblerError
	{
		public UnimplementedDataProcessMethodError(Segment errorSegment) : base(errorSegment)
		{
		}

		public override string ToString()
		{
			return $"Unimplemented Data Process Method: \"{ErrorSegment.content}\"";
		}
	}
	public class UnknownBaseTypeError(Segment errorSegment) : AssemblerError(errorSegment)
	{
		public override string ToString()
		{
			return "Unknown Base Type!";
		}
	}
	public class UnsupportedBaseTypeError(Segment errorSegment) : AssemblerError(errorSegment)
	{
		public override string ToString()
		{
			return "Unsupported Base Type!";
		}
	}
	public class UnknownOperationError(string instructionName, Segment errorSegment) : AssemblerError(errorSegment)
	{
		public override string ToString()
		{
			return $"Unknown operation for instruction \"{instructionName}\"!";
		}
	}
	public class UnknownLRCalcOperationError(Segment errorSegment) : AssemblerError(errorSegment)
	{
		public override string ToString()
		{
			return "Unknown LRCalc Operation!";
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
