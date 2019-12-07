/**
 * $Id: BigEndianBinaryReader.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: BigEndianBinaryReader.cs
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
using System.IO;
using System.Text;

namespace KA.StdfSharp
{
	/// <summary>
	/// Represents a BinaryReader that reads binary streams in big endian format.
	/// </summary>
    class BigEndianBinaryReader : BinaryReader
    {
		/// <summary>
		/// Creates an instance of a BigEndianBinaryReader that reads the passed stream
		/// </summary>
		/// <param name="stream">
		/// A <see cref="Stream"/> which the BigEndianBinaryReader reads from.
		/// </param>
        public BigEndianBinaryReader(Stream stream) : base(stream)
        {
        }

		/// <summary>
		/// Creates an instance of a BigEndianBinaryReader that reads the passed stream with a specified encoding.
		/// </summary>
		/// <param name="stream">
		/// A <see cref="Stream"/> which the BigEndianBinaryReader reads from.
		/// </param>
		/// <param name="encoding">
		/// A <see cref="Encoding"/> which the BigEndianBinaryReader uses to read from the stream.
		/// </param>
        public BigEndianBinaryReader(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }
        
		/// <summary>
		/// Reads an unsigned Int16
		/// </summary>
		/// <returns>
		/// A <see cref="System.UInt16"/>
		/// </returns>
        public override ushort ReadUInt16()
        {
            return (ushort) (((ushort) ReadByte()) << 8 | ReadByte());
        }

		/// <summary>
		/// Reads an unsigned Int32
		/// </summary>
		/// <returns>
		/// A <see cref="System.UInt32"/>
		/// </returns>
        public override uint ReadUInt32()
        {
            return (((uint)ReadByte()) << 24 | ((uint)ReadByte()) << 16 | ((uint)ReadByte()) << 8 | ReadByte());
        }

		/// <summary>
		/// Reads a float
		/// </summary>
		/// <returns>
		/// A <see cref="System.Single"/>
		/// </returns>
        public override float ReadSingle()
        {
            return Convert.ToSingle(ReadUInt32());
        }
    }
}