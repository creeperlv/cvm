using System;
using System.Text;

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
	public static unsafe string CStr2DotNetStr(this IntPtr ptr)
	{
		byte* bPtr = (byte*)ptr;
		int len = 0;
		while (true)
		{
			if (bPtr[len] == 0) break;
			len++;
		}
		return Encoding.ASCII.GetString(new Span<byte>(bPtr, len));
	}
}
