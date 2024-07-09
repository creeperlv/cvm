using System;
using System.IO;

public static class StreamUtils
{
	public static unsafe void WriteData<T>(this Stream s, T data) where T : unmanaged
	{
		byte* _this = (byte*)(&data);
		ReadOnlySpan<byte> _span = new ReadOnlySpan<byte>(_this, sizeof(T));
		s.Write(_span);
	}
}