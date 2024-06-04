using System;

namespace cvm.net.core
{
	public static unsafe class CVM
	{
		public static void Execute(CPUCore core, IDispatcher dispatcher)
		{
			Callframe frame = default;
			core.GetLatestCallframe(&frame);
			var inst = core.Machine.Programs[frame.ID].module.Instructions[frame.PC];
			uint Inst = inst.As<Instruction, uint>(0);

			switch (Inst)
			{
				case InstID.NOP:
					break;
				case InstID.ADD:
					var type = inst.As<Instruction, byte>(2);
					var IsRegister = inst.As<Instruction, byte>(3) == 1;
					switch (type)
					{
						case BaseDataType.BU:
							{
								byte L = inst.As<Instruction, byte>(4);
								byte R;
								byte T = inst.As<Instruction, byte>(7);
								if (IsRegister)
								{
									R = core.GetData<byte>(inst.As<Instruction, Int32>(5));
								}
								else
								{
									R = inst.As<Instruction, byte>(6);
								}
								unchecked
								{
									var d = (byte)(L + R);
									core.OF = d < L || d < R;
									core.SetData(T, d);
								}
							}
							break;
						case BaseDataType.I:
							{
								int L = inst.As<Instruction, byte>(4);
								int R;
								int T = inst.As<Instruction, byte>(7);
								if (IsRegister)
								{
									R = core.GetData<byte>(inst.As<Instruction, Int32>(5));
								}
								else
								{
									R = inst.As<Instruction, int>(5);
								}
								unchecked
								{
									var d = (int)(L + R);
									core.OF = d < L || d < R;
									core.SetData(T, d);
								}
							}
							break;
						default:
							break;
					}
					break;
				default:
					break;
			}
		}
	}
}