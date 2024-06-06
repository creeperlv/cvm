using cvm.net.fullvm.core.Disk;
using LibCLCC.NET.TextProcessing;
using System.Runtime.CompilerServices;

namespace cvm.net.disk.editor
{
	internal class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Please specify a file to edit!");
				return;
			}
			var file = args[0];
			string? script = null;
			if (args.Length == 2)
			{
				script = args[1];
			}
			DiskEditor.StartEdit(file, script);
		}
	}
	public class SegmentTravler
	{
		public Segment HEAD;
		public Segment Current;
		public SegmentTravler(Segment HEAD)
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
	public static class DiskEditor
	{
		static DiskImage? currentEditingImage;
		static string? diskFile;
		public static void StartEdit(string? file, string? scriptfile)
		{
			diskFile = file;
			if (diskFile != null)
			{
				if (File.Exists(diskFile))
				{
					currentEditingImage = new DiskImage()
					{
						FD = File.Open(diskFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
					};
				}
			}
			if (scriptfile != null)
			{
				ExecuteScript(scriptfile);
			}
			else
			{
				InteractiveEdit();
			}
		}
		static void ExecuteScript(string script)
		{
			using var stream = File.OpenRead(script);
			using var reader = new StreamReader(stream);
			while (true)
			{
				var line = reader.ReadLine();
				if (line == null) return;
				Execute(line, script);
			}
		}
		static void RequestValue(string Prompt, Func<string?, bool> func)
		{
			while (true)
			{
				Console.WriteLine(Prompt);
				if (func(Console.ReadLine()))
				{
					return;
				}
			}
		}
		static CommandLineScanner scanner = new CommandLineScanner();
		static void Execute(string Line, string scriptName = "runtime")
		{
			if (Line == "") return;
			var head = scanner.Scan(Line, false, scriptName);
			SegmentTravler t = new SegmentTravler(head);
			switch (head.content)
			{
				case "write-disk-info":
					{
						t.GoNext();
						ulong Sectors = 0;
						bool IsSizeSet = false;
						t.Traverse((s) =>
						{
							switch (s.content)
							{
								case "--sectors":
									if (!t.GoNext())
									{
										return;
									}
									if (ulong.TryParse(t.Current.content, out Sectors))
									{
										IsSizeSet = true;
									}
									break;
								case "--size":
									if (!t.GoNext())
									{
										return;
									}
									if (ulong.TryParse(t.Current.content, out Sectors))
									{
										var remainder = Sectors % DiskDefinitions.LBASectorSize;
										Sectors /= DiskDefinitions.LBASectorSize;
										Sectors += (remainder == 0 ? 0ul : 1ul);
										IsSizeSet = true;
									}
									break;
								default:
									break;
							}
						});
						if (!IsSizeSet)
						{
							RequestValue($"Please specify the size of the disk in sector count: (1 Sector = {DiskDefinitions.LBASectorSize} Bits)",
								(s) =>
								{
									if (ulong.TryParse(s, out Sectors))
									{
										IsSizeSet = true;
										return true;
									}
									return false;
								});
						}
						if (diskFile is not null)
						{
							if (currentEditingImage is not null)
							{
								currentEditingImage.Dispose();
							}
							currentEditingImage = new DiskImage()
							{
								FD = File.Open(diskFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
							};

							DiskImage.CreateNewImage(currentEditingImage.FD, new DiskMeta() { ImgVersion = DiskDefinitions.CurrentCVMIMGVersion, LBABlockCount = Sectors });
						}
					}
					break;
				case "chdisk":
					{
						if (t.GoNext())
						{
							diskFile = t.Current.content;
						}
						else
						{
							RequestValue($"Please specify the disk image file:",
								(s) =>
								{
									if (s is not null)
									{
										diskFile = s;
										return true;
									}
									return false;
								});

						}
						if (currentEditingImage is not null)
						{
							currentEditingImage.Dispose();
							currentEditingImage = null;
						}
					}
					break;
				case "create-gpt":
					{
						if (currentEditingImage == null)
						{
							Console.WriteLine("You must open/create a disk first!");
							return;
						}
						GPTPartMgr gPTPartMgr = new GPTPartMgr(currentEditingImage);

					}
					break;
				default:
					break;
			}
		}
		static void InteractiveEdit()
		{
			while (true)
			{
				Console.Write("Disk:" + (diskFile ?? "not-specified-disk"));
				Console.Write(" #");
				Execute(Console.ReadLine() ?? "");
			}
		}
	}
}
