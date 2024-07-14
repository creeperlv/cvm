using cvm.net.assembler.core;

namespace cvm.net.compiler.core.DataProcessors
{
	public class Base64Processor : IDataProcessor
	{
		public byte[] Process(string str, string baseSearchPath? = null)
		{
			return Convert.FromBase64String(str);
		}
	}
}
