namespace cvm.net.fullvm.core.Output
{
	public static class Terminal
	{
		public static ITerminal? currentUsingTerminal;
		public static void Write(char c)
		{
			currentUsingTerminal?.Write(c);
		}
		public static char Read()
		{
			return currentUsingTerminal?.Read() ?? '\0';
		}
	}
	public class ConsoleBasedVT : ITerminal
	{
		Queue<char> chars_ToWrite = new Queue<char>();
		public char Read()
		{
			if (chars_ToWrite.Count == 0)
			{
				var info = Console.ReadKey();
				bool isNormalInput = false;
				switch (info.Key)
				{
					case ConsoleKey.LeftArrow:
					case ConsoleKey.UpArrow:
					case ConsoleKey.RightArrow:
					case ConsoleKey.DownArrow:
						{

							chars_ToWrite.Enqueue('\x1b');
							chars_ToWrite.Enqueue('[');
							switch (info.Key)
							{
								case ConsoleKey.LeftArrow:
									chars_ToWrite.Enqueue('B');
									break;
								case ConsoleKey.UpArrow:
									chars_ToWrite.Enqueue('A');
									break;
								case ConsoleKey.RightArrow:
									chars_ToWrite.Enqueue('C');
									break;
								case ConsoleKey.DownArrow:
									chars_ToWrite.Enqueue('D');
									break;
							}
						}
						break;
					case ConsoleKey.Clear:
					case ConsoleKey.Pause:
					case ConsoleKey.Escape:
					case ConsoleKey.PageUp:
					case ConsoleKey.PageDown:
					case ConsoleKey.End:
					case ConsoleKey.Home:
					case ConsoleKey.Select:
					case ConsoleKey.Print:
					case ConsoleKey.Execute:
					case ConsoleKey.PrintScreen:
					case ConsoleKey.Insert:
					case ConsoleKey.Delete:
					case ConsoleKey.Help:
					case ConsoleKey.LeftWindows:
					case ConsoleKey.RightWindows:
					case ConsoleKey.Applications:
					case ConsoleKey.Sleep:
					case ConsoleKey.F1:
					case ConsoleKey.F2:
					case ConsoleKey.F3:
					case ConsoleKey.F4:
					case ConsoleKey.F5:
					case ConsoleKey.F6:
					case ConsoleKey.F7:
					case ConsoleKey.F8:
					case ConsoleKey.F9:
					case ConsoleKey.F10:
					case ConsoleKey.F11:
					case ConsoleKey.F12:
					case ConsoleKey.F13:
					case ConsoleKey.F14:
					case ConsoleKey.F15:
					case ConsoleKey.F16:
					case ConsoleKey.F17:
					case ConsoleKey.F18:
					case ConsoleKey.F19:
					case ConsoleKey.F20:
					case ConsoleKey.F21:
					case ConsoleKey.F22:
					case ConsoleKey.F23:
					case ConsoleKey.F24:
					case ConsoleKey.BrowserBack:
					case ConsoleKey.BrowserForward:
					case ConsoleKey.BrowserRefresh:
					case ConsoleKey.BrowserStop:
					case ConsoleKey.BrowserSearch:
					case ConsoleKey.BrowserFavorites:
					case ConsoleKey.BrowserHome:
					case ConsoleKey.VolumeMute:
					case ConsoleKey.VolumeDown:
					case ConsoleKey.VolumeUp:
					case ConsoleKey.MediaNext:
					case ConsoleKey.MediaPrevious:
					case ConsoleKey.MediaStop:
					case ConsoleKey.MediaPlay:
					case ConsoleKey.LaunchMail:
					case ConsoleKey.LaunchMediaSelect:
					case ConsoleKey.LaunchApp1:
					case ConsoleKey.LaunchApp2:
					case ConsoleKey.Oem1:
					case ConsoleKey.OemPlus:
					case ConsoleKey.OemComma:
					case ConsoleKey.OemMinus:
					case ConsoleKey.OemPeriod:
					case ConsoleKey.Oem2:
					case ConsoleKey.Oem3:
					case ConsoleKey.Oem4:
					case ConsoleKey.Oem5:
					case ConsoleKey.Oem6:
					case ConsoleKey.Oem7:
					case ConsoleKey.Oem8:
					case ConsoleKey.Oem102:
					case ConsoleKey.Process:
					case ConsoleKey.Packet:
					case ConsoleKey.Attention:
					case ConsoleKey.CrSel:
					case ConsoleKey.ExSel:
					case ConsoleKey.EraseEndOfFile:
					case ConsoleKey.Play:
					case ConsoleKey.Zoom:
					case ConsoleKey.NoName:
					case ConsoleKey.Pa1:
					case ConsoleKey.OemClear:
						chars_ToWrite.Enqueue('\x1b');
						chars_ToWrite.Enqueue('[');
						int k = (int)info.Key;
						if (k < 10)
							chars_ToWrite.Enqueue((char)('0' + k));
						else
						{
							chars_ToWrite.Enqueue((char)('0' + k / 10));
							chars_ToWrite.Enqueue((char)('0' + k % 10));
						}
						break;
					default:
						isNormalInput = true;
						break;
				}
				if (info.Modifiers != ConsoleModifiers.None)
				{
					if (isNormalInput)
					{
						chars_ToWrite.Enqueue('\x1b');
						chars_ToWrite.Enqueue('[');
						int k = (int)info.Key;
						if (k < 10)
							chars_ToWrite.Enqueue((char)('0' + k));
						else
						{
							chars_ToWrite.Enqueue((char)('0' + k / 10));
							chars_ToWrite.Enqueue((char)('0' + k % 10));
						}
					}
					chars_ToWrite.Enqueue('\x1b');
					chars_ToWrite.Enqueue(';');
					chars_ToWrite.Enqueue((char)('0' + (int)info.Modifiers));
					chars_ToWrite.Enqueue('~');
				}
				else
				{
					if (isNormalInput)
					{
						chars_ToWrite.Enqueue(info.KeyChar);
					}
				}
			}
			return chars_ToWrite.Dequeue();
		}

		public void Write(char c)
		{
			Console.Write(c);
		}
	}

	public interface ITerminal
	{
		void Write(char c);
		char Read();
	}
}
