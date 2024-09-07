using cvm.net.compiler.core;
using cvm.net.compiler.core.Errors;
using cvm.net.core;
using cvm.net.tools.core;
using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;

namespace cvm.net.compiler.core.AssemblerFunctions
{
    public static unsafe class JumpAssemlerFunctions
    {
        public unsafe static bool Assemble_JMP(ushort instID, Segment s, OperationResult<CVMObject> result, nint InstPtr, int PC)
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
            var IsRelativeSeg = st.Current;
            if (!ISADefinition.CurrentDefinition.JumpOps.TryGetValue(IsRelativeSeg.content.ToLower(), out var jump))
            {
                result.AddError(new UnknownOperationError(InstructionNames.JMP, IsRelativeSeg));
                return false;
            }
            InstPtr.SetData(jump, 3);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var ValueSeg = st.Current;

            InstPtr.Set(InstID.JMP, 0);

            if (DataConversion.TryParseRegister(ValueSeg.content, result, out var _T))
            {
                InstPtr.Set(1, 2);
                InstPtr.Set(_T, 4);

            }
            else
            {
                InstPtr.Set(0, 2);
                if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
                {
                    if (lbl >= 0)
                    {
                        InstPtr.Set(lbl, 4);
                        return true;
                    }
                }
                {
                    if (!result.Result.UndefinedLabels.Contains(ValueSeg.content))
                    {
                        result.Result.UndefinedLabels.Add(ValueSeg.content);
                    }
                    InstPtr.Set(result.Result.UndefinedLabels.IndexOf(ValueSeg.content), 3);
                    InstPtr.Set(IntermediateInstructions.JMP_LBL, 0);
                }
            }
            return true;
        }
        public unsafe static bool Assemble_PJMP(ushort instID, Segment s, OperationResult<CVMObject> result, nint InstPtr, int PC)
        {
            if (instID != InstID.PJMP)
            {
                return false;
            }
            SegmentTraveler st = new(s);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var IsRelativeSeg = st.Current;
            if (!ISADefinition.CurrentDefinition.JumpOps.TryGetValue(IsRelativeSeg.content.ToLower(), out var jump))
            {
                result.AddError(new UnknownOperationError(InstructionNames.JMP, IsRelativeSeg));
                return false;
            }
            InstPtr.SetData(jump, 3);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var ValueSeg = st.Current;

            InstPtr.Set(InstID.PJMP, 0);

            if (DataConversion.TryParseRegister(ValueSeg.content, result, out var _T))
            {
                InstPtr.Set(1, 2);
                InstPtr.Set(_T, 4);

            }
            else
            {
                InstPtr.Set(0, 2);
                if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
                {
                    if (lbl >= 0)
                    {
                        InstPtr.Set(lbl, 4);
                        return true;
                    }
                }
                {
                    if (!result.Result.UndefinedLabels.Contains(ValueSeg.content))
                    {
                        result.Result.UndefinedLabels.Add(ValueSeg.content);
                    }
                    InstPtr.Set(result.Result.UndefinedLabels.IndexOf(ValueSeg.content), 3);
                    InstPtr.Set(IntermediateInstructions.PJMP_LBL, 0);
                }
            }
            return true;
        }

        public unsafe static bool Assemble_CALL(ushort instID, Segment s, OperationResult<CVMObject> result, nint InstPtr, int PC)
        {
            if (instID != InstID.CALL)
            {
                return false;
            }
            SegmentTraveler st = new(s);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var IsRelativeSeg = st.Current;
            if (!ISADefinition.CurrentDefinition.JumpOps.TryGetValue(IsRelativeSeg.content.ToLower(), out var jump))
            {
                result.AddError(new UnknownOperationError(InstructionNames.JMP, IsRelativeSeg));
                return false;
            }
            InstPtr.SetData(jump, 3);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var ValueSeg = st.Current;

            InstPtr.Set(InstID.CALL, 0);

            if (DataConversion.TryParseRegister(ValueSeg.content, result, out var _T))
            {
                InstPtr.Set(1, 2);
                InstPtr.Set(_T, 4);

            }
            else
            {
                InstPtr.Set(0, 2);
                if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
                {
                    if (lbl >= 0)
                    {
                        InstPtr.Set(lbl, 4);
                        return true;
                    }
                }
                {
                    if (!result.Result.UndefinedLabels.Contains(ValueSeg.content))
                    {
                        result.Result.UndefinedLabels.Add(ValueSeg.content);
                    }
                    InstPtr.Set(result.Result.UndefinedLabels.IndexOf(ValueSeg.content), 3);
                    InstPtr.Set(IntermediateInstructions.CALL_LBL, 0);
                }
            }
            return true;
        }

        public unsafe static bool Assemble_JF(ushort instID, Segment s, OperationResult<CVMObject> result, nint InstPtr, int PC)
        {
            if (instID != InstID.JF)
            {
                return false;
            }
            InstPtr.Set(InstID.JF, 0);
            SegmentTraveler st = new(s);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var FlagID = st.Current;

            if (!DataConversion.TryParse<byte>(FlagID.content, out var Flag))
            {
                return false;
            }
            InstPtr.SetData(Flag, 3);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }

            var IsRelativeSeg = st.Current;

            if (!ISADefinition.CurrentDefinition.JumpOps.TryGetValue(IsRelativeSeg.content.ToLower(), out var jump))
            {
                result.AddError(new UnknownOperationError(InstructionNames.JF, IsRelativeSeg));
                return false;
            }
            InstPtr.SetData(jump, 4);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }

            var ValueSeg = st.Current;

            if (DataConversion.TryParseRegister(ValueSeg.content, result, out var _T))
            {
                InstPtr.Set(1, 2);
                InstPtr.Set(_T, 5);

            }
            else
            {
                InstPtr.Set(0, 2);
                if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
                {
                    if (lbl >= 0)
                    {
                        InstPtr.Set(lbl, 5);
                        return true;
                    }
                }
                {
                    if (!result.Result.UndefinedLabels.Contains(ValueSeg.content))
                    {
                        result.Result.UndefinedLabels.Add(ValueSeg.content);
                    }
                    InstPtr.Set(result.Result.UndefinedLabels.IndexOf(ValueSeg.content), 3);
                    InstPtr.Set(IntermediateInstructions.JF_LBL, 0);
                }
            }
            return true;
        }

        public unsafe static bool Assemble_JO(ushort instID, Segment s, OperationResult<CVMObject> result, nint InstPtr, int PC)
        {
            if (instID != InstID.JO)
            {
                return false;
            }
            InstPtr.Set(InstID.JO, 0);
            SegmentTraveler st = new(s);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }
            var FlagID = st.Current;

            if (!ISADefinition.CurrentDefinition.Booleans.TryGetValue(FlagID.content.ToLower(), out var _IsOn))
            {
                return false;
            }
            InstPtr.SetData(_IsOn, 3);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }

            var IsRelativeSeg = st.Current;

            if (!ISADefinition.CurrentDefinition.JumpOps.TryGetValue(IsRelativeSeg.content.ToLower(), out var jump))
            {
                result.AddError(new UnknownOperationError(InstructionNames.JF, IsRelativeSeg));
                return false;
            }
            InstPtr.SetData(jump, 4);
            if (!st.GoNext())
            {
                result.AddError(new IncompletInstructionError(st.Current));
                return false;
            }

            var ValueSeg = st.Current;

            if (DataConversion.TryParseRegister(ValueSeg.content, result, out var _T))
            {
                InstPtr.Set(1, 2);
                InstPtr.Set(_T, 5);

            }
            else
            {
                InstPtr.Set(0, 2);
                if (result.Result.Labels.TryGetValue(ValueSeg.content, out var lbl))
                {
                    if (lbl >= 0)
                    {
                        InstPtr.Set(lbl, 5);
                        return true;
                    }
                }
                {
                    if (!result.Result.UndefinedLabels.Contains(ValueSeg.content))
                    {
                        result.Result.UndefinedLabels.Add(ValueSeg.content);
                    }
                    InstPtr.Set(result.Result.UndefinedLabels.IndexOf(ValueSeg.content), 3);
                    InstPtr.Set(IntermediateInstructions.JF_LBL, 0);
                }
            }
            return true;
        }

    }
}
