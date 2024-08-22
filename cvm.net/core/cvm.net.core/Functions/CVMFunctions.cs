using cvm.net.core.CoreCompact;
using System;
using System.Collections.Generic;
using System.Text;

namespace cvm.net.core.Functions
{
	public static class CVMFunctions
	{
		public static void GenericAdd<N>(ExecuteContext context, int L, int R, int T) where N : unmanaged, INumbericData<N>
		{
			var LN = context.GetData<N>(L);
			var RN = context.GetData<N>(R);
			var TN = LN.Add(RN);
			context.SetData<N>(T, TN);
		}
		public static void GenericSub<N>(ExecuteContext context, int L, int R, int T) where N : unmanaged, INumbericData<N>
		{
			var LN = context.GetData<N>(L);
			var RN = context.GetData<N>(R);
			var TN = LN.Sub(RN);
			context.SetData<N>(T, TN);
		}
		public static void GenericMul<N>(ExecuteContext context, int L, int R, int T) where N : unmanaged, INumbericData<N>
		{
			var LN = context.GetData<N>(L);
			var RN = context.GetData<N>(R);
			var TN = LN.Mul(RN);
			context.SetData<N>(T, TN);
		}
		public static void GenericDiv<N>(ExecuteContext context, int L, int R, int T) where N : unmanaged, INumbericData<N>
		{
			var LN = context.GetData<N>(L);
			var RN = context.GetData<N>(R);
			var TN = LN.Div(RN);
			context.SetData<N>(T, TN);
		}
	}
}
