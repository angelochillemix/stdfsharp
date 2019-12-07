/**
 * $Id: PirRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: PirRecord.cs
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

using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{
    /// <summary>
    /// Represents the PIR record of STDF
    /// </summary>
    [StdfRecord(5, 10)]
    public sealed class PirRecord : StdfRecord
    {
        private IField<byte> headNumber = new StdfUByte(); // HEAD_NUM U*1 Test head number
        private IField<byte> siteNumber = new StdfUByte(); // SITE_NUM U*1 Test site number

        public PirRecord()
        {
            AddField("HEAD_NUM", headNumber);
            AddField("SITE_NUM", siteNumber);
        }

        public IField<byte> HeadNumber
        {
            get { return headNumber; }
        }

        public IField<byte> SiteNumber
        {
            get { return siteNumber; }
        }
    }
}