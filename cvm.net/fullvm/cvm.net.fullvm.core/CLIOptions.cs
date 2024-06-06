namespace cvm.net.fullvm.core
{
	public class CLIOptions
	{
		public Operation Operation = Operation.Help;
		public Dictionary<string, string> arguments = [];
		public Dictionary<string, List<string>> ListArguments = [];
		public void Append(string Key, string Value)
		{
			if (!ListArguments.ContainsKey(Key))
			{
				ListArguments.Add(Key, []);
			}
			ListArguments[Key].Add(Value);
		}
		public bool TryGet(string key, out string? value)
		{
			return arguments.TryGetValue(key, out value);
		}
		public List<string> GetList(string Key)
		{
			if (ListArguments.TryGetValue(Key, out var value))
			{
				return value;
			}
			return new List<string>();
		}
		public void Set(string K, string V)
		{
			if (!arguments.TryAdd(K, V))
				arguments[K] = V;
		}
	}
}
