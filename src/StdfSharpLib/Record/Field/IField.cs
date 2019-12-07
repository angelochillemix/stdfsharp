/**
 * $Id: IField.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: IField.cs
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

namespace KA.StdfSharp.Record.Field
{
    public interface IField : IBinaryStorable
    {
        /// <summary>
        /// Represents the name of the field.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the value of the field.
        /// </summary>
        object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the size in bytes of the field.
        /// </summary>
        ushort Size
        {
            get;
        }

        /// <summary>
        /// true if the field's value is valid, otherwise false (i.e. the field is invalid or missing).
        /// </summary>
        /// <value>true if the field's value is valid, otherwise false (i.e. the field is invalid or missing).</value>
        /// <remarks>
        /// Don't set validity. It should be set exclusively by the <see cref="Validate"/> method. Used internally by field flag.
        /// This operation is done automatically if the class derives from the abstract class <see cref="AbstractField{V}"/>.
        /// </remarks>
        bool Valid
        {
            get;
            set;
        }

        ///// <summary>
        ///// true if the field's value is valid, otherwise false (i.e. the field is invalid or missing).
        ///// </summary>
        ///// <returns>true if the field's value is valid, otherwise false (i.e. the field is missing)</returns>
        ///// <remarks>
        ///// The validity should be set by the <see cref="Validate"/> method. So, this method should be called before isValid() method.
        ///// This operation is done automatically if the class derives from the abstract class <see cref="AbstractField{V}"/>.
        ///// </remarks>
        //bool IsValid();

        /// <summary>
        /// Validates the field estabilishing if the field is valid or not.
        /// </summary>
        /// <remarks>It should be called before the IsValid() method and only once for heavy operation.
        /// This operation is done automatically if the class derives from the abstract class <see cref="AbstractField{V}"/>.
        /// </remarks>
        void Validate();

        /// <summary>
        /// resets the value of the field.
        /// </summary>
        void Reset();
    }
    
    
    /// <summary>
    /// Defines a generic interface for field of STDF record.
    /// </summary>
    /// <typeparam name="T">Used as internal type for size, reading and writing.</typeparam>
    public interface IField<T> : IField
    {
        /// <summary>
        /// Represents the value of the field which type is specified by the generic parameter.
        /// </summary>
        /// <value>The value of the field</value>
        new T Value
        {
            get;
            set;
        }
    }
}