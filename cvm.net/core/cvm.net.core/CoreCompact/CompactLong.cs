using cvm.net.core.Results;
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
		public CVMSimpleResult<CompactLong> AddOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactLong>(IsOF, result);
		}

		public CVMSimpleResult<CompactLong> SubOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactLong>(IsOF, result);
		}

		public CVMSimpleResult<CompactLong> DivOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactLong>(IsOF, result);
		}

		public CVMSimpleResult<CompactLong> MulOF(CompactLong R)
		{
			CompactLong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactLong(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactLong(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactLong>(IsOF, result);
		}
	}

}
