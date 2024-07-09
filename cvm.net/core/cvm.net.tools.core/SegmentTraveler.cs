using LibCLCC.NET.TextProcessing;

namespace cvm.net.tools.core
{
	public class SegmentTraveler
	{
		public Segment HEAD;
		public Segment Current;
		public SegmentTraveler(Segment HEAD)
		{
			this.HEAD = HEAD;
			Current = HEAD;
		}
		public bool GoNext()
		{
			if (Current.Next == null)
			{
				return false;
			}
			if (Current.Next.content == "" && Current.Next.Next == null)
			{
				return false;
			}
			Current = Current.Next;
			return true;
		}
		public void Traverse(Action<Segment> action)
		{
			while (true)
			{
				action(Current);
				if (!GoNext())
				{
					return;
				}
			}
		}
	}
}
