using cvm.net.fullvm.core.Disk;

namespace cvm.net.fullvm.core.Diagnosis
{
	public class DataInspector
	{
		public unsafe static void InspectData<T>(T data) where T : unmanaged
		{
			byte* ptr = (byte*)&data;
			for (int i = 0; i < sizeof(T); i++)
			{
				PrintHEX(ptr[i]);
				if ((i + 1) % 0xF == 0 && i != 0)
				{
					Console.WriteLine();
				}
			}

		}
		public unsafe static void InspectLBASector(LBABlock blk)
		{

			for (int i = 0; i < DiskDefinitions.LBASectorSize; i++)
			{
				PrintHEX(blk.Data[i]);
				if ((i + 1) % 0xF == 0 && i != 0)
				{
					Console.WriteLine();
				}
			}
		}
		public static void PrintHexDigit(int i)
		{
			if (i < 10)
			{
				Console.Write((char)('0' + i));
			}
			else
			{
				Console.Write((char)('A' + i - 0xA));
			}
		}
		public static void PrintHEX(byte b)
		{
			PrintHexDigit(b / 0x10);
			PrintHexDigit(b & 0x0F);
			Console.Write(' ');
		}
	}
}
