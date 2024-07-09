namespace cvm.net.core.libc
{
	public unsafe static class CString
	{
		public static void memset(byte* ptr, byte v, int len)
		{
			for (int i = 0; i < len; i++)
			{
				ptr[i] = v;
			}
		}
	}
}