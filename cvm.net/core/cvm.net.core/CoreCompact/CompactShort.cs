using cvm.net.core.Results;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactShort : INumbericData<CompactShort>
	{
		public short Value;
		public CompactShort(short value) { Value = value; }
		public CompactShort(int value) { Value = (short)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Add(CompactShort R)
		{
			return new CompactShort(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Sub(CompactShort R)
		{
			return new CompactShort(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Mul(CompactShort R)
		{
			return new CompactShort(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactShort Div(CompactShort R)
		{
			return new CompactShort(Value / R.Value);
		}
		public CVMSimpleResult<CompactShort> AddOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactShort>(IsOF, result);
		}

		public CVMSimpleResult<CompactShort> SubOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactShort>(IsOF, result);
		}

		public CVMSimpleResult<CompactShort> DivOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactShort>(IsOF, result);
		}

		public CVMSimpleResult<CompactShort> MulOF(CompactShort R)
		{
			CompactShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactShort(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactShort(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactShort>(IsOF, result);
		}
	}
}
