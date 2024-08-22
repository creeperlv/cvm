using cvm.net.core.Results;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace cvm.net.core.CoreCompact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CompactSByte : INumbericData<CompactSByte>
	{
		public sbyte Value;
		public CompactSByte(sbyte value) { Value = value; }
		public CompactSByte(int value) { Value = (sbyte)value; }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Add(CompactSByte R)
		{
			return new CompactSByte(Value + R.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Sub(CompactSByte R)
		{
			return new CompactSByte(Value - R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Mul(CompactSByte R)
		{
			return new CompactSByte(Value * R.Value);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CompactSByte Div(CompactSByte R)
		{
			return new CompactSByte(Value / R.Value);
		}
		public CVMSimpleResult<CompactSByte> AddOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public CVMSimpleResult<CompactSByte> SubOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public CVMSimpleResult<CompactSByte> DivOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSByte>(IsOF, result);
		}

		public CVMSimpleResult<CompactSByte> MulOF(CompactSByte R)
		{
			CompactSByte result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSByte(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSByte(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSByte>(IsOF, result);
		}
	}

}
