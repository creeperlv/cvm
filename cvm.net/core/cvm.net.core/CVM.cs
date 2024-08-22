using cvm.net.core.Functions;
using System;

namespace cvm.net.core
{
	public static unsafe class CVM
	{
		public static void Execute(ExecuteContext core, IDispatcher dispatcher)
		{
			Callframe frame = default;
			core.GetLatestCallFrame(&frame);
			var inst = core.program.LoadedModule[frame.ID].Instructions[frame.PC];
			uint Inst = inst.As<Instruction, ushort>(0);
			uint ADVSEG = inst.As<Instruction, byte>(0);
			if (ADVSEG >= 0xF0)
			{
				uint ADVOP = inst.As<Instruction, byte>(1);
				switch (ADVSEG)
				{
					case InstID.ADV0:


						break;
					default:
						break;
				}
			}
			else
			{
				switch (Inst)
				{
					case InstID.NOP:
						break;
					case InstID.EXIT:
						core.WillRun = false;
						break;
					case InstID.ADD:
						{
							CVMMathFunctions.MathAdd(core, inst);
						}
						break;
					case InstID.SUB:
						{
							CVMMathFunctions.MathSub(core, inst);
						}
						break;
					case InstID.MUL:
						{
							CVMMathFunctions.MathMul(core, inst);
						}
						break;
					case InstID.DIV:
						{
							CVMMathFunctions.MathDiv(core, inst);
						}
						break;
					case InstID.JMP:
						{

						}
						break;
					default:
						break;
				}
			}
			frame.PC++;
			core.SetLastestCallFrame(frame);
		}
	}
}