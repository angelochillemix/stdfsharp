/**
 * $Id: StdfRecordFactory.cs 22 2008-06-08 13:58:52Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfRecordFactory.cs
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
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using KA.StdfSharp;

namespace KA.StdfSharp.Record
{
    public class StdfRecordFactory : IStdfRecordFactory
    {
        private static readonly StdfRecordFactory instance = new StdfRecordFactory();

        private readonly Dictionary<byte, Dictionary<byte, Type>> RegisteredRecords = new Dictionary<byte, Dictionary<byte, Type>>();
        
        private readonly RecordNotFoundException recordNotFoundException = new RecordNotFoundException();

        static StdfRecordFactory()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(StdfRecordFactory));
            Type[] types = assembly.GetTypes();
            StringBuilder s = new StringBuilder();
            RegisterRecords(types, s);
            if (s.Length > 0)
            {
                s.Insert(0, "The StdfRecordAttribute attribute is not defined for the following records: ");
                throw new AttributeNotDefinedException(s.ToString());
            }
        }

        private static void RegisterRecords(Type[] types, StringBuilder s)
        {
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(typeof(StdfRecord)) && (!type.IsAbstract))
                {
                    StdfRecordAttribute attribute = StdfRecord.GetStdfRecordAttribute(type);
                    if (attribute == null)
                    {
                        if (s.Length > 0)
                            s.Append(", ");
                        s.Append(type.Name);
                        continue;
                    }
                    Instance.RegisterRecord(attribute.Type, attribute.Subtype, type);
                }
            }
        }

        private StdfRecordFactory()
        {
        }
        
        public static StdfRecordFactory Instance
        {
            get { return instance; }
        }
        
        /// <summary>
        /// Creates an empty record of <code>type</code> and <code>subtype</code> specified by <code>header</code>.
        /// </summary>
        /// <param name="header">The header of the record.</param>
        /// <returns>A record of type and subtype.</returns>
        /// <exception cref="InvalidOperationException">If the type has no public constructor and hence no instance can be created.</exception>
        /// <exception cref="StdfException">If the record instance cannot be created.</exception>
        /// <exception cref="RecordNotFoundException">If the record with specified header does not exist.</exception>
        internal StdfRecord CreateRecord(StdfHeader header)
        {
            StdfRecord record = CreateRecord(header.Type, header.Subtype);
            record.Length = header.Lenght;
            return record;
        }

        /// <summary>
        /// Creates a STDF record instance of the specified <code>type</code> and <code>subtype</code>.
        /// </summary>
        /// <param name="type">The type of record to create.</param>
        /// <param name="subtype">The subtype of record to create.</param>
        /// <returns>A record of specified type and subtype.</returns>
        /// <exception cref="RecordNotFoundException">If the record was not found.</exception>
        /// <exception cref="StdfException">If the record cannot be created.</exception>
        public StdfRecord CreateRecord(byte type, byte subtype)
        {
            Dictionary<byte, Type> dict;
            if (RegisteredRecords.TryGetValue(type, out dict))
            {
                try
                {
                    Type recordType;
                    if (dict.TryGetValue(subtype, out recordType))
                        return (StdfRecord)Activator.CreateInstance(recordType);
                }
                catch (MissingMethodException m)
                {
                    throw new StdfException(String.Format(CultureInfo.InvariantCulture, "Cannot create record instance with type {0} and subtype {1}", type, subtype), m);
                }
            }
            recordNotFoundException.Type = type;
            recordNotFoundException.Subtype = subtype;
            throw recordNotFoundException;
        }

		/// <summary>
		/// Creates a STDF record instance of the specified <code>Type</code>.
		/// </summary>
		/// <param name="recordType">
		/// A <see cref="Type"/> which represents the type of <code>StdfRecord</code>.
		/// </param>
		/// <returns>
		/// A <see cref="StdfRecord"/> which represents the instance of the record.
		/// </returns>
        public StdfRecord CreateRecord(Type recordType)
        {
            StdfRecordAttribute attribute = StdfRecord.GetStdfRecordAttribute(recordType);
            return CreateRecord(attribute.Type, attribute.Subtype);
        }
        
        public void RegisterRecord(byte type, byte subtype, Type record)
        {
            if (!record.IsSubclassOf(typeof(StdfRecord)))
                throw new ArgumentException("Only StdfRecord type can be registered");
            Dictionary<byte, Type> dict;
            if (!RegisteredRecords.TryGetValue(type, out dict))
            {
                dict = new Dictionary<byte, Type>();
                RegisteredRecords.Add(type, dict);
            }
            try
            {
                dict.Add(subtype, record);
            }
            catch (ArgumentException e)
            {
                throw new StdfException(String.Format("A record with type {0} and subtype {1} is already registered", type, subtype), e);
            }
        }
    }
}