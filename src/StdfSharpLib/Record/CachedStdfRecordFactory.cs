/**
 * $Id$
 * 
 * STDFSharp - STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: BufferedStdfRecordFactory
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

namespace KA.StdfSharp.Record
{
	/// <summary>
	/// Represents a factory of <code>StdfRecord</code> which caches the instances of created records.
	/// </summary>
	/// <remarks>
	/// Records number can be huge so to avoid creating many objects an instance of each record with specific
	/// type and subtype are cached and cleared at each reading. The client must read the information
	/// from the returned object without keeping a reference.
	/// </remarks>
    public class CachedStdfRecordFactory : IStdfRecordFactory
    {
        private static readonly IStdfRecordFactory instance = new CachedStdfRecordFactory();
        private readonly IStdfRecordFactory factory = StdfRecordFactory.Instance;
        private readonly Dictionary<byte, Dictionary<byte, StdfRecord>> recordsBuffer = new Dictionary<byte, Dictionary<byte, StdfRecord>>();

        public static IStdfRecordFactory Instance
        {
            get { return instance; }
        }

        #region IStdfRecordFactory Members

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
            Dictionary<byte, StdfRecord> dict = null;
            if (recordsBuffer.TryGetValue(type, out dict))
            {
                StdfRecord bufRecord;
                if (dict.TryGetValue(subtype, out bufRecord))
                {
                    bufRecord.Clear();
                    return bufRecord;
                }
            }
            StdfRecord record = factory.CreateRecord(type, subtype);
            if (dict == null)
            {
                dict = new Dictionary<byte, StdfRecord>();
                recordsBuffer.Add(type, dict);
            }
            dict.Add(subtype, record);
            return record;
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
            factory.RegisterRecord(type, subtype, record);
        }

        #endregion
    }
}