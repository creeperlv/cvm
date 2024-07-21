using cvm.net.compiler.core;

namespace cvm.net.linker
{
	public static class OptionNames
	{
		public static readonly string SourceFile = "source";
		public static readonly string OutputFile = "output";
		public static readonly string ArtifactType = "artifact";
		public static readonly Dictionary<string, ArtifactType> ArtifactTypeMapping = new()
		{
			{"minimal", compiler.core.ArtifactType.Minimal },
			{"m", compiler.core.ArtifactType.Minimal },
			{"full", compiler.core.ArtifactType.Complete },
			{"f", compiler.core.ArtifactType.Complete },
			{"complete", compiler.core.ArtifactType.Complete },
			{"c", compiler.core.ArtifactType.Complete },
		};
	}
}
