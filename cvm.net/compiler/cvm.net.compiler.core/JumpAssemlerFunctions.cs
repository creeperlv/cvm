using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.assembler.core
{
	public static unsafe class JumpAssemlerFunctions
	{
		public unsafe static bool Assemble_LRCalc(ushort instID, Segment s, OperationResult<CVMObject> result, IntPtr InstPtr, int PC)
		{
			if (instID != InstID.JMP)
			{
				return false;
			}
			SegmentTraveler st = new(s);
			if (!st.GoNext())
			{
				result.AddError(new IncompletInstructionError(st.Current));
				return false;
			}

			Instruction instruction = default;
			instruction.Set(InstID.JMP, 0);
			var ValueSeg = st.Current;

			if (DataConversion.TryParseRegister(ValueSeg.content, result, out var _T))
			{
				instruction.Set(1, 2);
				instruction.Set(_T, 3);

			}
			else
			{
				instruction.Set(0, 2);
				if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
				{
					if (lbl >= 0)
					{
						instruction.Set(lbl, 3);
						((Instruction*)InstPtr)[0] = instruction;
						return true;
					}
				}
				{
					if (!result.Result.UndefinedLabels.Contains(ValueSeg.content))
					{
						result.Result.UndefinedLabels.Add(ValueSeg.content);
					}
					instruction.Set(result.Result.UndefinedLabels.IndexOf(ValueSeg.content), 3);
					instruction.Set(IntermediateInstructions.JMP_LBL, 0);
				}
			}
			((Instruction*)InstPtr)[0] = instruction;
			return true;
		}

	}
}
