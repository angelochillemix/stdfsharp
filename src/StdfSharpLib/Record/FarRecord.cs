/**
 * $Id: FarRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: FarRecord.cs
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

using KA.StdfSharp;
using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{
    /// <summary>
    /// Represents the FAR record of STDF
    /// </summary>
    [StdfRecord(0, 10)]
    public sealed class FarRecord : StdfRecord
    {
        private IField<byte> cpuType = new StdfUByte(); // CPU_TYPE U*1 CPU type that wrote this file
        private IField<byte> version = new StdfUByte(); // STDF_VER U*1 STDF version number

        public FarRecord()
        {
            AddField("CPU_TYPE", cpuType);
            AddField("STDF_VER", version);
        }
        
        /// <summary>
        /// Returns cpu type information.
        /// </summary>
        public Cpu Cpu
        {
            get
            {
                return new Cpu(cpuType.Value);
            }
            
            set
            {
                cpuType.Value = (byte)value.Type;
            }
        }
        
        /// <summary>
        /// Represents the CPU_TYPE field of FAR record. 
        /// CPU type that wrote this file.
        /// </summary>
        /// <value>The field which represents the raw value of cpu type. 
        /// For more meaningful information use <code><see cref="StdfSharp.Cpu">Cpu</see></code> property.</value>
        public IField<byte> CpuType
        {
            get { return cpuType; }
        }

        /// <summary>
        /// Represents the STDF_VER field of FAR record. 
        /// STDF version number.
        /// </summary>
        public IField<byte> Version
        {
            get { return version; }
        }
    }
}