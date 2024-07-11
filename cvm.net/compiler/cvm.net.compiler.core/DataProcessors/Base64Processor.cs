using cvm.net.assembler.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cvm.net.compiler.core.DataProcessors
{
	public class Base64Processor : IDataProcessor
	{
		public byte[] Process(string str)
		{
			return Convert.FromBase64String(str);
		}
	}
}
