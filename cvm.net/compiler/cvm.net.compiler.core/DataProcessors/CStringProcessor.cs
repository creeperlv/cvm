using cvm.net.assembler.core;
using System.Text;

namespace cvm.net.compiler.core.DataProcessors
{
	public class CStringProcessor : IDataProcessor
	{
		public byte[] Process(string str, string? baseSearchPath = null)
		{
			return Encoding.ASCII.GetBytes(str);
		}
	}
}
