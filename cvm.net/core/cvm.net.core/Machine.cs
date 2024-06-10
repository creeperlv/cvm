﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static cvm.net.core.libc.StdLib;
namespace cvm.net.core
{

	public delegate void FuncCall(ExecuteContext core);
	public class Machine
	{
		public List<RuntimeProgram> Programs = new List<RuntimeProgram>();
		public IDispatcher dispatcher;

		public Machine(IDispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
		}

		public Dictionary<int, FuncCall> Calls = new Dictionary<int, FuncCall>();
		//public List<ExecuteContext> Contexts = new List<ExecuteContext>();
	}
	public sealed unsafe class ExecuteContext : IDisposable
	{
		public const int RegisterCount = 512;
		public const int RegisterLimit = 256;
		public const int CallstackBlock = 16;
		public Machine Machine;
		public RuntimeProgram program;
		public bool WillRun=true;
		public Callframe* Callstack;
		public int CallstackSize;
		public int CallstackIndex;
		public byte* Registers;
		public bool OF;
		public bool JF;
		public T GetData<T>(int offset) where T : unmanaged
		{
			if (offset + sizeof(T) > RegisterLimit) return default;
			var ptr = Registers + offset;
			return ((T*)ptr)[0];
		}
		public void SetData<T>(int offset, T d) where T : unmanaged
		{
			if (offset + sizeof(T) > RegisterLimit) return;
			((T*)(Registers + offset))[0] = d;
		}
		public ExecuteContext(Machine machine, RuntimeProgram program)
		{
			Machine = machine;
			Registers = (byte*)malloc(sizeof(byte) * RegisterCount);
			Callstack = (Callframe*)malloc(sizeof(Callframe) * CallstackBlock);
			CallstackIndex = 0;
			CallstackSize = CallstackBlock;
			this.program = program;
		}
		public void AppendCallstack(Callframe frame)
		{
			if (CallstackIndex + 1 >= CallstackSize)
			{
				Callstack = (Callframe*)realloc(Callstack, CallstackSize + CallstackBlock);
				CallstackSize += CallstackBlock;
			}
			Callstack[CallstackIndex] = frame;
		}
		public void WriteCallstack(Callframe frame)
		{
			Callstack[CallstackIndex] = frame;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetLatestCallFrame(Callframe* frame)
		{
			frame[0] = Callstack[CallstackIndex];
		}
		public void Dispose()
		{
			free(Registers);
		}
	}
	public interface IDispatcher
	{
		void StartDispatcher(Machine machine);
		void Dispatch(ExecuteContext core);
		IEnumerable<ExecuteContext>? GetCPUCores();
	}
}