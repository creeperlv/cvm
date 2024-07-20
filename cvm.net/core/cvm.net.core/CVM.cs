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
					case InstID.ADD:
						var type = inst.As<Instruction, byte>(2);
						var IsRegister = inst.As<Instruction, byte>(3) == 1;
						switch (type)
						{
							case BaseDataType.BU:
								{
									byte LR = inst.As<Instruction, byte>(4);
									byte R;
									byte TR = inst.As<Instruction, byte>(7);
									byte L = core.GetData<byte>(LR);
									if (IsRegister)
									{
										R = core.GetData<byte>(inst.As<Instruction, byte>(5));
									}
									else
									{
										R = inst.As<Instruction, byte>(6);
									}
									unchecked
									{
										var d = (byte)(L + R);
										core.OF = d < L || d < R;
										core.SetData(TR, d);
									}
								}
								break;
							case BaseDataType.I:
								{
									byte LR = inst.As<Instruction, byte>(4);
									int R;
									int T = inst.As<Instruction, byte>(7);
									int L = core.GetData<int>(LR);
									if (IsRegister)
									{
										R = core.GetData<int>(inst.As<Instruction, byte>(5));
									}
									else
									{
										R = inst.As<Instruction, int>(5);
									}
									unchecked
									{
										var d = (int)(L + R);
										core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
										core.SetData(T, d);
									}
								}
								break;
							case BaseDataType.IU:
								{
									byte LR = inst.As<Instruction, byte>(4);
									uint R;
									int T = inst.As<Instruction, byte>(7);
									uint L = core.GetData<uint>(LR);
									if (IsRegister)
									{
										R = core.GetData<uint>(inst.As<Instruction, byte>(5));
									}
									else
									{
										R = inst.As<Instruction, uint>(5);
									}
									unchecked
									{
										var d = (int)(L + R);
										core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
										core.SetData(T, d);
									}
								}
								break;
							case BaseDataType.S:
								{
									byte LR = inst.As<Instruction, byte>(4);
									short R;
									int T = inst.As<Instruction, byte>(7);
									short L = core.GetData<short>(LR);
									if (IsRegister)
									{
										R = core.GetData<short>(inst.As<Instruction, byte>(5));
									}
									else
									{
										R = inst.As<Instruction, short>(5);
									}
									unchecked
									{
										var d = (int)(L + R);
										core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
										core.SetData(T, d);
									}
								}
								break;
							case BaseDataType.SU:
								{
									byte LR = inst.As<Instruction, byte>(4);
									ushort R;
									int T = inst.As<Instruction, byte>(7);
									ushort L = core.GetData<ushort>(LR);
									if (IsRegister)
									{
										R = core.GetData<ushort>(inst.As<Instruction, byte>(5));
									}
									else
									{
										R = inst.As<Instruction, ushort>(5);
									}
									unchecked
									{
										var d = (int)(L + R);
										core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
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
}