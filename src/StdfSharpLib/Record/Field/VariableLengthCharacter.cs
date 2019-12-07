/**
 * $Id: VariableLengthCharacter.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: VariableLengthCharacter.cs
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
    public class VariableLengthCharacter : AbstractField<string>
    {
        public VariableLengthCharacter()
        {
            Reset();
        }
        
        public override ushort Size
        {
            get
            {
                return Convert.ToUInt16(Value.Length + 1);
            }
        }

        protected override void ReadValue(BinaryReader reader)
        {
            byte bytesToRead = reader.ReadByte();
            if (bytesToRead == 0)
                return;
            Value = new string(reader.ReadChars(bytesToRead));
        }

        protected override void WriteValue(BinaryWriter writer)
        {
            writer.Write(Convert.ToByte(Value.Length));
            writer.Write(Value.ToCharArray());
        }

        /// <summary>
        /// Validate the field's value. If the value is null validate to false.
        /// </summary>
        protected override void DoValidate()
        {
            Valid = ((Value == null) ? false : (Value.Length > 0));
        }

        public override void ResetValue()
        {
            Value = string.Empty;
        }
    }
}