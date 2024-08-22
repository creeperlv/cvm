using cvm.net.core.Results;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
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
		public CVMSimpleResult<CompactULong> AddOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactULong>(IsOF, result);
		}

		public CVMSimpleResult<CompactULong> SubOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactULong>(IsOF, result);
		}

		public CVMSimpleResult<CompactULong> DivOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactULong>(IsOF, result);
		}

		public CVMSimpleResult<CompactULong> MulOF(CompactULong R)
		{
			CompactULong result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactULong(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactULong(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactULong>(IsOF, result);
		}
	}

}
