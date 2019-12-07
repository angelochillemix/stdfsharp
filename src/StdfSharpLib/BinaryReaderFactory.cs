///**
// * $Id$
// * 
// * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
// *
// * File: 
// * Description:
// * 
// * Copyright (C) 2006 Outburst <outburst@users.sourceforge.net>
// *  
// * This library is free software; you can redistribute it and/or 
// * modify it under the terms of the GNU Lesser General Public License 
// * as published by the Free Software Foundation; either version 2.1 
// * of the License, or (at your option) any later version.
// * 
// * This library is distributed in the hope that it will be useful, 
// * but WITHOUT ANY WARRANTY; without even the implied warranty of 
// * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// * GNU Lesser General Public License for more details.
// * 
// * You should have received a copy of the GNU Lesser General Public License
// * along with this library; if not, write to the Free Software Foundation, 
// * Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// */

using System;
using System.IO;

namespace KA.StdfSharp
{
	
	/// <summary>
	/// Represents a factory of BinaryReader instances. Provides a default implementation
	/// or a proper one depending on CPU architecture
	/// </summary>
	public static class BinaryReaderFactory
	{
		/// <summary>
		/// Returns a new instance of BinaryReader that reads on the passed stream. 
		/// </summary>
		/// <returns>
		/// A <see cref="BinaryReader"/>
		/// </returns>
		public static BinaryReader CreateDefaultBinaryReader(Stream stream)
		{
			return new BinaryReader(stream);
		}

		/// <summary>
		/// Returns a new instance of BinaryReader that reads on the passed stream.
		/// A <see cref="Cpu"/> which represents the CPU of the platform where the STDF file has been generated
		/// </param>
		/// <returns>
		/// A <see cref="BinaryReader"/> depending on CPU architecture.
		/// </returns>
		public static BinaryReader CreateBinaryReader(Cpu cpu, Stream stream)
		{
			if (cpu.IsLittleEndian)
				return CreateDefaultBinaryReader(stream);
			return new BigEndianBinaryReader(stream);
		}
	}
}
