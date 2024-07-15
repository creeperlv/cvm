using cvm.net.cli.core;
using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.fullvm.core.Diagnosis;
using LibCLCC.NET.Operations;
using System.Text.Json;

namespace cvm.net.assembler
{
	internal class Program
	{
		static void Main(string[] args)
		{
			CLIOptions options = new();
			for (int i = 0; i < args.Length; i++)
			{
				string item = args[i];
				switch (item.ToLower())
				{
					case "-o":
					case "--output":
						{
							i++;
							item = args[i];
							options.Set(OptionNames.OutputFile, item);
						}
						break;
					default:
						{
							options.Append(OptionNames.SourceFile, item);
						}
						break;
				}
			}
			Assembler assembler = new Assembler();
			{
				var sourceList = options.GetList(OptionNames.SourceFile);
				OperationResult<CVMObject>? result = null;
				foreach (var item in sourceList)
				{
					FileInfo fileInfo = new FileInfo(item);
					using var stream = File.Open(item, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
					result = assembler.Assemble(stream, fileInfo.DirectoryName, item, result);
				}
				{
					if (result is null)
					{
						Console.WriteLine("No result!");
						return;
					}

					if (!(result.HasError()))
					{
						var obj = result.Result;
						CVMObjectContext context = new CVMObjectContext();
						Console.WriteLine(JsonSerializer.Serialize(obj, context.CVMObject));
					}
					else
					{

						foreach (var item in result.Errors)
						{
							if (item is AssemblerError ae)
								Console.WriteLine($"Error:{ae.ToString()} at:{ae.ErrorSegment.content}({ae.ErrorSegment.ID}:{ae.ErrorSegment.Index})");
						}
					}
				}
			}
		}
	}
}
