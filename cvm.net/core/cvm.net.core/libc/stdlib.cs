using System;
using System.Runtime.InteropServices;

namespace cvm.net.core.libc
{

	public unsafe static class StdLib
	{
		public static void* malloc(int size)
		{
			return (void*)Marshal.AllocHGlobal(size);
		}
		public static T* malloc<T>(int size) where T : unmanaged
		{
			return (T*)Marshal.AllocHGlobal(size);
		}
		public static void* realloc(void* ptr, int size)
		{
			return (void*)Marshal.ReAllocHGlobal((IntPtr)ptr, (IntPtr)size);
		}
		public static void free(void* ptr)
		{
			Marshal.FreeHGlobal((IntPtr)ptr);
		}
	}
}