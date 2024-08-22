using cvm.net.core.Results;
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
		public CVMSimpleResult<CompactUInt> AddOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public CVMSimpleResult<CompactUInt> SubOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public CVMSimpleResult<CompactUInt> DivOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUInt>(IsOF, result);
		}

		public CVMSimpleResult<CompactUInt> MulOF(CompactUInt R)
		{
			CompactUInt result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactUInt(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactUInt(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactUInt>(IsOF, result);
		}
	}

}
