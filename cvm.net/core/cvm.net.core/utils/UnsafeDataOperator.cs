using System;

public static class UnsafeDataOperator
{
	public static unsafe void Set<T, V>(this T t, V v, int OffsetInBytes = 0) where T : unmanaged where V : unmanaged
	{
		byte* ptr = ((byte*)&t) + OffsetInBytes;
		((V*)ptr)[0] = v;
	}
	public static unsafe void SetData< V>(this IntPtr t, V v, int OffsetInBytes = 0) where V : unmanaged
	{
		byte* ptr = ((byte*)t) + OffsetInBytes;
		((V*)ptr)[0] = v;
	}
}