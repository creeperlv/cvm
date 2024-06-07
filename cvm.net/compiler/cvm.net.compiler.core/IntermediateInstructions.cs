namespace cvm.net.compiler.core
{
	public static class IntermediateInstructions
	{
		public const uint JMP_LBL = 0x1010;
		public const uint JF_LBL = 0x1011;
		public const uint JO_LBL = 0x1012;
		public const uint CALL_LBL = 0x1014;
		public const uint PJMP_LBL = 0x1016;
	}
}
