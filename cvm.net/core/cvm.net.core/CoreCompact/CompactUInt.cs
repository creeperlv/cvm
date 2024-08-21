using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactUInt : INumbericData<CompactUInt>
	{
		public uint Value;
		public CompactUInt(uint value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Add(CompactUInt R)
		{
			return new CompactUInt(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Sub(CompactUInt R)
		{
			return new CompactUInt(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Mul(CompactUInt R)
		{
			return new CompactUInt(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUInt Div(CompactUInt R)
		{
			return new CompactUInt(Value / R.Value);
		}
	}

}
