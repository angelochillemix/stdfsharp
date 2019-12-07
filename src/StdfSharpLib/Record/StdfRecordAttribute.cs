/**
 * $Id: StdfRecordAttribute.cs 5 2006-09-01 00:55:06Z Angelo $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfRecordAttribute.cs
 * Description: Represents a custom attribute used to set information of a record
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

namespace KA.StdfSharp.Record
{
    /// <summary>
    /// Represents a custom attribute used to set information of a record, such as type and subtype
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class StdfRecordAttribute : Attribute
    {
        private byte type;
        private byte subtype;
        
        /// <summary>
        /// Creates a <code>StdfRecordAttribute</code> with specific type and subtype.
        /// </summary>
        /// <param name="type">The type information of the record</param>
        /// <param name="subtype">The subtype information of the record</param>
        public StdfRecordAttribute(byte type, byte subtype)
        {
            this.type = type;
            this.subtype = subtype;
        }

        /// <summary>
        /// Represents the type information of a record
        /// </summary>
        public byte Type
        {
            get { return type; }
        }

        /// <summary>
        /// Represents the subtype information of a record
        /// </summary>
        public byte Subtype
        {
            get { return subtype; }
        }
    }
}