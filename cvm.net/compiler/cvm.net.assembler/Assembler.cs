using cvm.net.compiler.core;
using cvm.net.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler
{
	public static unsafe class AssemlerFunctions
	{
		public static Dictionary<uint, Func<Segment, OperationResult<CVMObject>, IntPtr, int, bool>> AssembleFunctions =
			new(){
				{InstID.EXIT,Assemble_Exit },
				};
		public unsafe static bool Assemble_Exit(Segment s, OperationResult<CVMObject> result, IntPtr instPtr, int PC)
		{
			Instruction inst = default;
			inst.Set(InstID.EXIT);
			((Instruction*)instPtr)[0] = inst;
			return true;
		}
	}
	public class Assembler
	{
		public unsafe OperationResult<CVMObject> Assemble(Stream stream, string FileName)
		{
			OperationResult<CVMObject> OResult = new CVMObject();
			StreamReader streamReader = new StreamReader(stream);
			AssemblyScanner scanner = new AssemblyScanner();
			int PC = 0;
			while (true)
			{
				var line = streamReader.ReadLine();
				if (line == null)
				{
					break;
				}
				var HEAD = scanner.Scan(line, false, FileName);
				if (ISADefinition.CurrentDefinition.Names.TryGetValue(HEAD.content, out var instID))
				{
					if (AssemlerFunctions.AssembleFunctions.TryGetValue(instID, out var assemble))
					{
						Instruction inst = default;
						if (assemble(HEAD, OResult, (IntPtr)(&inst), PC))
						{
							OResult.Result.instructions.Add(inst);
							PC++;
						}

					}
				}
			}
			return OResult;
		}
	}
}
