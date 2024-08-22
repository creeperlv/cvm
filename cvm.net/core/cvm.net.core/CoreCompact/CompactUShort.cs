using cvm.net.core.Results;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactUShort : INumbericData<CompactUShort>
	{
		public ushort Value;
		public CompactUShort(ushort value) { Value = value; }
		public CompactUShort(int value) { Value = (ushort)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Add(CompactUShort R)
		{
			return new CompactUShort(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Sub(CompactUShort R)
		{
			return new CompactUShort(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Mul(CompactUShort R)
		{
			return new CompactUShort(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactUShort Div(CompactUShort R)
		{
			return new CompactUShort(Value / R.Value);
		}
		public CVMSimpleResult<CompactUShort> AddOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public CVMSimpleResult<CompactUShort> SubOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public CVMSimpleResult<CompactUShort> DivOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUShort>(IsOF, result);
		}

		public CVMSimpleResult<CompactUShort> MulOF(CompactUShort R)
		{
			CompactUShort result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUShort(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUShort(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUShort>(IsOF, result);
		}
	}

}
