using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactSByte : INumbericData<CompactSByte>
	{
		public sbyte Value;
		public CompactSByte(sbyte value) { Value = value; }
		public CompactSByte(int value) { Value = (sbyte)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Add(CompactSByte R)
		{
			return new CompactSByte(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Sub(CompactSByte R)
		{
			return new CompactSByte(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Mul(CompactSByte R)
		{
			return new CompactSByte(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Div(CompactSByte R)
		{
			return new CompactSByte(Value / R.Value);
		}
	}

}
