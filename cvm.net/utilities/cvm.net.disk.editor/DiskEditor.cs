using cvm.net.core.libc;
using cvm.net.fullvm.core.Disk;
using cvm.net.tools.core;
using LibCLCC.NET.TextProcessing;
using System.Text;

namespace cvm.net.disk.editor
{
	public static class DiskEditor
	{
		static DiskImage? currentEditingImage;
		static string? diskFile;
		static GPTPartMgr? GPTMgr;
		static Dictionary<string,Action<Segment, SegmentTraveler>> Commands = new (){
			{"exit",(a,b)=>Exit()} };
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
		static void CloseCurrentOpenDisk()
		{
			if (currentEditingImage != null)
			{
				currentEditingImage.Dispose();
				currentEditingImage = null;
			}
			GPTMgr = null;
		}
		static unsafe void Execute(string Line, string scriptName = "runtime")
		{
			if (Line == "") return;
			var head = scanner.Scan(Line, false, scriptName);
			SegmentTraveler t = new SegmentTraveler(head);
			switch (head.content)
			{
				case "load-disk":
					{
						LoadDisk();
					}
					break;
				case "create-disk":
				case "new-disk":
					{
						CreateDisk(t);
					}
					break;
				case "chdisk":
					{
						ChangeDisk(t);
					}
					break;
				case "create-gpt":
				case "new-gpt":
					{
						CreateGPT();
					}
					break;
				case "load-gpt":
					{
						LoadGPT();
					}
					break;
				case "save-gpt":
					{
						SaveGPT();
					}
					break;
				case "new-part":
					{
						NewPartition(t);
					}
					break;
				case "list-part":
					{
						ListPart();
					}
					break;

				case "exit":
					Exit();
					break;
				default:
					Console.WriteLine($"Unknown command:{head.content}");
					break;
			}
		}

		private static unsafe void Exit()
		{
			Environment.Exit(0);
		}

		private static unsafe void SaveGPT()
		{
			if (currentEditingImage == null)
			{
				Console.WriteLine("You must open/create a disk first!");
				return;
			}
			if (GPTMgr is null)
			{
				Console.WriteLine("You must load/create GPT table first!");
				return;
			}
			GPTMgr.WriteTableMeta();
		}

		private static unsafe void LoadGPT()
		{
			if (currentEditingImage == null)
			{
				Console.WriteLine("You must open/create a disk first!");
				return;
			}
			GPTMgr = new GPTPartMgr(currentEditingImage);
			GPTMgr.Load();
		}

		private static unsafe void CreateGPT()
		{
			if (currentEditingImage == null)
			{
				Console.WriteLine("You must open/create a disk first!");
				return;
			}
			GPTMgr = new GPTPartMgr(currentEditingImage);
			GPTMgr.header.CurrentLBA = 1;
			GPTMgr.header.HEADER = DiskDefinitions.GPTHeader;
			GPTMgr.header.DiskID = Guid.NewGuid();
		}

		private static unsafe void ChangeDisk(SegmentTraveler t)
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
			CloseCurrentOpenDisk();
		}

		private static unsafe void CreateDisk(SegmentTraveler t)
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
				CloseCurrentOpenDisk();
				currentEditingImage = new DiskImage()
				{
					FD = File.Open(diskFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)
				};

				DiskImage.CreateNewImage(currentEditingImage.FD, new DiskMeta() { ImgVersion = DiskDefinitions.CurrentCVMIMGVersion, LBABlockCount = Sectors });
				currentEditingImage.DataOffset = currentEditingImage.FD.Position;
			}
		}

		private static unsafe void ListPart()
		{
			if (GPTMgr is null)
			{
				Console.WriteLine("You must load/create GPT table first!");
				return;
			}
			foreach (var item in GPTMgr.Parts)
			{
				string name;
				fixed (byte* ptr = item.metadata.Name)
				{
					name = ((IntPtr)ptr).CStr2DotNetStr();

				}
				Console.WriteLine($"Disk:{name}");
				Console.WriteLine($"\tID:{item.metadata.PartID}");
				Console.WriteLine($"\tType:{item.metadata.PartType}");
				Console.WriteLine($"\tStart Sector:{item.metadata.FirstLBA}");
				Console.WriteLine($"\tEnd Sector:{item.metadata.LastLBA}");
			}
		}

		private static unsafe void LoadDisk()
		{
			if (diskFile == null)
			{
				Console.WriteLine("You must specify a file name first!");
				return;
			}
			CloseCurrentOpenDisk();
			currentEditingImage = new DiskImage()
			{
				FD = File.Open(diskFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
			};
			currentEditingImage.LoadImageInfo();
		}

		private unsafe static void NewPartition(SegmentTraveler t)
		{
			if (currentEditingImage == null)
			{
				Console.WriteLine("You must open/create a disk first!");
				return;
			}
			if (GPTMgr is null)
			{
				Console.WriteLine("You must load/create GPT table first!");
				return;
			}
			PartitionMetadata* metadata = (PartitionMetadata*)StdLib.malloc(sizeof(PartitionMetadata));
			metadata->PartID = Guid.NewGuid();
			bool IsFirstLBASet = false;
			bool IsLastLBASet = false;
			bool IsPartTypeSet = false;
			string? Name = null;
			t.Traverse((s) =>
			{
				switch (s.content)
				{
					case "--first-lba":
						if (!t.GoNext())
						{
							return;
						}
						if (long.TryParse(t.Current.content, out metadata->FirstLBA))
						{
							IsFirstLBASet = true;
						}
						break;
					case "--last-lba":
						if (!t.GoNext())
						{
							return;
						}
						if (long.TryParse(t.Current.content, out metadata->LastLBA))
						{
							IsLastLBASet = true;
						}
						break;

					case "--name":
						if (!t.GoNext())
						{
							return;
						}
						Name = t.Current.content;
						break;
					case "--type":
						if (!t.GoNext())
						{
							return;
						}
						var tName = t.Current.content;
						{
							if (DiskDefinitions.PartTypeNames.TryGetValue(tName, out var type))
							{
								if (DiskDefinitions.PartitionTypeIDs.TryGetValue(type, out metadata->PartType))
								{
									IsPartTypeSet = true;
								}
							}
						}
						break;
					default:
						break;
				}
			});
			if (!IsFirstLBASet)
			{
				RequestValue($"Please specify the start sector: (1 Sector = {DiskDefinitions.LBASectorSize} Bits)",
					(s) =>
					{
						if (long.TryParse(s, out metadata->FirstLBA))
						{
							IsFirstLBASet = true;
							return true;
						}
						return false;
					});
			}
			if (!IsLastLBASet)
			{
				RequestValue($"Please specify the end sector: (1 Sector = {DiskDefinitions.LBASectorSize} Bits)",
					(s) =>
					{
						if (long.TryParse(s, out metadata->LastLBA))
						{
							IsLastLBASet = true;
							return true;
						}
						return false;
					});
			}
			if (!IsPartTypeSet)
			{
				RequestValue($"Please specify the a partition type:",
					(s) =>
					{
						if (s == null) return false;
						if (DiskDefinitions.PartTypeNames.TryGetValue(s.ToUpper(), out var type))
						{
							if (DiskDefinitions.PartitionTypeIDs.TryGetValue(type, out metadata->PartType))
							{
								IsPartTypeSet = true;
								return true;
							}
						}
						return false;
					});
			}
			if (Name == null)
			{
				RequestValue($"Please specify the a name for the partition:",
					(s) =>
					{
						Name = s;
						return Name != null;
					});
			}
			metadata->PartID = Guid.NewGuid();
			{
				var nb = Encoding.ASCII.GetBytes(Name ?? "New Partition");
				fixed (byte* src = nb)
				{
					Buffer.MemoryCopy(src, metadata->Name, DiskDefinitions.GPTPartitionNameMaxLen, nb.Length);
				}
				metadata->Name[nb.Length] = 0;
			}
			GPTMgr.Parts.Add(new DiskPart(currentEditingImage, GPTMgr) { metadata = metadata[0] });
			StdLib.free(metadata);
		}

		static void InteractiveEdit()
		{
			while (true)
			{
				Console.Write("Disk:" + (diskFile ?? "not-specified-disk"));
				Console.Write("# ");
				Execute(Console.ReadLine() ?? "");
			}
		}
	}
}
