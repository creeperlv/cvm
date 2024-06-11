namespace cvm.net.fullvm.core.Output
{
	public static class Terminal
	{
		public static ITerminal? currentUsingTerminal;
		public static int Width
		{
			get => currentUsingTerminal?.Width ?? 0; set
			{
				if (currentUsingTerminal is not null)
					currentUsingTerminal.Width = value;
			}
		}
		public static int Height
		{
			get => currentUsingTerminal?.Height ?? 0;
			set
			{
				if (currentUsingTerminal is not null) currentUsingTerminal.Height = value;
			}
		}
		public static bool IsKeyAvailable() => currentUsingTerminal?.IsKeyAvailable() ?? false;
		public static void Clear() => currentUsingTerminal?.Clear();
		public static void SetPosition(int x, int y)
		{
			currentUsingTerminal?.SetCursorPosition(x, y);
		}
		public static (int, int) GetPosition()
		{
			return currentUsingTerminal?.GetPosition() ?? (0, 0);
		}
		public static void Write(char c)
		{
			currentUsingTerminal?.Write(c);
		}
		public static void Write(string str)
		{
			if (currentUsingTerminal is null) return;
			foreach (var item in str)
			{
				currentUsingTerminal.Write(item);
			}
		}
		public static void WriteLine(string str)
		{
			if (currentUsingTerminal is null) return;
			foreach (var item in str)
			{
				currentUsingTerminal.Write(item);
			}
			currentUsingTerminal.Write('\n');
		}
		public static int Read()
		{
			return currentUsingTerminal?.Read() ?? '\0';
		}
		public static ConsoleKeyInfo ReadKey(bool Intercept=true)
		{
			return currentUsingTerminal?.ReadKey(Intercept) ?? default;
		}
		public static string? ReadLine()
		{
			return currentUsingTerminal?.ReadLine();
		}

	}
}
