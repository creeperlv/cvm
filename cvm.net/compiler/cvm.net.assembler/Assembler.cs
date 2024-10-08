﻿using cvm.net.assembler.core;
using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;

namespace cvm.net.assembler
{
	public class Assembler
	{
		public unsafe OperationResult<CVMObject> Assemble(Stream stream, string? FileFolder, string FileName, OperationResult<CVMObject>? previousCompile)
		{
			OperationResult<CVMObject> OResult = previousCompile ?? new CVMObject();
			StreamReader streamReader = new StreamReader(stream);
			AssemblyScanner scanner = new AssemblyScanner();
			int PC = 0;
			ASMSections section = ASMSections.Code;
			while (true)
			{
				var line = streamReader.ReadLine();
				if (line == null)
				{
					break;
				}
				var HEAD = scanner.Scan(line, false, FileName);
				var Next = HEAD.Next;
				var HEAD_NAME = HEAD.content;
				if (ISADefinition.CurrentDefinition.Sections.TryGetValue(HEAD_NAME, out ASMSections value))
				{
					section = value;
					continue;
				}
				switch (section)
				{
					case ASMSections.Data:
						{
							SegmentTraveler st = new SegmentTraveler(HEAD);
							var Name = st.Current;
							if (!st.GoNext())
							{
								OResult.AddError(new UnexpectedEndError(st.Current));
								continue;
							}
							var DataType = st.Current;

							if (!st.GoNext())
							{
								OResult.AddError(new UnexpectedEndError(st.Current));
								continue;
							}
							var Data = st.Current;
							if (!ISADefinition.CurrentDefinition.DataProcessMethods.TryGetValue(DataType.content, out var m))
							{
								OResult.AddError(new UnknownDataProcessMethodError(st.Current));
								continue;
							}
							if (!ISADefinition.CurrentDefinition.Processors.TryGetValue(m, out var processor))
							{
								OResult.AddError(new UnimplementedDataProcessMethodError(st.Current));
								continue;
							}
							var data = processor.Process(Data.content, FileFolder);
							OResult.Result.Data.Add(Name.content, data);
						}
						break;
					case ASMSections.Code:
						if (ISADefinition.CurrentDefinition.Names.TryGetValue(HEAD_NAME, out var instID))
						{
							if (AssemblerFunctionDefinition.AssembleFunctions.TryGetValue(instID, out var assemble))
							{
								Instruction inst = default;
								if (assemble(instID, HEAD, OResult, (IntPtr)(&inst), PC))
								{
									OResult.Result.instructions.Add(inst);
									PC++;
								}
							}
							else
							{
								if (Next != null)
								{
									if (Next.content == ":")
									{
										OResult.Result.Labels.Add(HEAD_NAME, PC);
										continue;
									}
								}
								OResult.AddError(new UnimplementedInstructionError(HEAD_NAME, HEAD));
							}
						}
						break;
					case ASMSections.Definitions:
						{
							if (ISADefinition.CurrentDefinition.Definitions.TryGetValue(HEAD_NAME.ToLower(), out var defType))
							{
								switch (defType)
								{
									case ASMDefinition.Const:
										break;
									case ASMDefinition.Lib:
										break;
									case ASMDefinition.Symbol:
										break;
									default:
										break;
								}
							}
						}
						break;
					default:
						break;
				}
			}
			return OResult;
		}
	}
}
