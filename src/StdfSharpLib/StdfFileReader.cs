/**
 * $Id: StdfFileReader.cs 23 2008-06-08 15:17:18Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfFileReader.cs
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
using System.IO;
using KA.StdfSharp.Record;

namespace KA.StdfSharp
{
    public class RecordReadEventArgs : EventArgs
    {
        private StdfRecord record;
        private StdfRecordAttribute attribute;

        internal RecordReadEventArgs(StdfRecord record)
        {
            this.record = record;
            attribute = StdfRecord.GetStdfRecordAttribute(this.record.GetType());
        }

        public byte Type
        {
            get { return attribute.Type; }
        }

        public byte Subtype
        {
            get { return attribute.Subtype; }
        }

        public StdfRecord Record
        {
            get { return record; }
        }
    }

    public delegate void RecordReadEventHandler(object sender, RecordReadEventArgs e);
    
    public class StdfFileReader : IDisposable
    {
        public event RecordReadEventHandler RecordRead;

        private readonly Dictionary<byte, Dictionary<byte, RecordReadEventHandler>> RegisteredDelegates;

        private BinaryReader reader;
        private Stream stream;
        private StdfHeader header;
		
        private bool disposed = false;
        private bool closed = false;
        
        private static readonly ObjectDisposedException disposedException = new ObjectDisposedException(typeof(StdfFileReader).FullName, "Object already disposed");

        //private static readonly IStdfRecordFactory factory = CachedStdfRecordFactory.Instance;
        private static readonly IStdfRecordFactory factory = StdfRecordFactory.Instance;

        /// <summary>
        /// Initialized a new instance of <see cref="StdfFileReader"/> class based on the supplied stream.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        public StdfFileReader(Stream stream)
        {
            this.stream = stream;
            header = new StdfHeader();
            InitializeReader();
            RegisteredDelegates = new Dictionary<byte, Dictionary<byte, RecordReadEventHandler>>();
        }

        //~StdfFileReader()
        //{
        //    Dispose(false);
        //}

        /// <summary>
        /// Initializes the <see cref="BinaryReader"/> assuring the correct endianess will be used. 
        /// To estabilish endianess the FAR record (that should be the first record) is read.
        /// </summary>
        private void InitializeReader()
        {
            try
            {
				reader = BinaryReaderFactory.CreateDefaultBinaryReader(stream);
                FarRecord far = ReadFarRecord();
				//reader.Close();
				reader = BinaryReaderFactory.CreateBinaryReader(far.Cpu, stream);
            }
            catch (Exception e)
            {
                throw new StdfException("An error occurred during reader initialization", e);
            }
        }

        /// <summary>
        /// Reads the FAR record from the stream.
        /// </summary>
        /// <returns></returns>
        private FarRecord ReadFarRecord()
        {
            FarRecord record = (FarRecord)ReadRecord();
            stream.Position = 0;
            return record;
        }

        /// <summary>
        /// Registers a delegate for a specific record of type <code>type</code> and subtype <code>subtype</code>.
        /// </summary>
        /// <param name="type">The type of the record</param>
        /// <param name="subtype">The subtype of the record</param>
        /// <param name="recordReadDelegate">The delegate to register</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        /// <seealso cref="RecordReadEventHandler"/>
        /// <remarks>Delegate registration permits to clients to receive notification only after registered records are read.
        /// All not registered records are skipped and not read from the stream.</remarks>
        public void RegisterDelegate(byte type, byte subtype, RecordReadEventHandler recordReadDelegate)
        {
            if (disposed)
                throw disposedException;
            Dictionary<byte, RecordReadEventHandler> dict;
            if (!RegisteredDelegates.TryGetValue(type, out dict))
            {
                dict = new Dictionary<byte, RecordReadEventHandler>();
                RegisteredDelegates.Add(type, dict);
            }
            else
            {
                RecordReadEventHandler eventHandler;
                if (dict.TryGetValue(subtype, out eventHandler))
                    recordReadDelegate = (RecordReadEventHandler)Delegate.Combine(eventHandler, recordReadDelegate); // Combining multicast delegate for record of type "type" and subtype "subtype".
            }
            dict.Add(subtype, recordReadDelegate);
        }

        /// <summary>
        /// Registers a delegate for a specific <code>StdfRecord</code>.
        /// </summary>
        /// <param name="recordType">The record <code>Type</code> for which the delegated is registered.</param>
        /// <param name="recordReadDelegate">The delegate to register.</param>
        /// <exception cref="ArgumentException">If the passed <code>Type</code> is not a subclass of <code>StdfRecord</code>.</exception>
        /// <exception cref="AttributeNotDefinedException">If the <code>StdfRecordAttribute</code> is not defined for the passed record Type.</exception>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void RegisterDelegate(Type recordType, RecordReadEventHandler recordReadDelegate)
        {
            if (disposed)
                throw disposedException;
            CheckRecordType(recordType);
            StdfRecordAttribute attribute = StdfRecord.GetStdfRecordAttribute(recordType);
            RegisterDelegate(attribute.Type, attribute.Subtype, recordReadDelegate);
        }
        
        /// <summary>
        /// Removes a delegate registration for a specific record.
        /// </summary>
        /// <param name="type">The type of the record</param>
        /// <param name="subtype">The subtype of the record</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void RemoveDelegate(byte type, byte subtype)
        {
            if (disposed)
                throw disposedException;
            Dictionary<byte, RecordReadEventHandler> dict;
            if (RegisteredDelegates.TryGetValue(type, out dict) && dict.ContainsKey(subtype))
                dict.Remove(subtype);
        }

        /// <summary>
        /// Removes a delegate to be notified when the specified record is read.
        /// </summary>
        /// <param name="recordType">The Type of the record to remove</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void RemoveDelegate(Type recordType)
        {
            if (disposed)
                throw disposedException;
            CheckRecordType(recordType);
            StdfRecordAttribute attribute = StdfRecord.GetStdfRecordAttribute(recordType);
            RemoveDelegate(attribute.Type, attribute.Subtype);
        }

        private static void CheckRecordType(Type recordType)
        {
            string recordtype = "recordType";
            if (recordType == null)
                throw new ArgumentNullException(recordtype, "Object cannot be null");
            if (!recordType.IsSubclassOf(typeof(StdfRecord)))
                throw new ArgumentException("Only StdfRecord type can be registered", recordtype);
        }

        private RecordReadEventHandler this[byte type, byte subtype]
        {
            get
            {
                Dictionary<byte, RecordReadEventHandler> dict;
                if (RegisteredDelegates.TryGetValue(type, out dict))
                {
                    if (dict.ContainsKey(subtype))
                        return dict[subtype];
                }
                return null;
            }
        }
        
        /// <summary>
        /// Reads all the records raising the event and notifying all registered delegates.
        /// </summary>
        /// <seealso cref="RegisterDelegate(byte,byte,RecordReadEventHandler)"/>
        /// <seealso cref="RegisterDelegate(Type,RecordReadEventHandler)"/>
        /// <exception cref="StdfException">If an error occurs during the reading.</exception>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void Read()
        {
            if (disposed)
                throw disposedException;
            while (reader.PeekChar() != -1)
            {
                try
                {
                    ReadRecordHeader();
                    RecordReadEventHandler d = this[header.Type, header.Subtype];
                    if (d == null && RecordRead == null)
                    {
                        SkipRecord();
                        continue;
                    }
                    
                    ReadRecord(header, d);
                }
                catch (EndOfStreamException)
                {
                    return;
                }
                catch(RecordNotFoundException)
                {
                    SkipRecord();
                }
                catch (Exception e)
                {
                    throw new StdfException("The record cannot be read", e);
                }
            }
        }

        private StdfRecord ReadRecord(StdfHeader h, RecordReadEventHandler d)
        {
            StdfRecord record = factory.CreateRecord(h.Type, h.Subtype);
            record.Length = h.Lenght;
            record.Read(reader);
            RecordReadEventArgs e = new RecordReadEventArgs(record);
            OnRecordRead(e);
            if (d != null)
                d(this, e);
            return record;
        }
        
        /// <summary>
        /// Reads each record sequentially until the end of the stream raising the event and notifying all registered delegates.
        /// </summary>
        /// <returns>Returns the read record or null if the end of the stream is reached or an error occurred.</returns>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public StdfRecord ReadRecord()
        {
            if (disposed)
                throw disposedException;
            if (reader.PeekChar() == -1)
                return null;
            StdfRecord record = null;
            try
            {
                record = ReadRecord(ReadRecordHeader(), null);
            }
            catch (RecordNotFoundException)
            {
                SkipRecord();
            }
            return record;
        }

        /// <summary>
        /// Raises the event when a record is read.
        /// </summary>
        /// <param name="e">The <code>RecordReadEventArgs</code> to pass to the registered event listener.</param>
        protected virtual void OnRecordRead(RecordReadEventArgs e)
        {
            if (disposed)
                throw disposedException;
            if (RecordRead != null)
                RecordRead(this, e);
        }

        private void SkipRecord()
        {
            reader.BaseStream.Position += header.Lenght;
        }

        private StdfHeader ReadRecordHeader()
        {
            header.Read(reader);
            return header;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the StdfFileWriter and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    RegisteredDelegates.Clear();
					if (!closed)
					{
						Close();
                    }
                }
            }
            disposed = true;
        }        
        
        /// <summary>
        /// Releases the resources used by the <see cref="StdfFileReader"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
		
		/// <summary>
		/// Closes the underlying resources used by this reader
		/// </summary>
		public void Close()
		{
            if (reader != null)
            {
                ((IDisposable)reader).Dispose();
                reader = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
			closed = true;
		}
    }
}