namespace cvm.net.disk.editor
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string? file = null;
			if (args.Length > 0)
			{
				file = args[0];
			}
			string? script = null;
			if (args.Length == 2)
			{
				script = args[1];
			}
			DiskEditor.StartEdit(file, script);
		}
	}
}
