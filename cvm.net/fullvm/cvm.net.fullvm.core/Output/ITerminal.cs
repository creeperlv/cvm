namespace cvm.net.fullvm.core.Output
{
	public interface ITerminal
	{
		int Width { get; set; }
		int Height { get; set; }
		(int, int) GetPosition();
		void SetCursorPosition(int x, int y);
		void Write(char c);
		int Read();
		void Clear();
		bool IsKeyAvailable();
		ConsoleKeyInfo ReadKey(bool Intercept);
		string? ReadLine();
	}
}
