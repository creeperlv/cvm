namespace cvm.net.assembler.core
{
	public interface IDataProcessor
	{
		byte[] Process(string str, string? baseSearchPath = null);
	}
}
