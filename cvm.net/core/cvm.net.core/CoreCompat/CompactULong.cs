using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompat
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactULong : INumbericData<CompactULong>
	{
		public ulong Value;
		public CompactULong(ulong value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Add(CompactULong R)
		{
			return new CompactULong(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Sub(CompactULong R)
		{
			return new CompactULong(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Mul(CompactULong R)
		{
			return new CompactULong(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactULong Div(CompactULong R)
		{
			return new CompactULong(Value / R.Value);
		}
	}

}
