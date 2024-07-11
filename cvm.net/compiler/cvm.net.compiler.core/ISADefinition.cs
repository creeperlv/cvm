using cvm.net.assembler.core;
using cvm.net.compiler.core.DataProcessors;
using cvm.net.core;

namespace cvm.net.compiler.core
{
	public enum ASMSections
	{
		Data, Code, Definitions
	}
	public enum ASMDefinition
	{
		Const, Lib, Symbol
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
				{"intg", InstID.INTG},
				{"gsint", InstID.GSINT},
				{"grint", InstID.GRINT},
				{"adv0", InstID.ADV0},
				{"dump", InstID.DUMP},
				{"mcp", InstID.MCP},
				{"refs", InstID.REFS},
			},
			LROps = new()
			{
				{"mod",LRCalcOP.MOD },
				{"modulo ",LRCalcOP.MOD },
				{"pow",LRCalcOP.POW },
				{"min",LRCalcOP.MIN },
				{"max",LRCalcOP.MAX },
			},
			Sections = new() {
				{"data", ASMSections.Data},
				{"code", ASMSections.Code},
				{"definitions", ASMSections.Definitions},
				{"definition", ASMSections.Definitions},
				{"def", ASMSections.Definitions},
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
			SHOps = new()
			{
				{"left",0 },
				{"l",0 },
				{"r",1 },
				{"right",1 },
			},
			Definitions = new()
			{
				{"C", ASMDefinition.Const },
				{"Const", ASMDefinition.Const },
				{"L", ASMDefinition.Lib},
				{"Lib", ASMDefinition.Lib},
				{"S", ASMDefinition.Symbol },
				{"Sym", ASMDefinition.Symbol },
				{"Symbol", ASMDefinition.Symbol },
			},
			SCalcOps = new()
			{
				{"abs", SelfCalcOP.ABS },
				{"acos", SelfCalcOP.ACOS },
				{"acosh", SelfCalcOP.ACOSH},
				{"asin", SelfCalcOP.ASIN},
				{"asinh", SelfCalcOP.ASINH},
				{"atan", SelfCalcOP.ATAN},
				{"atan2", SelfCalcOP.ATAN2},
				{"atan2h", SelfCalcOP.ATAN2H},
				{"atanh", SelfCalcOP.ATANH},
				{"cbrt", SelfCalcOP.CBRT},
				{"cel", SelfCalcOP.CEL},
				{"ceil", SelfCalcOP.CEL},
				{"cos", SelfCalcOP.COS},
				{"cosh", SelfCalcOP.COSH},
				{"exp", SelfCalcOP.EXP},
				{"flr", SelfCalcOP.FLR},
				{"floor", SelfCalcOP.FLR},
				{"log", SelfCalcOP.LOG},
				{"log10", SelfCalcOP.LOG10},
				{"rnd", SelfCalcOP.RND},
				{"rand", SelfCalcOP.RND},
				{"random", SelfCalcOP.RND},
				{"sign", SelfCalcOP.SIGN},
				{"sin", SelfCalcOP.SIN},
				{"sinh", SelfCalcOP.SINH},
				{"sqrt", SelfCalcOP.SQRT},
				{"tan", SelfCalcOP.TAN},
				{"tanh", SelfCalcOP.TANH},
				{"tun", SelfCalcOP.TUN},
			},
			LogicOps = new() {
				{ "and", 0 },
				{ "or", 1 },
				{ "not", 2 },
				{ "xor", 3 },
				{ "nor", 4 },
			},
			JumpOps = new()
			{
				{"absolute",0},
				{"false",0},
				{"relative",1},
				{"true",1},
			},
			CompOps = new()
			{

			},
			Booleans = new()
			{
				{"true",1},
				{"on",1},
				{"false",0},
				{"off",0},
			},
			DataProcessMethods = new()
			{
				{"cstring", DataProcessMethod.CChar },
				{"cchar", DataProcessMethod.CChar },
				{"base64", DataProcessMethod.Base64 },
				{"extfile", DataProcessMethod.extfile},
				{"externalfile", DataProcessMethod.extfile},
				{"hex", DataProcessMethod.hex},
			},
			Processors = new()
			{
				{
					DataProcessMethod.CChar,new CStringProcessor()
				},
				{
					DataProcessMethod.Base64,new Base64Processor()
				}
			}
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
		public Dictionary<string, byte> LROps = new Dictionary<string, byte>()
		{

		};
		public Dictionary<string, ASMSections> Sections = [];
		public Dictionary<string, ASMDefinition> Definitions = [];
		public Dictionary<string, ushort> Names = new()
		{

		};
		public Dictionary<string, byte> RegisterNames = [];
		public Dictionary<string, byte> SHOps = [];
		public Dictionary<string, byte> LogicOps = [];
		public Dictionary<string, byte> SCalcOps = [];
		public Dictionary<string, byte> JumpOps = [];
		public Dictionary<string, byte> CompOps = [];
		public Dictionary<string, byte> Booleans = [];
		public Dictionary<string, DataProcessMethod> DataProcessMethods = [];
		public Dictionary<DataProcessMethod, IDataProcessor> Processors = [];
	}
}
