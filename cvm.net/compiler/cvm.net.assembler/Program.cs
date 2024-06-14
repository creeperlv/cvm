﻿using cvm.net.cli.core;
using cvm.net.compiler.core;
using cvm.net.fullvm.core.Diagnosis;
using LibCLCC.NET.Operations;

namespace cvm.net.assembler
{
	internal class Program
	{
		static void Main(string[] args)
		{
			unsafe
			{
				int i = 0;
				var r = DataConversion.ParseByteDataFromBinaryString("1_0000_0000", (byte*)&i, sizeof(int));
				Console.WriteLine($"{r}:{i}");
				i=0;
				r = DataConversion.ParseByteDataFromHexString("1F", (byte*)&i, sizeof(int));
				Console.WriteLine($"{r}:{i}");
				DataInspector.InspectData(i);
				Console.WriteLine();
				r = DataConversion.ParseByteDataFromHexString("F0F", (byte*)&i, sizeof(int));
				Console.WriteLine($"{r}:{i}");
				DataInspector.InspectData(i);
				Console.WriteLine();
			}
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
					using var stream = File.Open(item, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
					result = assembler.Assemble(stream, item, result);
				}
				if (!(result?.HasError() ?? true))
				{
					var obj = result.Result;

				}
			}
		}
	}
	public static class OptionNames
	{
		public readonly static string SourceFile = "source";
		public readonly static string OutputFile = "output";
	}
}
