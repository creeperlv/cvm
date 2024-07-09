namespace cvm.net.core
{
	public static class LRCalcOP
	{
		public const byte MOD = 0x00;
		public const byte POW = 0x01;
		public const byte MIN = 0x02;
		public const byte MAX = 0x03;
	}

	public static class Advanced0OP
	{
		public const byte CRC32 = 0x00;
		public const byte VEC3_ADD = 0x01;
		public const byte VEC3_SUB = 0x02;
		public const byte VEC3_MUL = 0x03;
		public const byte VEC3_DIV = 0x04;
	}
	public static class GInfoID
	{
		public const uint CPU_MANUFACTURER = 0x00;
		public const uint CPU_ID = 0x00;
		public const uint CPU_COUNT = 0x00;
	}
}