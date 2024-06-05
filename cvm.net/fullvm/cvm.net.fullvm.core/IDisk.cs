namespace cvm.net.fullvm.core
{
	public interface IDisk
	{
		void SetPosToLBA(long LBABlockAddress);
		void WriteLBA(LBABlock Data, long LBABlockAddress);
		LBABlock ReadLBA(long LBABlockAddress);
		void Flush();
	}
}
