using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.compiler.core.Errors
{
	public class UnimplementedInstructionError : AssemblerError
	{
		public string InstructionName;

		public UnimplementedInstructionError(string instructionName, Segment segment) : base(segment)
		{
			InstructionName = instructionName;
		}

		public override string ToString()
		{
			return $"Unimplemented Instruction: {InstructionName}";
		}
	}
}
