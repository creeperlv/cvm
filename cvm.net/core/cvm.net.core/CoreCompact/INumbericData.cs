using cvm.net.core.Results;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace cvm.net.core.CoreCompact
{
	public interface INumbericData<T> where T : unmanaged, INumbericData<T>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Add(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Sub(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Mul(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		T Div(T R);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		CVMSimpleResult<T> AddOF(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		CVMSimpleResult<T> SubOF(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		CVMSimpleResult<T> DivOF(T R);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		CVMSimpleResult<T> MulOF(T R);
	}

}
