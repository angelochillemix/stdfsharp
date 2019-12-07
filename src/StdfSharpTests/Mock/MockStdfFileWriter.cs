/**
 * $Id: MockStdfFileWriter.cs 11 2006-10-20 21:11:16Z outburst $
 * 
 * STDFSharpTests
 *
 * File: MockStdfFileWriter.cs
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

namespace KA.StdfSharp.Tests.Mock
{
    public class MockStdfFileWriter
    {
        private static readonly Stream stream = new MemoryStream();

        private readonly StdfFileWriter writer = null;
        
        public MockStdfFileWriter(CpuType cpu)
        {
            writer = new StdfFileWriter(stream);
            InitializeStream(cpu);
        }

        private void InitializeStream(CpuType cpu)
        {
            FarRecord far = new FarRecord();
            far.Cpu = new Cpu(cpu);
            far.Version.Value = 4;
            WriteRecord(far);
        }
        
        public Stream Stream
        {
            get { return stream; }
        }

        /// <summary>
        /// Sets the stream at beginning position.
        /// </summary>
        public void Reset()
        {
            stream.Position = 0;
        }
        
        /// <summary>
        /// Writes a record through the <see cref="StdfFileWriter"/>. 
        /// </summary>
        /// <param name="record">The record to write.</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void WriteRecord(StdfRecord record)
        {
            writer.WriteRecord(record);
        }

        /// <summary>
        /// Writes the array of records.
        /// </summary>
        /// <param name="records">The record to write through the <see cref="StdfFileWriter"/>.</param>
        /// <exception cref="ObjectDisposedException">If the object is already disposed.</exception>
        public void WriteRecords(StdfRecord[] records)
        {
            writer.WriteRecords(records);
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            writer.Dispose();
        }
    }
}