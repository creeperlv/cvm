using cvm.net.core;

namespace cvm.net.compiler.core
{
	[Serializable]
	public class CVMObject
	{
		public Version ObjectVersion = Constants.CurrentCVMObjectVersion;
		public Dictionary<string, int> Labels = [];
		public List<string> UndefinedLabels = [];
		public Dictionary<string, string> Consts = [];
		public Dictionary<int, string> Data = [];
		public List<Instruction> instructions = [];
	}
}
