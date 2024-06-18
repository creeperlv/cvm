﻿using cvm.net.core;
using System.Text.Json.Serialization;

namespace cvm.net.compiler.core
{
	[Serializable]
	public class CVMObject
	{
		public Version ObjectVersion { get; set; } = Constants.CurrentCVMObjectVersion;
		public Dictionary<string, int> Labels { get; set; } = [];
		public List<string> UndefinedLabels { get; set; } = [];
		public Dictionary<string, string> Consts { get; set; } = [];
		public Dictionary<int, string> Data { get; set; } = [];
		public List<Instruction> instructions { get; set; } = [];
	}
}
