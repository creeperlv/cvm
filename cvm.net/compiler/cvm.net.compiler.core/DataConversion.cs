﻿using LibCLCC.NET.Operations;
using Microsoft.Win32;
using System;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace cvm.net.compiler.core
{
	public class DataConversion
	{
		public unsafe static bool TryParse<T>(string input, OperationResult<CVMObject> obj, out T result) where T : unmanaged, IParsable<T>
		{

			if (TryParse(input, out result)) return true;
			if (obj.Result.Consts.TryGetValue(input, out var value))
			{
				return TryParse(value, obj, out result);
			}
			return false;
		}
		public unsafe static bool TryParse<T>(string input, out T result) where T : unmanaged, IParsable<T>
		{
			if (T.TryParse(input, null, out result)) return true;
			T data = default;
			if (input.StartsWith("0x"))
			{
				if (ParseByteDataFromHexString(input[2..], (byte*)&data, sizeof(T)))
				{
					result = data;
					return true;
				}
			}
			if (input.StartsWith("0b"))
			{
				if (ParseByteDataFromBinaryString(input[2..], (byte*)&data, sizeof(T)))
				{
					result = data;
					return true;
				}
			}
			result = default;
			return false;
		}
		public static bool TryParseRegister(string name, OperationResult<CVMObject> obj, out byte register)
		{
			if (TryParseRegister(name, out register)) return true;
			if (obj.Result.Consts.TryGetValue(name, out var value))
			{
				return TryParseRegister(value, obj, out register);
			}
			return false;
		}
		public static bool TryParseRegister(string name, out byte register)
		{
			if (!name.StartsWith('$'))
			{
				register = 0;
				return false;
			}
			if (ISADefinition.CurrentDefinition.RegisterNames.TryGetValue(name, out register))
			{
				return true;
			}
			if (TryParseULong(name, out var r))
			{
				register = (byte)(r);
				return true;
			}
			return false;

		}
		static bool HexChar2Byte(char c, out byte b)
		{
			if (c >= '0' && c <= '9') { b = ((byte)(c - '0')); return true; }
			if (c >= 'A' && c <= 'F') { b = ((byte)(c - 'A' + 10)); return true; }
			if (c >= 'a' && c <= 'f') { b = ((byte)(c - 'a' + 10)); return true; }
			b = 0xFF;
			return false;
		}
		static bool BinChar2Byte(char c, out byte b)
		{
			if (c == '0')
			{
				b = 0;
				return true;
			}
			if (c == '1')
			{
				b = 1;
				return true;
			}
			b = 0xFF;
			return false;
		}

		/// <summary>
		/// Usage:ParseByteDataFromHexString("1F3",(byte*)ptr,sizeof(long));
		/// </summary>
		/// <param name="str"></param>
		/// <param name="ptr"></param>
		/// <param name="MaxLen"></param>
		/// <returns></returns>
		public unsafe static bool ParseByteDataFromHexString(string str, byte* ptr, int MaxLen, bool IsLittleEndian = true)
		{
			//MaxLen -= 1;
			int index = 0;
			byte b = 0;
			int _i = 1;
			for (int i = str.Length - 1; i >= 0; i--)
			{
				char item = str[i];
				if (!HexChar2Byte(item, out var _b))
				{
					return false;
				}
				if (_i % 2 == 0)
				{
					b += (byte)(_b << 4);
				}
				else
				{
					b = _b;
				}
				if (index > MaxLen)
				{
					return false;
				}
				if (_i % 2 == 0 && _i != 0)
				{
					if (IsLittleEndian)
						ptr[index] = b;
					else
						ptr[MaxLen - index - 1] = b;
					index++;
					b = 0;
				}
				_i++;
			}
			if (index <= MaxLen)
			{
				if (IsLittleEndian)
					ptr[index] = b;
				else
					ptr[MaxLen - index - 1] = b;
			}
			return true;
		}
		public unsafe static bool ParseByteDataFromBinaryString(string str, byte* ptr, int MaxLen, bool IsLittleEndian = true)
		{
			byte b = 0;
			int _i = 0;
			int __i = 1;
			for (int i = str.Length - 1; i >= 0; i--)
			{
				char item = str[i];
				if (item == '0')
				{
					b = (byte)(b | 0 << (__i - 1) % 8);
				}
				else if (item == '1')
				{
					b = (byte)(b | 1 << (__i - 1) % 8);

				}
				else if (item == '_')
				{
					continue;
				}
				else
				{
					return false;
				}
				if (__i % 8 == 0 && __i != 0)
				{
					if (IsLittleEndian)
						ptr[_i] = b;
					else
						ptr[MaxLen - _i - 1] = b;
					_i++;
					b = 0;
				}
				__i++;
			}
			if (_i <= MaxLen)
			{
				if (IsLittleEndian)
					ptr[_i] = b;
				else
					ptr[MaxLen - _i - 1] = b;
			}
			return true;
		}
		public static bool TryParseULong(string name, out ulong data)
		{
			if (ulong.TryParse(name, null, out data))
			{
				return true;
			}
			ulong d = 0;
			var n = name.ToLower();
			if (n.StartsWith("0x"))
			{
				n = n.Substring(2);

				for (int i = 0; i < n.Length; i++)
				{
					char c = n[i];
					if (HexChar2Byte(c, out var b) == false)
					{
						data = 0;
						return false;
					}
					d += b;
					if (i != 0)
						d *= 0x10;
				}
				data = d;
				return true;
			}
			if (n.StartsWith("0b"))
			{
				n = n.Substring(2);

				for (int i = 0; i < n.Length; i++)
				{
					char c = n[i];
					if (BinChar2Byte(c, out var b) == false)
					{
						data = 0;
						return false;
					}
					d += b;
					if (i != 0)
						d *= 0b10;
				}
				data = d;
				return true;
			}
			data = default;
			return false;
		}
	}
}
