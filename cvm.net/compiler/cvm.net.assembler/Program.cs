using cvm.net.compiler.core;
using cvm.net.core;

namespace cvm.net.assembler
{
	internal class Program
	{
		static void Main(string[] args)
		{

		}
	}
	public class Assembler
	{
		public CVMObject Assemble(Stream stream, string FileName)
		{
			CVMObject cvmObject = new CVMObject();
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
					switch (instID)
					{
						case InstID.EXIT:
							{
								Instruction inst=default;
								inst.Set(instID);
							}
							break;
						default:
							break;
					}
					PC++;
				}
			}
			return cvmObject;
		}
	}
}
