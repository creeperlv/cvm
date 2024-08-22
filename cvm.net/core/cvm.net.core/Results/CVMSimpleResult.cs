using System;
using System.Collections.Generic;
using System.Text;

namespace cvm.net.core.Results
{
	public struct CVMSimpleResult<T> where T : unmanaged
	{
		public bool IsSuccess;
		public T Value;

		public CVMSimpleResult(bool isSuccess, T value)
		{
			IsSuccess = isSuccess;
			Value = value;
		}
	}
}
