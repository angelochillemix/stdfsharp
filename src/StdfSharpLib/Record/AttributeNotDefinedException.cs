/**
 * $Id: AttributeNotDefinedException.cs 5 2006-09-01 00:55:06Z Angelo $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: AttributeNotDefinedException.cs
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
    public class AttributeNotDefinedException : Exception
    {
        private Type type;
        private string details;
        
        public AttributeNotDefinedException() : this(null, string.Empty)
        {
        }

        public AttributeNotDefinedException(string details) : this(null, details)
        {
        }

        public AttributeNotDefinedException(Type recordType) : this(recordType, string.Empty)
        {
        }

        public AttributeNotDefinedException(Type recordType, string details) : base(details)
        {
            type = recordType;
            this.details = details;
        }

        protected AttributeNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
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
            StringBuilder msg = new StringBuilder("StdfRecordAttribure not defined");
            if (type != null)
                msg.AppendFormat(" for record {0}", type.FullName);
            msg.Append('.');
            if (details != null)
                msg.Append(' ').Append(details);
            return msg.ToString();
        }

        ///<summary>
        ///Gets a message that describes the current exception.
        ///</summary>
        ///
        ///<returns>
        ///The error message that explains the reason for the exception, or an empty string("").
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public override string Message
        {
            get { return ToString(); }
        }
    }
}