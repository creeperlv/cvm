using cvm.net.core;

namespace cvm.net.compiler.core
{
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
				{"func", InstID.FUNC },
				{"syscall", InstID.FUNC },
				{"malloc", InstID.MALLOC },
				{"realloc", InstID.REALLOC},
				{"free", InstID.FREE},
				{"exit", InstID.EXIT},
			}
		};
		public Dictionary<string, uint> Names = new()
		{

		};
	}
}
