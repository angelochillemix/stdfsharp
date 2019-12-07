/**
 * $Id: IBinaryStorable.cs 5 2006-09-01 00:55:06Z Angelo $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: IBinaryStorable.cs
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

using System.IO;

namespace KA.StdfSharp
{
    /// <summary>
    /// Defines the capability to read and write to a binary stream.
    /// </summary>
    public interface IBinaryStorable
    {
        /// <summary>
        /// Reads from a binary reader.
        /// </summary>
        /// <param name="reader">The binary reader from which to read</param>
        void Read(BinaryReader reader);

        /// <summary>
        /// Writes to a binary writer.
        /// </summary>
        /// <param name="writer">The binary writer to which to write</param>
        void Write(BinaryWriter writer);
    }
}