using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactLong : INumbericData<CompactLong>
	{
		public long Value;
		public CompactLong(long value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Add(CompactLong R)
		{
			return new CompactLong(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Sub(CompactLong R)
		{
			return new CompactLong(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Mul(CompactLong R)
		{
			return new CompactLong(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactLong Div(CompactLong R)
		{
			return new CompactLong(Value / R.Value);
		}
	}

}
