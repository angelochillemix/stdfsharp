/**
 * $Id: FixedLengthCharacter.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: FixedLengthCharacter.cs
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

namespace KA.StdfSharp.Record.Field
{
    public class FixedLengthCharacter : AbstractField<char[]>
    {
        private int length;
        
        public FixedLengthCharacter(int length)
        {
            this.length = length;
            Reset();
        }

        /// <summary>
        /// Returns the size in bytes of this field.
        /// </summary>
        public override ushort Size
        {
            get { return Convert.ToUInt16(sizeof(char) * length); }
        }

        /// <summary>
        /// Reads this field's value form the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader from where to read the field's value.</param>
        protected override void ReadValue(BinaryReader reader)
        {
            Value = reader.ReadChars(length);
        }

        /// <summary>
        /// Writes this field's value to a binary writer.
        /// </summary>
        /// <param name="writer">The binary writer where to write the field's value.</param>
        protected override void WriteValue(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Value = ");
            foreach (char c in Value)
                str.Append(c); 
            str.Append(" Valid = ").Append(Valid.ToString());
            return str.ToString();
        }

        public override void ResetValue()
        {
            Value = new char[length];
            for (int i = 0; i < length; i++)
                Value[i] = ' ';
        }
    }
}