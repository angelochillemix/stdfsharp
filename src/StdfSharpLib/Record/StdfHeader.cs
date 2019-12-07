/**
 * $Id: StdfHeader.cs 5 2006-09-01 00:55:06Z Angelo $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfHeader.cs
 * Description: Represents the header of a STDF file.
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

namespace KA.StdfSharp.Record
{
    internal class StdfHeader : IBinaryStorable
    {
        private ushort length;
        private byte type;
        private byte subtype;

        public static int Size
        {
            get { return sizeof(ushort) + 2 * sizeof(byte); }
        }
        
        public ushort Lenght
        {
            get { return length; }
            set { length = value; }
        }

        public byte Type
        {
            get { return type; }
            set { type = value; }
        }

        public byte Subtype
        {
            get { return subtype; }
            set { subtype = value; }
        }


        #region IBinaryStorable Members

        public void Read(System.IO.BinaryReader reader)
        {
            length = reader.ReadUInt16();
            type = reader.ReadByte();
            subtype = reader.ReadByte();
        }

        public void Write(System.IO.BinaryWriter writer)
        {
            writer.Write(length);
            writer.Write(type);
            writer.Write(subtype);
        }

        #endregion
    }
}