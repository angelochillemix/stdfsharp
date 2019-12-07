/**
 * $Id: StdfFile.cs 8 2006-09-11 05:36:47Z Angelo $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfFile.cs
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
using KA.StdfSharp.Record;

namespace KA.StdfSharp
{
    public class StdfFile
    {
        private string filePath = null;
        private byte version;
        private Cpu cpu;

        private bool farRecordRead;

        /// <summary>
        /// Creates a <code>StdfFile</code> from the specified <code>filePath</code>
        /// </summary>
        /// <param name="filePath">The path of the STDF file</param>
        public StdfFile(string filePath)
        {
            this.filePath = filePath;
        }

        private void ReadFarRecord()
        {
            using (StdfFileReader r = OpenForRead())
            {
                FarRecord record = (FarRecord)r.ReadRecord();
                cpu = new Cpu(record.CpuType.Value);
                version = record.Version.Value;
            }
            farRecordRead = true;
        }
        
        /// <summary>
        /// Represents the specification version used to wrote this STDF file.
        /// </summary>
        public StdfVersion Version
        {
            get
            {
                if (!farRecordRead)
                    ReadFarRecord();
                switch (version)
                {
                    case 3:
                        return StdfVersion.V3;
                    case 4:
                        return StdfVersion.V4;
                    default:
                        throw new StdfException("Unknown STDF version.");
                }
            }
        }
        
        /// <summary>
        /// Represents the cpu that wrote this STDF file.
        /// </summary>
        public Cpu Cpu
        {
            get
            {
                if (!farRecordRead)
                    ReadFarRecord();
                return cpu;
            }
        }
        
        /// <summary>
        /// Opens this file for reading.
        /// </summary>
        /// <returns></returns>
        /// <returns>An <code>StdfFileReader</code> used to read the STDF file.</returns>
        public StdfFileReader OpenForRead()
        {
            return new StdfFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        /// <summary>
        /// Opens this file for writing with exclusive access.
        /// </summary>
        /// <returns>An <code>StdfFileWriter</code> used to write the STDF file.</returns>
        public StdfFileWriter OpenForWrite()
        {
            return new StdfFileWriter(File.OpenWrite(filePath));
        }
    }
}