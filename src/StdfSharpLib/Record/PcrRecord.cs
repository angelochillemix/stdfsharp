/**
 * $Id: PcrRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: PcrRecord.cs
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
    /// Represents the PCR record of STDF.
    /// </summary>
    [StdfRecord(1, 30)]
    public sealed class PcrRecord : StdfRecord
    {
        private IField<byte> headNumber             = new StdfUByte(); // HEAD_NUM U*1 Test head number
        private IField<byte> siteNumber             = new StdfUByte(); // SITE_NUM U*1 Test site number
        private IField<uint> partCount              = new StdfUInt(); // PART_CNT U*4 Number of parts tested
        private IField<uint> retestedPartsCount     = new StdfUInt(); // RTST_CNT U*4 Number of parts retested
        private IField<uint> abortedTestCount       = new StdfUInt(); // ABRT_CNT U*4 Number of aborts during testing
        private IField<uint> goodPartsCount         = new StdfUInt(); // GOOD_CNT U*4 Number of good (passed) parts tested
        private IField<uint> functionalPartsCount   = new StdfUInt(); // FUNC_CNT U*4 Number of functional parts tested

        public PcrRecord()
        {
            AddField("HEAD_NUM", headNumber);
            AddField("SITE_NUM", siteNumber);
            AddField("PART_CNT", partCount);
            AddField("RTST_CNT", retestedPartsCount);
            AddField("ABRT_CNT", abortedTestCount);
            AddField("GOOD_CNT", goodPartsCount);
            AddField("FUNC_CNT", functionalPartsCount);
        }
        
        public IField<byte> HeadNumber
        {
            get { return headNumber; }
        }

        public IField<byte> SiteNumber
        {
            get { return siteNumber; }
        }

        public IField<uint> PartCount
        {
            get { return partCount; }
        }

        public IField<uint> RetestedPartsCount
        {
            get { return retestedPartsCount; }
        }

        public IField<uint> AbortedTestCount
        {
            get { return abortedTestCount; }
        }

        public IField<uint> GoodPartsCount
        {
            get { return goodPartsCount; }
        }

        public IField<uint> FunctionalPartsCount
        {
            get { return functionalPartsCount; }
        }
    }
}