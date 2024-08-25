using System;

namespace cvm.net.core
{
	public struct Callframe
	{
		public int ID;
		public int PC;
	}
	public struct GloabalCallframe
	{
		public int ExecutionContextID;
		public int ID;
		public int PC;
	}
}