using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactSingle : INumbericData<CompactSingle>
	{
		public float Value;
		public CompactSingle(float value) { Value = value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Add(CompactSingle R)
		{
			return new CompactSingle(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Sub(CompactSingle R)
		{
			return new CompactSingle(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Mul(CompactSingle R)
		{
			return new CompactSingle(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSingle Div(CompactSingle R)
		{
			return new CompactSingle(Value / R.Value);
		}
	}

}
