using cvm.net.core;

namespace cvm.net.compiler.core
{
	[Serializable]
	public class CVMObject
	{
		public Version ObjectVersion { get; set; } = Constants.CurrentCVMObjectVersion;
		public Dictionary<string, int> Labels { get; set; } = [];
		public Dictionary<string, int> Symbols { get; set; } = [];
		public Dictionary<string, int> Libs { get; set; } = [];
		public List<string> UndefinedLabels { get; set; } = [];
		public Dictionary<string, string> Consts { get; set; } = [];
		public Dictionary<int, string> Data { get; set; } = [];
		public List<Instruction> instructions { get; set; } = [];
	}
}
