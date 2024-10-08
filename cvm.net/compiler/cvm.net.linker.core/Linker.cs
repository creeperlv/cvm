﻿using cvm.net.compiler.core;
using cvm.net.core;
using LibCLCC.NET.Operations;
using System.Text;
using static cvm.net.core.libc.StdLib;
namespace cvm.net.linker.core
{
	public class Linker
	{
		public OperationResult<bool> Combine(CVMObject L, CVMObject R)
		{
			OperationResult<bool> result = false;

			result.Result = true;
			return result;
		}
		public unsafe void FinalizeBinary(CVMObject obj, IntPtr modulePtr, ArtifactType type)
		{
			int len = obj.instructions.Count;
			switch (type)
			{
				case ArtifactType.Minimal:
					break;
				case ArtifactType.Complete:
					{

						CVMModule module = default;
						module.BaseModule.InstructionCount = len;
						module.BaseModule.Instructions = malloc<Instruction>(sizeof(Instruction) * len);
						{
							using MemoryStream ms = new();
							foreach (var item in obj.Symbols)
							{
								var nameBytes = Encoding.UTF8.GetBytes(item.Key);
								ms.WriteData(nameBytes.Length);
								ms.Write(nameBytes);
								ms.WriteData((byte)0);
								ms.WriteData(item.Value);
							}
							var data = ms.GetBuffer();
							module.SymbolTable = malloc<byte>(data.Length);
							module.SymbolCount = obj.Symbols.Count;
							fixed (byte* src = data)
							{
								Buffer.MemoryCopy(src, module.SymbolTable, data.Length, data.Length);
							}
						}
					}
					break;
				default:
					break;
			}
		}
		public void SignBinary(IntPtr Binary, int Length, Stream output)
		{

		}
	}
}
