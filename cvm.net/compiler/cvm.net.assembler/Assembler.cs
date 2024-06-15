using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using LibCLCC.NET.Operations;

namespace cvm.net.assembler
{
	public class Assembler
	{
		public unsafe OperationResult<CVMObject> Assemble(Stream stream, string FileName, OperationResult<CVMObject>? previousCompile)
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
						break;
					case ASMSections.Code:
						if (ISADefinition.CurrentDefinition.Names.TryGetValue(HEAD_NAME, out var instID))
						{
							if (AssemlerFunctions.AssembleFunctions.TryGetValue(instID, out var assemble))
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
					case ASMSections.Consts:
						break;
					default:
						break;
				}
			}
			return OResult;
		}
	}
}
