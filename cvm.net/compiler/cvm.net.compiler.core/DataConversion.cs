using System.Numerics;

namespace cvm.net.compiler.core
{
	public class DataConversion
	{
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
			if (TryParseLong(name, out var r))
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
		public static bool TryParseLong(string name, out ulong data)
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
