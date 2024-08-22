using cvm.net.core.Results;
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

		public CVMSimpleResult<CompactDouble> AddOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public CVMSimpleResult<CompactDouble> SubOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public CVMSimpleResult<CompactDouble> DivOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactDouble>(IsOF, result);
		}

		public CVMSimpleResult<CompactDouble> MulOF(CompactDouble R)
		{
			CompactDouble result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactDouble(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactDouble(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactDouble>(IsOF, result);
		}
	}

}
