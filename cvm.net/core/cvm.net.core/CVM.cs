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
										R = core.GetData<byte>(inst.As<Instruction, byte>(5));

										unchecked
										{
											var d = (byte)(L + R);
											core.OF = d < L || d < R;
											core.SetData(TR, d);
										}
									}
									break;
								case BaseDataType.BS:
									{
										byte LR = inst.As<Instruction, byte>(4);
										sbyte R;
										int T = inst.As<Instruction, byte>(7);
										sbyte L = core.GetData<sbyte>(LR);
										if (IsRegister)
										{
											R = core.GetData<sbyte>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, sbyte>(5);
										}
										unchecked
										{
											var d = (sbyte)(L + R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
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
											var d = (uint)(L + R);
											core.OF = d < L || d < R;
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
											var d = (short)(L + R);
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
											var d = (ushort)(L + R);
											core.OF = d < L || d < R;
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.L:
									{
										byte LR = inst.As<Instruction, byte>(4);
										long R;
										int T = inst.As<Instruction, byte>(7);
										long L = core.GetData<long>(LR);
										if (IsRegister)
										{
											R = core.GetData<long>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, long>(5);
										}
										unchecked
										{
											var d = (long)(L + R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.LU:
									{
										byte LR = inst.As<Instruction, byte>(4);
										ulong R;
										int T = inst.As<Instruction, byte>(7);
										ulong L = core.GetData<ulong>(LR);
										if (IsRegister)
										{
											R = core.GetData<ulong>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, ulong>(5);
										}
										unchecked
										{
											var d = (ulong)(L + R);
											core.OF = d < L || d < R;
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.F:
									{
										byte LR = inst.As<Instruction, byte>(4);
										float R;
										int T = inst.As<Instruction, byte>(7);
										float L = core.GetData<float>(LR);
										if (IsRegister)
										{
											R = core.GetData<float>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, float>(5);
										}
										unchecked
										{
											var d = (float)(L + R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.D:
									{
										byte LR = inst.As<Instruction, byte>(4);
										double R;
										int T = inst.As<Instruction, byte>(7);
										double L = core.GetData<double>(LR);
										if (IsRegister)
										{
											R = core.GetData<double>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, double>(5);
										}
										unchecked
										{
											var d = (double)(L + R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
										}
									}
									break;
								default:
									break;
							}
						}
						break;
					case InstID.SUB:
						{
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
											var d = (byte)(L - R);
											core.OF = d > L;
											core.SetData(TR, d);
										}
									}
									break;
								case BaseDataType.BS:
									{
										byte LR = inst.As<Instruction, byte>(4);
										sbyte R;
										int T = inst.As<Instruction, byte>(7);
										sbyte L = core.GetData<sbyte>(LR);
										if (IsRegister)
										{
											R = core.GetData<sbyte>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, sbyte>(5);
										}
										unchecked
										{
											var d = (sbyte)(L - R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
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
											var d = (int)(L - R);
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
											var d = (uint)(L - R);
											core.OF = d > L;
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
											var d = (short)(L - R);
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
											var d = (ushort)(L - R);
											core.OF = d > L;
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.L:
									{
										byte LR = inst.As<Instruction, byte>(4);
										long R;
										int T = inst.As<Instruction, byte>(7);
										long L = core.GetData<long>(LR);
										if (IsRegister)
										{
											R = core.GetData<long>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, long>(5);
										}
										unchecked
										{
											var d = (long)(L - R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.LU:
									{
										byte LR = inst.As<Instruction, byte>(4);
										ulong R;
										int T = inst.As<Instruction, byte>(7);
										ulong L = core.GetData<ulong>(LR);
										if (IsRegister)
										{
											R = core.GetData<ulong>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, ulong>(5);
										}
										unchecked
										{
											var d = (ulong)(L - R);
											core.OF = d > L;
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.F:
									{
										byte LR = inst.As<Instruction, byte>(4);
										float R;
										int T = inst.As<Instruction, byte>(7);
										float L = core.GetData<float>(LR);
										if (IsRegister)
										{
											R = core.GetData<float>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, float>(5);
										}
										unchecked
										{
											var d = (float)(L - R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.D:
									{
										byte LR = inst.As<Instruction, byte>(4);
										double R;
										int T = inst.As<Instruction, byte>(7);
										double L = core.GetData<double>(LR);
										if (IsRegister)
										{
											R = core.GetData<double>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, double>(5);
										}
										unchecked
										{
											var d = (double)(L - R);
											core.OF = ((L < 0 && R < 0) && d >= 0) || ((L > 0 && R > 0) && d <= 0);
											core.SetData(T, d);
										}
									}
									break;
								default:
									break;
							}
						}
						break;
					case InstID.MUL:
						{
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
											var d = (ushort)(L * R);
											core.OF = d > byte.MaxValue;
											core.SetData(TR, d);
										}
									}
									break;
								case BaseDataType.BS:
									{
										byte LR = inst.As<Instruction, byte>(4);
										sbyte R;
										int T = inst.As<Instruction, byte>(7);
										sbyte L = core.GetData<sbyte>(LR);
										if (IsRegister)
										{
											R = core.GetData<sbyte>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, sbyte>(5);
										}
										checked
										{
											sbyte d;
											try
											{
												d = (sbyte)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (sbyte)(L * R);
												}
											}
											core.SetData(T, d);
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
										checked
										{
											int d;
											try
											{
												d = (int)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (int)(L * R);
												}
											}
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
										checked
										{
											uint d;
											try
											{
												d = (uint)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (uint)(L * R);
												}
											}
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
										checked
										{
											short d;
											try
											{
												d = (short)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (short)(L * R);
												}
											}
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
										checked
										{
											ushort d;
											try
											{
												d = (ushort)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (ushort)(L * R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.L:
									{
										byte LR = inst.As<Instruction, byte>(4);
										long R;
										int T = inst.As<Instruction, byte>(7);
										long L = core.GetData<long>(LR);
										if (IsRegister)
										{
											R = core.GetData<long>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, long>(5);
										}
										checked
										{
											long d;
											try
											{
												d = (long)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (long)(L * R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.LU:
									{
										byte LR = inst.As<Instruction, byte>(4);
										ulong R;
										int T = inst.As<Instruction, byte>(7);
										ulong L = core.GetData<ulong>(LR);
										if (IsRegister)
										{
											R = core.GetData<ulong>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, ulong>(5);
										}
										checked
										{
											ulong d;
											try
											{
												d = (ulong)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (ulong)(L * R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.F:
									{
										byte LR = inst.As<Instruction, byte>(4);
										float R;
										int T = inst.As<Instruction, byte>(7);
										float L = core.GetData<float>(LR);
										if (IsRegister)
										{
											R = core.GetData<float>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, float>(5);
										}
										checked
										{
											float d;
											try
											{
												d = (float)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (float)(L * R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.D:
									{
										byte LR = inst.As<Instruction, byte>(4);
										double R;
										int T = inst.As<Instruction, byte>(7);
										double L = core.GetData<double>(LR);
										if (IsRegister)
										{
											R = core.GetData<double>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, double>(5);
										}
										checked
										{
											double d;
											try
											{
												d = (double)(L * R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (double)(L * R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								default:
									break;
							}
						}
						break;
					case InstID.DIV:
						{
							var type = inst.As<Instruction, byte>(2);
							//var IsRegister = inst.As<Instruction, byte>(3) == 1;
							switch (type)
							{
								case BaseDataType.BU:
									{
										byte LR = inst.As<Instruction, byte>(4);
										byte R;
										byte TR = inst.As<Instruction, byte>(7);
										byte L = core.GetData<byte>(LR);
										R = core.GetData<byte>(inst.As<Instruction, byte>(5));
										checked
										{
											byte d;
											try
											{
												d = (byte)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (byte)(L / R);
												}
											}
											core.SetData(TR, d);
										}
									}
									break;
								case BaseDataType.BS:
									{
										byte LR = inst.As<Instruction, byte>(4);
										sbyte R;
										int T = inst.As<Instruction, byte>(7);
										sbyte L = core.GetData<sbyte>(LR);
										if (IsRegister)
										{
											R = core.GetData<sbyte>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, sbyte>(5);
										}
										checked
										{
											sbyte d;
											try
											{
												d = (sbyte)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (sbyte)(L / R);
												}
											}
											core.SetData(T, d);
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
										checked
										{
											int d;
											try
											{
												d = (int)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (int)(L / R);
												}
											}
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
										checked
										{
											uint d;
											try
											{
												d = (uint)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (uint)(L / R);
												}
											}
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
										checked
										{
											short d;
											try
											{
												d = (short)(L / R);
											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
												}
												d = (short)(L / R);
											}
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
										checked
										{
											ushort d;
											try
											{
												d = (ushort)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (ushort)(L / R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.L:
									{
										byte LR = inst.As<Instruction, byte>(4);
										long R;
										int T = inst.As<Instruction, byte>(7);
										long L = core.GetData<long>(LR);
										if (IsRegister)
										{
											R = core.GetData<long>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, long>(5);
										}
										checked
										{
											long d;
											try
											{
												d = (long)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (long)(L / R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.LU:
									{
										byte LR = inst.As<Instruction, byte>(4);
										ulong R;
										int T = inst.As<Instruction, byte>(7);
										ulong L = core.GetData<ulong>(LR);
										if (IsRegister)
										{
											R = core.GetData<ulong>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, ulong>(5);
										}
										checked
										{
											ulong d;
											try
											{
												d = (ulong)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (ulong)(L / R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.F:
									{
										byte LR = inst.As<Instruction, byte>(4);
										float R;
										int T = inst.As<Instruction, byte>(7);
										float L = core.GetData<float>(LR);
										if (IsRegister)
										{
											R = core.GetData<float>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, float>(5);
										}
										checked
										{
											float d;
											try
											{
												d = (float)(L / R);
											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (float)(L / R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								case BaseDataType.D:
									{
										byte LR = inst.As<Instruction, byte>(4);
										double R;
										int T = inst.As<Instruction, byte>(7);
										double L = core.GetData<double>(LR);
										if (IsRegister)
										{
											R = core.GetData<double>(inst.As<Instruction, byte>(5));
										}
										else
										{
											R = inst.As<Instruction, double>(5);
										}
										checked
										{
											double d;
											try
											{
												d = (double)(L / R);

											}
											catch (Exception)
											{
												core.OF = true;
												unchecked
												{
													d = (double)(L / R);
												}
											}
											core.SetData(T, d);
										}
									}
									break;
								default:
									break;
							}
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