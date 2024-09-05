namespace cvm.net.core
{
	public struct IODefinition
    {
        InputPortCall IN;
		OutputPortCall OUT;
	}
	public unsafe delegate void InputPortCall(ExecuteContext context, byte* register, int length);
	public unsafe delegate void OutputPortCall(ExecuteContext context, byte* register, int length);
}