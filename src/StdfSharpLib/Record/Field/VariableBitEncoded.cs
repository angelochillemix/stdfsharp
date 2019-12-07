/**
 * $Id: VariableBitEncoded.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: VariableBitEncoded.cs
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

namespace KA.StdfSharp.Record.Field
{
    public class VariableBitEncoded<R> : AbstractField<byte[]> where R : StdfRecord
    {
        private R record;
        
        public VariableBitEncoded(R record)
        {
            this.record = record;
            Reset();
        }
        
        /// <summary>
        /// Returns the size in bytes of this field.
        /// </summary>
        public override ushort Size
        {
            get { return Convert.ToUInt16(Value.Length + 1); }
        }

        /// <summary>
        /// Returns the parent record of this field.
        /// </summary>
        protected R ParentRecord
        {
            get { return record; }
        }

        /// <summary>
        /// Reads this field's value from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader from where to read the field's value.</param>
        protected override void ReadValue(BinaryReader reader)
        {
            byte bytesToRead = reader.ReadByte();
            if (bytesToRead == 0)
                return;
            Value = reader.ReadBytes(bytesToRead);
        }

        /// <summary>
        /// Writes this field's value to a binary writer.
        /// </summary>
        /// <param name="writer">The binary writer where to write the field's value.</param>
        protected override void WriteValue(BinaryWriter writer)
        {
            writer.Write(Convert.ToByte(Value.Length));
            writer.Write(Value);
        }

        public override void ResetValue()
        {
            Value = new byte[] { };
        }
    }
}