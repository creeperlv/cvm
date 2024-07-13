using cvm.net.assembler.core;

namespace cvm.net.compiler.core.DataProcessors
{
	public class HEXProcessor : IDataProcessor
	{
		public unsafe byte[] Process(string str)
		{
			var bytes = str.Length / 2 + str.Length % 2;
			byte[] data = new byte[bytes];
			fixed (byte* dataPtr = data)
			{
				DataConversion.ParseByteDataFromHexString(str, dataPtr, data.Length);
			}
			return data;
		}
	}
}
