/**
 * $Id: CpuType.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: CpuType.cs
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

namespace KA.StdfSharp
{
    /// <summary>
    /// Cpu types enumeration as reported by STDF specifications.
    /// </summary>
    public enum CpuType
    {
        Vax = 0,
        Sun = 1,
        Sun386 = 2
    }

    public struct Cpu
    {
        private CpuType type;

        /// <summary>
        /// Initializes an instance of this Cpu.
        /// </summary>
        /// <param name="type">The <see cref="CpuType"/> of the cpu to initialize.</param>
        public Cpu(CpuType type)
        {
            this.type = type;
        }
        
        /// <summary>
        /// Initializes an instance of this Cpu.
        /// </summary>
        /// <param name="type">The type of the cpu to initialize.</param>
        /// <remarks>The byte value arrives from STDF field.</remarks>
        public Cpu(byte type)
        {
            switch (type)
            {
                case 0:
                    this.type = CpuType.Vax;
                    break;
                case 1:
                    this.type = CpuType.Sun;
                    break;
                case 2:
                    this.type = CpuType.Sun386;
                    break;
                default:
                    throw new ArgumentException("Unknown cpu type", "type");
            }
        }

        public CpuType Type
        {
            get { return type; }
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Cpu))
                return false;
            Cpu cpu = (Cpu)obj;
            return type == cpu.Type;
        }

        public static bool operator ==(Cpu cpu1, Cpu cpu2)
        {
            return cpu1.Equals(cpu2);
        }

        public static bool operator !=(Cpu cpu1, Cpu cpu2)
        {
            return !cpu1.Equals(cpu2);
        }
        
        public override int GetHashCode()
        {
            return type.GetHashCode() ^ type.GetHashCode();
        }

        /// <summary>
        /// Returns true if this cpu has little-endian endianess, otherwise false.
        /// </summary>
        /// <returns></returns>
        public bool IsLittleEndian
        {
			get
			{
	            switch (Type)
	            {
	                case CpuType.Vax:
	                case CpuType.Sun:
	                    return false;
	                case CpuType.Sun386:
	                    return true;
	            }
	            throw new ArgumentException("Unknown cpu type");
			}
        }
    }
    
    /// <summary>
    /// Utility class for endianess management.
    /// </summary>
    public static class Endianess
    {
        
        /// <summary>
        /// Returns true if the passed 
        /// </summary>
        /// <param name="cpu"></param>
        /// <returns></returns>
        public static bool IsLittleEndian(byte cpu)
        {
            switch (cpu)
            {
                case 0:
                case 1:
                    return false;
                case 2:
                    return true;
            }
            throw new ArgumentException("Unknown cpu type", "cpu");
        }

        public static bool IsLittleEndian(CpuType cpu)
        {
            return IsLittleEndian((byte) cpu);
        }
    }
}