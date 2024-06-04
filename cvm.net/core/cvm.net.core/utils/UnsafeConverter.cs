using System;

public static unsafe class UnsafeConverter
{
	public static V As<T, V>(this T t, int Offset = 0) where V : unmanaged where T : unmanaged
	{
		return ((V*)&t)[Offset];
	}
	public static T As<T>(this Span<byte> data, int Offset = 0) where T : unmanaged
	{
		fixed (byte* ptr = data)
		{
			return ((T*)(ptr + Offset))[0];
		}
	}
	public static T As<T>(this IntPtr ptr, int Offset = 0) where T : unmanaged
	{
		{
			return ((T*)(ptr + Offset))[0];
		}
	}
}