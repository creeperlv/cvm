using cvm.net.core;

namespace cvm.net.compiler.core
{
	public enum ASMSections
	{
		Data, Code, Consts
	}
	public static class TypeNames
	{
		public readonly static string Register = "Register";
	}
	public class ISADefinition
	{
		public static ISADefinition CurrentDefinition = new ISADefinition()
		{
			Names = new()
			{
				{"add", InstID.ADD },
				{"sub", InstID.SUB },
				{"mul", InstID.MUL },
				{"div", InstID.DIV },
				{"lrcalc", InstID.LR_CALC},
				{"scalc", InstID.SELF_CALC},
				{"ld", InstID.LD},
				{"loaddata", InstID.LD},
				{"sd", InstID.SD},
				{"savedata", InstID.SD},
				{"set", InstID.SET},
				{"cvt", InstID.CVT},
				{"convert", InstID.CVT},
				{"comp", InstID.COMP},
				{"compare", InstID.COMP},
				{"sh", InstID.SH},
				{"shift", InstID.SH},
				{"lg", InstID.LG},
				{"logic", InstID.LG},
				{"jmp", InstID.JMP},
				{"jump", InstID.JMP},
				{"jf", InstID.JF},
				{"jumpflag", InstID.JF},
				{"jo", InstID.JO},
				{"jumpoverflow", InstID.JO},
				{"jumpof", InstID.JO},
				{"pjmp", InstID.PJMP},
				{"parallel_jump", InstID.PJMP},
				{"pjump", InstID.PJMP},
				{"rf", InstID.RF},
				{"reset_flag", InstID.RF},
				{"reset_overflow", InstID.RO},
				{"rint", InstID.RINT},
				{"register_int", InstID.RINT},
				{"register_interrupt", InstID.RINT},
				{"set_interrupt", InstID.SINT},
				{"set_int", InstID.SINT},
				{"sint", InstID.SINT},
				{"call", InstID.CALL },
				{"func", InstID.INT },
				{"syscall", InstID.INT },
				{"malloc", InstID.MALLOC },
				{"realloc", InstID.REALLOC},
				{"free", InstID.FREE},
				{"exit", InstID.EXIT},
			},
			Sections = new() {
				{"Data", ASMSections.Data},
				{"Code", ASMSections.Code},
				{"Consts", ASMSections.Consts},
				},
			Types = new() {
				{"byte",BaseDataType.BU },
				{"sbyte",BaseDataType.BS},
				{"int16",BaseDataType.S},
				{"short",BaseDataType.S},
				{"uint16",BaseDataType.SU},
				{"ushort",BaseDataType.SU},
				{"int64",BaseDataType.L},
				{"long",BaseDataType.L},
				{"uint64",BaseDataType.LU},
				{"ulong",BaseDataType.LU},
				{"float",BaseDataType.F},
				{"single",BaseDataType.F},
				{"double",BaseDataType.D},
				{"int",BaseDataType.I},
				{"int32",BaseDataType.I},
				{"uint",BaseDataType.IU},
				{"uint32",BaseDataType.IU},
			},
		};
		unsafe static ISADefinition()
		{
			var RegisterCount = ExecuteContext.RegisterLimit / sizeof(MemoryPtr);
			for (int i = 0; i < RegisterCount; i++)
			{
				CurrentDefinition.RegisterNames.Add($"x{i}", (byte)(i * sizeof(MemoryPtr)));
			}
			Dictionary<string, string> regNameAlias = new()
			{
				{"ra","x1" },
				{"sp","x2" },
				{"gp","x3" },
				{"t0","x5" },
				{"t1","x6" },
				{"t2","x7" },
				{"a0","x10" },
				{"a1","x11" },
				{"a2","x12" },
				{"a3","x13" },
				{"a4","x14" },
				{"a5","x15" },
				{"a6","x16" },
				{"a7","x17" },
			};
			foreach (var item in regNameAlias)
			{
				if (CurrentDefinition.RegisterNames.TryGetValue(item.Value, out var RegisterDefinition))
				{
					CurrentDefinition.RegisterNames.Add(item.Key, RegisterDefinition);
				}
			}
		}
		public Dictionary<string, byte> Types = new Dictionary<string, byte>()
		{

		};
		public Dictionary<string, ASMSections> Sections = [];
		public Dictionary<string, ushort> Names = new ()
		{

		};
	public Dictionary<string, byte> RegisterNames = [];
}
}
