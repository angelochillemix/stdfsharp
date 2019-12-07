/**
 * $Id: Char.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfChar.cs
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

namespace KA.StdfSharp.Record.Field
{
    public class StdfChar : AbstractField<char>
    {
        public StdfChar()
        {
            Reset();
        }
        
        /// <summary>
        /// Returns the size in bytes of this field.
        /// </summary>
        public override ushort Size
        {
            get { return sizeof(char); }
        }

        /// <summary>
        /// Validates the field estabilishing at once if the field is valid or not.
        /// </summary>
        /// <remarks>It should be called before the IsValid() method.
        /// This operation is done automatically if the class derives from the abstract class <code>AbstractField</code>.
        /// </remarks>
        protected override void DoValidate()
        {
            Valid = (!System.Char.IsWhiteSpace(Value));
        }

        protected override void ReadValue(BinaryReader reader)
        {
            Value = reader.ReadChar();
        }

        protected override void WriteValue(BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public override void ResetValue()
        {
            Value = ' ';
        }
    }
}