/**
 * $Id: StdfRecord.cs 22 2008-06-08 13:58:52Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfRecord.cs
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
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{ 
    public abstract class StdfRecord : IBinaryStorable
    {
        private FieldRegistry fieldRegistry;

        private readonly StdfHeader header;

        private StdfRecordAttribute attribute;
        //private ushort bytesToRead;
		
		private BytesReadChecker bytesChecker = new BytesReadChecker();

        protected StdfRecord()
        {
            attribute = GetStdfRecordAttribute(GetType());
            fieldRegistry = new FieldRegistry();
            header = new StdfHeader();
        }

        public void Clear()
        {
			bytesChecker.Clear();
            //bytesToRead = 0;
            foreach (IField field in fieldRegistry)
            {
                field.Reset();
            }
        }

        public byte Type
        {
            get { return attribute.Type;  }
        }

        public byte Subtype
        {
            get { return attribute.Subtype; }
        }
        
        /// <summary>
        /// Returns a field named <code>name</code>.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>A <see cref="IField"/> or null if a field named <code>name</code> does not exist.</returns>
        /// <remarks>The name should be the same as reported by the STDF specification.</remarks>
        public IField this[string name]
        {
            get
            {
                return fieldRegistry[name];
            }
        }
        
        /// <summary>
        /// Add a field to this record.
        /// </summary>
        /// <param name="name">The name of the field to add.</param>
        /// <param name="field">The field to add.</param>
        /// <remarks>Each subclasses must add its fields in a ordered way, 
        /// the same used to read and write these fields from and to the STDF file.</remarks>
        internal protected void AddField(string name, IField field)
        {
            fieldRegistry.RegisterField(name, field);
        }

        /// <summary>
        /// Reads the record from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader used to read the record.</param>
        public void Read(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "Object cannot be null");
            
            if (reader.PeekChar() == -1)
                return;

            //ushort bytesRead = 0;
			bytesChecker.ResetCount();
            foreach (IField field in fieldRegistry)
            {
                if (reader.PeekChar() == -1)
                    return;
				if (!bytesChecker.BytesCountInRange())
					return;
				
                field.Read(reader);
				bytesChecker.IncreaseBytesRead(field.Size);
				//bytesRead += field.Size;
                field.Validate();
            }
        }
		
		private class BytesReadChecker
		{
			private ushort bytesRead   = 0;
			private ushort bytesToRead = 0;
			
			public void Clear()
			{
				bytesToRead = 0;
				ResetCount();
			}
			
			public void ResetCount()
			{
				bytesRead = 0;
			}
			
			public ushort BytesToRead
			{
				set
				{
					bytesToRead = value;
				}
			}
			
	        public bool BytesCountInRange()
	        {
	            return (bytesToRead == 0 || bytesRead < bytesToRead);
	        }
			
			public void IncreaseBytesRead(ushort increment)
			{
				bytesRead += increment;
			}
		}

//        private bool CanRead(ushort bytesRead)
//        {
//            return (bytesToRead == 0 || bytesRead < bytesToRead);
//        }

        public void Validate()
        {
            foreach (IField field in fieldRegistry)
            {
                field.Validate();
            }
        }

        /// <summary>
        /// Writes the header of each records and delegates the writing of fields to subclasses.
        /// </summary>
        /// <param name="writer">The writer where to write the record</param>
        public virtual void Write(BinaryWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "Object cannot be null");
            
            WriteHeader(writer);

            foreach (IField field in fieldRegistry)
                field.Write(writer);
            writer.Flush();
        }

        private void WriteHeader(BinaryWriter writer)
        {
            Debug.Assert(header != null);
            header.Lenght = Length;
            header.Type = attribute.Type;
            header.Subtype = attribute.Subtype;
            header.Write(writer);
        }

        /// <summary>
        /// Returns the length in bytes automatically calculated summing the size of each fields of the record.
        /// </summary>
        /// <value>Represents the number of bytes to read the entire record.</value>
        /// <remarks>For each <code>IField</code>, the value of the <code>Size</code> property is requested to calculate 
        /// the whole record's length.
        /// The property can be overriden by subclasses to use different calculation method.</remarks>
        public virtual ushort Length
        {
            get
            {
                return fieldRegistry.ByteLength;
            }
            
            set
            {
				bytesChecker.BytesToRead = value;
                //bytesToRead = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="StdfRecordAttribute"/> of type. <code>type</code> must be a StdfRecord.
        /// </summary>
        /// <param name="type">The type for which the attribute must be retrieved.</param>
        /// <returns>The <see cref="StdfRecordAttribute"/> of type or null if it is not defined.</returns>
        /// <exception cref="ArgumentNullException">If type is null.</exception>
        /// <exception cref="ArgumentException">If type is not a subclass of StdfRecord type.</exception>
        public static StdfRecordAttribute GetStdfRecordAttribute(Type type)
        {
            Debug.Assert(type != null && type.IsSubclassOf(typeof(StdfRecord)));
            if (type == null)
                throw new ArgumentNullException("type", "Object cannot be null");
            if (!(type.IsSubclassOf(typeof(StdfRecord))))
                throw new ArgumentException("Type must be a StdfRecord", "type");
            
            return Attribute.GetCustomAttribute(type, typeof(StdfRecordAttribute)) as StdfRecordAttribute;
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
            StringBuilder recordInfo = new StringBuilder();
            StringBuilder str = new StringBuilder();
            foreach (string name in fieldRegistry.FieldsName)
            {
                if (str.Length > 0)
                    str.Append(", ");
                str.Append("Field ").Append(name).Append(": ").Append(fieldRegistry[name].ToString());
            }
            recordInfo.Append("Record ").Append(GetType().Name).Append(", ").
                Append(", ").Append("Type: ").Append(Type).
                Append(", ").Append("Subtype: ").Append(Subtype).
                Append(", ").Append("Lenght: ").Append(Length).
                Append(", ").Append(str);
            return recordInfo.ToString();
        }

        internal sealed class FieldRegistry : IEnumerable<IField>
        {
            private Dictionary<string, IField> fields;
            private List<IField> fieldList;

            public FieldRegistry()
            {
                fields = new Dictionary<string, IField>();
                fieldList = new List<IField>();
            }

            internal void RegisterField(string name, IField field)
            {
                fields.Add(name, field);
                fieldList.Add(field);
            }

            /// <summary>
            /// Returns a field named <code>name</code>.
            /// </summary>
            /// <param name="name">The name of the field.</param>
            /// <returns>A <see cref="IField"/> or null if a field named <code>name</code> does not exist.</returns>
            /// <remarks>The name should be the same as reported by the STDF specification.</remarks>
            public IField this[string name]
            {
                get
                {
                    IField field;
                    if (!fields.TryGetValue(name, out field))
                        return null;
                    return field;
                }
            }

            /// <summary>
            /// Returns an array of fields' names</code>.
            /// </summary>
            /// <returns>An array of fields' names.</returns>
            public string[] FieldsName
            {
                get
                {
                    string[] names = new string[fields.Keys.Count];
                    fields.Keys.CopyTo(names, 0);
                    return names;
                }
            }

            /// <summary>
            /// Returns the length in bytes automatically calculated summing the size of each fields of the record.
            /// </summary>
            /// <value>Represents the number of bytes to read the entire record.</value>
            /// <remarks>For each <code>IField</code>, the value of the <code>Size</code> property is requested to calculate 
            /// the whole record's length.
            /// The property can be overriden by subclasses to use different calculation method.</remarks>
            public ushort ByteLength
            {
                get
                {
                    ushort length = 0;
                    foreach (IField field in fieldList)
                        length += field.Size;
                    return length;
                }
            }

            public IEnumerator<IField> GetEnumerator()
            {
                return fieldList.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}