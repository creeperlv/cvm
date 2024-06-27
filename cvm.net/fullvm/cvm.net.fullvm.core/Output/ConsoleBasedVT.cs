namespace cvm.net.fullvm.core.Output
{
	public class ConsoleBasedVT : ITerminal
	{
		public int Width { get => Console.WindowWidth; set => Console.WindowWidth = value; }
		public int Height { get => Console.WindowHeight; set => Console.WindowHeight = value; }
		public ConsoleKeyInfo ReadKey(bool Intercept)
		{
			return Console.ReadKey(Intercept);
		}
		public (int, int) GetPosition() => Console.GetCursorPosition();
		public bool IsKeyAvailable() => Console.KeyAvailable;
		public int Read()
		{
			return Console.Read();
		}
		public string? ReadLine()
		{
			return Console.ReadLine();
		}
		public void Write(char c)
		{
			Console.Write(c);
		}
		public void Clear()
		{
			Console.Clear();
		}

		public void SetCursorPosition(int x, int y)
		{
			Console.SetCursorPosition(x, y);
		}
	}
}
