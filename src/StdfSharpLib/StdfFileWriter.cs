/**
 * $Id: StdfFileWriter.cs 12 2006-10-21 19:03:09Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfFileWriter.cs
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
using KA.StdfSharp.Record;

namespace KA.StdfSharp
{
    public class StdfFileWriter : IDisposable
    {
        private BinaryWriter writer;
        private Stream stream;

        private bool disposed = false;
        private bool closed = false;

        private static readonly ObjectDisposedException disposedException = new ObjectDisposedException(typeof(StdfFileWriter).FullName, "Object already disposed");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StdfFileWriter"/> class based on the supplied stream.
        /// </summary>
        /// <param name="stream">The output stream.</param>
        /// <remarks>UTF-8 encoding will be used as the encoding for strings.</remarks>
        public StdfFileWriter (Stream stream)
        {
            writer = new BinaryWriter(stream);
            this.stream = stream;
        }
        
        //~StdfFileWriter()
        //{
        //    Dispose(false);
        //}
        
        /// <summary>
        /// Writes a record through the <see cref="StdfFileWriter"/>. 
        /// </summary>
        /// <param name="record">The record to write.</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void WriteRecord(StdfRecord record)
        {
            if (disposed)
                throw disposedException;
            if (record == null)
                throw new ArgumentNullException("record", "Object cannot be null");
            record.Write(writer);
        }

        /// <summary>
        /// Writes the array of records.
        /// </summary>
        /// <param name="records">The record to write through the <see cref="StdfFileWriter"/>.</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void WriteRecords(StdfRecord[] records)
        {
            if (disposed)
                throw disposedException;
            if (records == null)
                throw new ArgumentNullException("records", "Object cannot be null");
            foreach (StdfRecord record in records)
            {
                WriteRecord(record);
            }
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
					if (!closed)
					{
						Close();
                    }
                }
            }
            disposed = true;
        }

        /// <summary>
        /// Releases the resources used by the <see cref="StdfFileWriter"/>.
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
            if (writer != null)
            {
                ((IDisposable)writer).Dispose();
                writer = null;
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