using System.Runtime.InteropServices;
namespace cvm.net.fullvm.core.Output
{
	[StructLayout(LayoutKind.Sequential)]
	public struct VTCharacter
	{
		public int Character;
		public int BG;
		public int FG;
		public int Attribute;
	}
}
