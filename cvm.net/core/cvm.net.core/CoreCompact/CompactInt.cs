using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactInt : INumbericData<CompactInt>
	{
		public int Value;
		public CompactInt(int value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Add(CompactInt R)
		{
			return new CompactInt(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Sub(CompactInt R)
		{
			return new CompactInt(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Mul(CompactInt R)
		{
			return new CompactInt(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactInt Div(CompactInt R)
		{
			return new CompactInt(Value / R.Value);
		}
	}

}
