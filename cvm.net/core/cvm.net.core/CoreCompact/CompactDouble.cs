using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactDouble : INumbericData<CompactDouble>
	{
		public double Value;
		public CompactDouble(double value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Add(CompactDouble R)
		{
			return new CompactDouble(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Sub(CompactDouble R)
		{
			return new CompactDouble(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Mul(CompactDouble R)
		{
			return new CompactDouble(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactDouble Div(CompactDouble R)
		{
			return new CompactDouble(Value / R.Value);
		}
	}

}
