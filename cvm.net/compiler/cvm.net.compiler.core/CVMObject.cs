using cvm.net.core;

namespace cvm.net.compiler.core
{
	public class CVMObject
	{
		public Dictionary<string, int> Labels = [];
		public Dictionary<string, string> Consts = [];
		public Dictionary<int, string> Data = [];
		public List<Instruction> instructions = [];
	}
}
