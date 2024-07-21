using cvm.net.cli.core;

namespace cvm.net.linker
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
					case "-a":
					case "--artifact":
						{
							i++;
							item = args[i];
							options.Set(OptionNames.ArtifactType, item);
						}
						break;
					default:
						{
							options.Append(OptionNames.SourceFile, item);
						}
						break;
				}
			}
		}
	}
}
