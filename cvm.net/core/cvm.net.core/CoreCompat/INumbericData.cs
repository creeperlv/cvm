using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace cvm.net.core.CoreCompat
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
	}

}
