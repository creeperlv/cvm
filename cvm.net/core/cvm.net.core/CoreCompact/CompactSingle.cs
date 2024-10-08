﻿using cvm.net.core.Results;
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
		public CVMSimpleResult<CompactSingle> AddOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value + R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value + R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public CVMSimpleResult<CompactSingle> SubOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value - R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value - R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public CVMSimpleResult<CompactSingle> DivOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value / R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value / R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSingle>(IsOF, result);
		}

		public CVMSimpleResult<CompactSingle> MulOF(CompactSingle R)
		{
			CompactSingle result = default;
			bool IsOF = false;
			checked
			{
				try
				{
					result = new CompactSingle(Value * R.Value);
				}
				catch (System.Exception)
				{
					unchecked
					{
						IsOF = true;
						result = new CompactSingle(Value * R.Value);
					}
				}
			}
			return new CVMSimpleResult<CompactSingle>(IsOF, result);
		}
	}

}
