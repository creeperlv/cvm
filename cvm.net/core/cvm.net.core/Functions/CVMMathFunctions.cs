using cvm.net.core.CoreCompact;
using System;
using System.Collections.Generic;
using System.Text;

namespace cvm.net.core.Functions
{
	public static class CVMMathFunctions
	{
		public static void MathAdd(ExecuteContext context, Instruction inst)
		{
			var type = inst.As<Instruction, byte>(2);
			var ofCheck = inst.As<Instruction, byte>(3);
			var L = inst.As<Instruction, byte>(4);
			var R = inst.As<Instruction, byte>(5);
			var T = inst.As<Instruction, byte>(6);

			switch (type)
			{
				case BaseDataType.BU:
					GenericAdd<CompactByte>(context, L, R, T);
					break;
				case BaseDataType.BS:
					GenericAdd<CompactSByte>(context, L, R, T);
					break;
				case BaseDataType.S:
					GenericAdd<CompactShort>(context, L, R, T);
					break;
				case BaseDataType.SU:
					GenericAdd<CompactUShort>(context, L, R, T);
					break;
				case BaseDataType.I:
					GenericAdd<CompactInt>(context, L, R, T);
					break;
				case BaseDataType.IU:
					GenericAdd<CompactUInt>(context, L, R, T);
					break;
				case BaseDataType.L:
					GenericAdd<CompactLong>(context, L, R, T);
					break;
				case BaseDataType.LU:
					GenericAdd<CompactULong>(context, L, R, T);
					break;
				case BaseDataType.F:
					GenericAdd<CompactSingle>(context, L, R, T);
					break;
				case BaseDataType.D:
					GenericAdd<CompactDouble>(context, L, R, T);
					break;
			}
		}
		public static void MathSub(ExecuteContext context, Instruction inst)
		{
			var type = inst.As<Instruction, byte>(2);
			var ofCheck = inst.As<Instruction, byte>(3);
			var L = inst.As<Instruction, byte>(4);
			var R = inst.As<Instruction, byte>(5);
			var T = inst.As<Instruction, byte>(6);

			switch (type)
			{
				case BaseDataType.BU:
					GenericSub<CompactByte>(context, L, R, T);
					break;
				case BaseDataType.BS:
					GenericSub<CompactSByte>(context, L, R, T);
					break;
				case BaseDataType.S:
					GenericSub<CompactShort>(context, L, R, T);
					break;
				case BaseDataType.SU:
					GenericSub<CompactUShort>(context, L, R, T);
					break;
				case BaseDataType.I:
					GenericSub<CompactInt>(context, L, R, T);
					break;
				case BaseDataType.IU:
					GenericSub<CompactUInt>(context, L, R, T);
					break;
				case BaseDataType.L:
					GenericSub<CompactLong>(context, L, R, T);
					break;
				case BaseDataType.LU:
					GenericSub<CompactULong>(context, L, R, T);
					break;
				case BaseDataType.F:
					GenericSub<CompactSingle>(context, L, R, T);
					break;
				case BaseDataType.D:
					GenericSub<CompactDouble>(context, L, R, T);
					break;
			}
		}
		public static void MathMul(ExecuteContext context, Instruction inst)
		{
			var type = inst.As<Instruction, byte>(2);
			var ofCheck = inst.As<Instruction, byte>(3);
			var L = inst.As<Instruction, byte>(4);
			var R = inst.As<Instruction, byte>(5);
			var T = inst.As<Instruction, byte>(6);

			switch (type)
			{
				case BaseDataType.BU:
					GenericMul<CompactByte>(context, L, R, T);
					break;
				case BaseDataType.BS:
					GenericMul<CompactSByte>(context, L, R, T);
					break;
				case BaseDataType.S:
					GenericMul<CompactShort>(context, L, R, T);
					break;
				case BaseDataType.SU:
					GenericMul<CompactUShort>(context, L, R, T);
					break;
				case BaseDataType.I:
					GenericMul<CompactInt>(context, L, R, T);
					break;
				case BaseDataType.IU:
					GenericMul<CompactUInt>(context, L, R, T);
					break;
				case BaseDataType.L:
					GenericMul<CompactLong>(context, L, R, T);
					break;
				case BaseDataType.LU:
					GenericMul<CompactULong>(context, L, R, T);
					break;
				case BaseDataType.F:
					GenericMul<CompactSingle>(context, L, R, T);
					break;
				case BaseDataType.D:
					GenericMul<CompactDouble>(context, L, R, T);
					break;
			}
		}
		public static void MathDiv(ExecuteContext context, Instruction inst)
		{
			var type = inst.As<Instruction, byte>(2);
			var ofCheck = inst.As<Instruction, byte>(3);
			var L = inst.As<Instruction, byte>(4);
			var R = inst.As<Instruction, byte>(5);
			var T = inst.As<Instruction, byte>(6);

			switch (type)
			{
				case BaseDataType.BU:
					GenericDiv<CompactByte>(context, L, R, T);
					break;
				case BaseDataType.BS:
					GenericDiv<CompactSByte>(context, L, R, T);
					break;
				case BaseDataType.S:
					GenericDiv<CompactShort>(context, L, R, T);
					break;
				case BaseDataType.SU:
					GenericDiv<CompactUShort>(context, L, R, T);
					break;
				case BaseDataType.I:
					GenericDiv<CompactInt>(context, L, R, T);
					break;
				case BaseDataType.IU:
					GenericDiv<CompactUInt>(context, L, R, T);
					break;
				case BaseDataType.L:
					GenericDiv<CompactLong>(context, L, R, T);
					break;
				case BaseDataType.LU:
					GenericDiv<CompactULong>(context, L, R, T);
					break;
				case BaseDataType.F:
					GenericDiv<CompactSingle>(context, L, R, T);
					break;
				case BaseDataType.D:
					GenericDiv<CompactDouble>(context, L, R, T);
					break;
			}
		}
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
