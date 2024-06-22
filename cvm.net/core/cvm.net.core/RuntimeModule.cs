using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace cvm.net.core
{
	public unsafe class RuntimeModule
	{
		public Instruction* Instructions;
		public int InstructionCount;
		public int GlobalModuleID;
		public List<RTSymbolDefinition> Offsets = new List<RTSymbolDefinition>();
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct RTSymbolDefinition
	{
		public int ModuleID;
		public int Offset;
	}
}
