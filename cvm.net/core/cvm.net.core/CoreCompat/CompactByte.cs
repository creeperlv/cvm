using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompat
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactByte : INumbericData<CompactByte>
	{
		public byte Value;
		public CompactByte(byte value) { Value = value; }
		public CompactByte(int value) { Value = (byte)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Add(CompactByte R)
		{
			return new CompactByte(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Sub(CompactByte R)
		{
			return new CompactByte(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Mul(CompactByte R)
		{
			return new CompactByte(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactByte Div(CompactByte R)
		{
			return new CompactByte(Value / R.Value);
		}
	}

}
