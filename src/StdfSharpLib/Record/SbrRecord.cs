/**
 * $Id: SbrRecord.cs 11 2006-10-20 21:11:16Z outburst $
 * 
 * STDFSharp
 *
 * File: HbrRecord.cs
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

namespace KA.StdfSharp.Record
{
    /// <summary>
    /// Represents the SBR record of STDF.
    /// </summary>
    [StdfRecord(1, 50)]
    public sealed class SbrRecord : BinRecord
    {
        public enum FieldName
        {
            HEAD_NUM,
            SITE_NUM,
            SBIN_NUM,
            SBIN_CNT,
            SBIN_PF,
            SBIN_NAM
        }

        public SbrRecord()
        {
            AddField(FieldName.HEAD_NUM.ToString(), HeadNumber);
            AddField(FieldName.SITE_NUM.ToString(), SiteNumber);
            AddField(FieldName.SBIN_NUM.ToString(), Number);
            AddField(FieldName.SBIN_CNT.ToString(), PartsCount);
            AddField(FieldName.SBIN_PF.ToString(),  PassFail);
            AddField(FieldName.SBIN_NAM.ToString(), Name);
        }

        public override BinType BinType
        {
            get { return Record.BinType.Software; }
        }
    }
}