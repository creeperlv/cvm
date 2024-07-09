namespace cvm.net.core
{
	public unsafe struct Ptr<T> where T: unmanaged
	{
		public T* ptr;
	}
}