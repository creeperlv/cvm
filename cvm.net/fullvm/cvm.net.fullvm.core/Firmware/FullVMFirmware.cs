using cvm.net.fullvm.core.Disk;
using cvm.net.fullvm.core.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvm.net.fullvm.core.Firmware
{
	public class FullVMFirmware
	{
		public readonly static Version version = new Version(1, 0, 0, 0);
		public List<DiskImage> imgs = [];
		void Loop()
		{
			int LW = 0;
			int LH = 0;
			while (true)
			{

				var w = Terminal.Width;
				var h = Terminal.Height;
				if (LW != w || LH != h|| Operated)
				{
					Draw(w, h);
					LW = w;
					LH = h;
					Operated=false;
				}
				if (Terminal.IsKeyAvailable())
				{
					Event();
					Operated=true;
				}
				//Thread.Sleep(16);
			}
		}
		private void DrawHeader(int w, string Title)
		{
			var l = Title.Length;
			Terminal.Write("\x1b[104m");
			Terminal.Write("\x1b[97m");
			if (l > w)
			{
				Terminal.Write(Title[..w]);
				Terminal.Write("\x1b[0m");
			}
			else
			{
				Terminal.Write(Title);
				for (int i = 0; i < w - l; i++)
				{
					Terminal.Write(' ');
				}
				Terminal.Write("\x1b[0m");

			}
		}
		int a = 0;
		private void Event()
		{
			switch (CurrentScreen)
			{
				case 0:
					{
						var k = Terminal.ReadKey(true);
						if (k.Key == ConsoleKey.Delete)
						{
							CurrentScreen = 2;
						}
					}
					break;
				case 2:
					{
						var k = Terminal.ReadKey(true);
						if (k.Key == ConsoleKey.LeftArrow)
						{
							a--;
							a = a % 2;
						}
						else if (k.Key == ConsoleKey.RightArrow)
						{
							a++;
							a = a % 2;
						}
					}
					break;
				default:
					break;
			}
		}
		int CurrentScreen = 0;
		bool Operated=true;
		private void Draw(int w, int h)
		{
			Terminal.Clear();
			switch (CurrentScreen)
			{
				case 0:
					{

						DrawHeader(w, "CVM Firmware v." + version);
						if (imgs.Count == 0)
						{
							h -= 2;
							for (int i = 0; i < h / 2; i++)
							{
								Terminal.Write('\n');
							}
							var lbl = "No Disk Loaded.";
							var _w = w - lbl.Length;
							if (_w > 0)
							{
								for (int i = 0; i < _w / 2; i++)
								{
									Terminal.Write(' ');
								}
								Terminal.WriteLine(lbl);
							}
							else
							{
								Terminal.WriteLine(lbl[(-_w)..(lbl.Length + _w)]);

							}
							for (int i = 0; i < h / 2 - 1; i++)
							{
								Terminal.Write('\n');
							}
						}
					}
					break;
				case 1:
					{

					}
					break;
				case 2:
					{
						DrawHeader(w, "CVM Firmware v." + version);
						h -= 2;
						for (int i = 0; i < h / 2; i++)
						{
							Terminal.Write('\n');
						}
						var lbl = "No Options Available.";
						if (a == 1)
						{
							lbl="There will be.";
						}
						var _w = w - lbl.Length;
						if (_w > 0)
						{
							for (int i = 0; i < _w / 2; i++)
							{
								Terminal.Write(' ');
							}
							Terminal.WriteLine(lbl);
						}
						else
						{
							Terminal.WriteLine(lbl[(-_w)..(lbl.Length + _w)]);

						}
					}
					break;
				default:
					break;
			}

		}

		public void Init(bool IsGraphicsMode)
		{
			if (!IsGraphicsMode)
			{
				Loop();
			}
		}
	}
}
