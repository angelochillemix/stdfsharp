/**
 * $Id: AbstractField.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: AbstractField.cs
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
    /// <summary>
    /// Represents the base class for all fields.
    /// </summary>
    /// <typeparam name="V">The type of the field's value.</typeparam>
    public abstract class AbstractField<V> : IField<V>
    {
        private StringBuilder str = null;

        private object value;

        private bool validated = false;
        private bool valid = true;

        private string name = string.Empty;
        
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name.Equals(value))
                    return;
                name = value;
            }
        }

        /// <summary>
        /// Returns or sets the value of this field.
        /// </summary>
        public V Value
        {
            get
            {
                return (V)value;
            }
            
            set
            {
                ((IField) this).Value = value;
            }
        }

        public void Reset()
        {
            ResetValue();
        }

        public abstract void ResetValue();

        /// <summary>
        /// Returns or sets the value of this field.
        /// </summary>
        object IField.Value
        {
            get { return value; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value", "Object cannot be null");
                if ((this.value == value))
                    return;
                if (this.value != null && this.value.Equals(value))
                    return;
                this.value = value;
                validated = false;
            }
        }

        /// <summary>
        /// <see cref="IField.Valid"/>
        /// </summary>
        public bool Valid
        {
            get
            {
                if (!validated)
                    Validate();
                return valid;
            }
            set
            {
                valid = value;
                validated = true;
            }
        }

        /// <summary>
        /// Returns the size in bytes of this field.
        /// </summary>
        public abstract ushort Size { get; }

        /// <summary>
        /// Validates the field estabilishing only once if the field is valid or not.
        /// </summary>
        /// <remarks>It should be called before the Valid.
        /// This operation is done automatically if the class derives from the abstract class <code>AbstractField</code>.
        /// </remarks>
        public void Validate()
        {
            if (validated)
                return;
            DoValidate();
            validated = true;
        }

        /// <summary>
        /// Validate the field's value.
        /// </summary>
        /// <remarks>Each subclasses should override to validate field's value for specific condition.</remarks>
        protected virtual void DoValidate()
        {
            Valid = true;
        }
        

        /// <summary>
        /// Reads this field's value from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader from where to read the field's value.</param>
        protected abstract void ReadValue(BinaryReader reader);

        /// <summary>
        /// Reads this field from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader from where to read the field's value.</param>
        /// <exception cref="ArgumentNullException">If the passed reader is null.</exception>
        public void Read(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "Object cannot be null");
            if (reader.PeekChar() == -1)
                return;
            ReadValue(reader);
        }

        /// <summary>
        /// Writes this field to a binary writer. If the field's value is null nothing will be written.
        /// </summary>
        /// <param name="writer">The binary writer to which to write</param>
        /// <exception cref="ArgumentNullException">If the passed writer is null.</exception>
        public void Write(BinaryWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "Object cannot be null");
            if (Value == null)
                return;
            WriteValue(writer);
        }

        /// <summary>
        /// Writes this field's value to a binary writer.
        /// </summary>
        /// <param name="writer">The binary writer where to write the field's value.</param>
        protected abstract void WriteValue(BinaryWriter writer);
        
        public override int GetHashCode()
        {
            return value.GetHashCode() ^ value.GetHashCode();
        }
        
        /// <summary>
        /// Returns true if this field's value is equals to the passed not null object's value, otherwise false.
        /// obj must be an instance of IField.
        /// </summary>
        /// <param name="obj">The object to evaluate.</param>
        /// <returns>if this field's value is equals to the passed not null object's value, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this == obj)
                return true;
            IField field = obj as IField;
            return ((field != null) ? field.Value.Equals(value) : false);
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
            if (str == null)
                str = new StringBuilder();
            str.Length = 0;
            str.Append("Value = ").Append(Value.ToString()).Append(" Valid = ").Append(Valid.ToString());
            return str.ToString();
        }
    }
}