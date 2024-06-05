namespace cvm.net.fullvm.core
{
	public class SimpleFS
	{
		DiskPart part;

		public SimpleFS(DiskPart part)
		{
			this.part = part;
			Load();
		}
		public SFSNode? rootNode;
		public Dictionary<ulong, SFSItem> items = [];
		public Dictionary<ulong, SFSNode> nodes = [];
		private unsafe void Load()
		{
			int offset = 0;
			var Data = part.ReadLBA(offset + 0);
			offset++;
			int ItemCount = Data.As<LBABlock, int>(0);
			int NodeCount = Data.As<LBABlock, int>(0);
			for (int i = 0; i < ItemCount; i++)
			{
				Data = part.ReadLBA(offset);
				SFSItem item = Data.As<LBABlock, SFSItem>(0);
				items.Add(item.ID, item);
				offset++;
			}
			for (int i = 0; i < NodeCount; i++)
			{
				Data = part.ReadLBA(offset);
				SFSNodeBlock item = Data.As<LBABlock, SFSNodeBlock>(0);
				SFSNode node;
				if (nodes.ContainsKey(item.NodeID))
				{
					node = nodes[item.NodeID];
				}
				else
				{
					node = new SFSNode();
					nodes.Add(item.NodeID, node);
				}
				for (int __i = 0; __i < item.NodeCount; __i++)
				{
					node.Children.Add(item.Children[i]);
				}
				offset++;
			}

		}
	}
}
