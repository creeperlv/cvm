using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler
{
	public class AssemblyScanner : GeneralPurposeScanner
	{
		public AssemblyScanner()
		{
			this.PredefinedSegmentCharacters = new List<char> { ':', ',' };
			this.lineCommentIdentifiers =
   [
				new LineCommentIdentifier { StartSequence = ";" },
				new LineCommentIdentifier { StartSequence = "#" }
   ];
		}
	}
}
