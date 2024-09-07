using System;

public static class UnsafeDataOperator
{
	public static unsafe void Set<V>(this IntPtr t, V v, int OffsetInBytes = 0) where V : unmanaged
	{
		byte* ptr = ((byte*)t) + OffsetInBytes;
		((V*)ptr)[0] = v;
	}
	public static unsafe void SetOr(this IntPtr t, byte v, int OffsetInBytes = 0)
	{
		byte* ptr = ((byte*)t) + OffsetInBytes;
		(ptr)[0] = (byte)(v | (ptr)[0]);
	}
	public static unsafe void SetOr(this IntPtr t, int v, int OffsetInBytes = 0)
	{
		int* ptr = (int*)(((byte*)t) + OffsetInBytes);
		(ptr)[0] = (byte)(v | (ptr)[0]);
	}
	public static unsafe void SetData<V>(this IntPtr t, V v, int OffsetInBytes = 0) where V : unmanaged
	{
		byte* ptr = ((byte*)t) + OffsetInBytes;
		((V*)ptr)[0] = v;
	}
}
