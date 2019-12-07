/**
 * $Id: RecordNotFoundException.cs 7 2006-09-03 18:08:50Z Angelo $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: RecordNotFoundException.cs
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
using System.Runtime.Serialization;
using System.Text;

namespace KA.StdfSharp.Record
{
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        private readonly string details;
        private byte type;
        private byte subtype;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecordNotFoundException() : this(string.Empty)
        {
        }

        /// <summary>
        /// Creates a <code>RecordNotFoundException</code> with details message.
        /// </summary>
        /// <param name="details">The details message of the exception</param>
        public RecordNotFoundException(string details) : base(details)
        {
        }

        /// <summary>
        /// Creates a <code>RecordNotFoundException</code> for a a specific <code>type</code> and <code>subtype</code>
        /// with empty details message.
        /// </summary>
        /// <param name="type">The type of the record not found</param>
        /// <param name="subtype">The type of the record not found</param>
        public RecordNotFoundException(byte type, byte subtype) : this(type, subtype, string.Empty)
        {
        }

        /// <summary>
        /// Creates a <code>RecordNotFoundException</code> for a a specific <code>type</code> and <code>subtype</code>
        /// with details message.
        /// </summary>
        /// <param name="type">The type of the record not found</param>
        /// <param name="subtype">The type of the record not found</param>
        /// <param name="details">The details message of the exception</param>
        public RecordNotFoundException(byte type, byte subtype, string details) : base(details)
        {
            this.type = type;
            this.subtype = subtype;
            this.details = details;
        }

        /// <summary>
        /// Creates a new instance of this exception with serialized object data about the exception being thrown.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized data.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected RecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        ///<summary>
        ///Creates and returns a string representation of the current exception.
        ///</summary>
        ///
        ///<returns>
        ///A string representation of the current exception.
        ///</returns>
        ///<filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" /></PermissionSet>
        public override string ToString()
        {
            StringBuilder msg = new StringBuilder("No registered record found");
            msg.AppendFormat(" with type {0} and subtype {1}.", type, subtype);
            msg.Append('.');
            if (details != null)
                msg.Append(' ').Append(details);
            return msg.ToString();
        }

        /// <summary>
        /// The record's type not found.
        /// </summary>
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The record's subtype not found.
        /// </summary>
        public byte Subtype
        {
            get { return subtype; }
            set { subtype = value; }
        }
    }
}