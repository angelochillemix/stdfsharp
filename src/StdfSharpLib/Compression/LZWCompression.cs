/**
 * $Id: LZWCompression.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: LZWCompression.cs
 * Description:
 * 
 * Copyright (C) 2006 Outburst <outburst@users.sourceforge.net>
 *  
 * This library is free software; you can redistribute it and/or 
 * modify it under the terms of the GNU Lesser General Public License 
 * as published by the Free Software Foundation; either version 2.1 
 * of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful, 
 * but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this library; if not, write to the Free Software Foundation, 
 * Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KA.StdfSharp.Compression
{
	public class LZWCompression
	{
        private const int NumBytesPerCode = 2;

        private int ReadCode(BinaryReader reader)
        {
            int code = 0;
            int shift = 0;

            for (int i = 0; i < NumBytesPerCode; i++)
            {
                byte nextByte = reader.ReadByte();
                code += nextByte << shift;
                shift += 8;
            }

            return code;
        }

        private void WriteCode(BinaryWriter writer, int code)
        {
            int shift = 0;
            int mask = 0xFF;

            for (int i = 0; i < NumBytesPerCode; i++)
            {
                byte nextByte = (byte)((code >> shift) & mask);
                writer.Write(nextByte);
                shift += 8;
            }
        }

        public void Compress(StreamReader input, BinaryWriter output)
        {
            LzwStringTable table = new LzwStringTable(NumBytesPerCode);

            char firstChar = (char)input.Read();
            string match = firstChar.ToString();

            while (input.Peek() != -1)
            {
                char nextChar = (char)input.Read();
                string nextMatch = match + nextChar;

                if (table.Contains(nextMatch))
                {
                    match = nextMatch;
                }
                else
                {
                    WriteCode(output, table.GetCode(match));
                    table.AddCode(nextMatch);
                    match = nextChar.ToString();
                }
            }
            WriteCode(output, table.GetCode(match));
        }

        public void Decompress(BinaryReader input, StreamWriter output)
        {
            List<string> table = new List<string>();

            for (int i = 0; i < 256; i++)
            {
                char ch = (char)i;
                table.Add(ch.ToString());
            }

            int firstCode = ReadCode(input);
            char matchChar = (char)firstCode;
            string match = matchChar.ToString();

            output.Write(match);

            while (input.PeekChar() != -1)
            {
                int nextCode = ReadCode(input);

                string nextMatch;
                if (nextCode < table.Count)
                    nextMatch = table[nextCode];
                else
                    nextMatch = match + match[0];

                output.Write(nextMatch);

                table.Add(match + nextMatch[0]);
                match = nextMatch;
            }
        }
	}

    internal class LzwStringTable
    {
        private Dictionary<string, int> table = new Dictionary<string, int>();
        private int nextAvailableCode = 256;
        private int maxCode;
		
        public LzwStringTable(int numBytesPerCode)
        {
            maxCode = (1 << (8 * numBytesPerCode)) - 1;
        }

        public void AddCode(string s)
        {
            if (nextAvailableCode <= maxCode)
            {
                if (s.Length != 1 && !table.ContainsKey(s))
                    table[s] = nextAvailableCode++;
            }
            else
            {
                throw new Exception("LZW string table overflow");
            }
        }

        public int GetCode(string s)
        {
            if (s.Length == 1)
                return (int)s[0];
            else
                return table[s];
        }

        public bool Contains(string s)
        {
            return s.Length == 1 || table.ContainsKey(s);
        }
    }
	
}