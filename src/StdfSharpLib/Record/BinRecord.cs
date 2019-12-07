/**
 * $Id: BinRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: BinRecord.cs
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
using System.Collections.Generic;

using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{
    public enum BinStatus
    {
        Unknown,
        Passed,
        Fail
    }
    
    public enum BinType
    {
        Hardware,
        Software
    }
    
    /// <summary>
    /// Represents the base class of bin records.
    /// </summary>
    public abstract class BinRecord : StdfRecord
    {
        public const char RawPassedStatus = 'P';
        public const char RawFailStatus = 'F';
        public const char RawUnknownStatus = ' ';

        private IField<byte>    headNumber = new StdfUByte(); // HEAD_NUM U*1 Test head number
        private IField<byte>    siteNumber = new StdfUByte(); // SITE_NUM U*1 Test site number
        private IField<ushort>  number = new StdfUShort(); // SBIN_NUM U*2 Software bin number
        private IField<uint>    partsCount = new StdfUInt(); // SBIN_CNT U*4 Number of parts in bin
        private IField<char>    passFail = new StdfChar(); // SBIN_PF C*1 Pass/fail indication
        private IField<string>  name = new StdfString(); // SBIN_NAM C*n Name of software bin
		
        private static readonly IDictionary<char, BinStatus> RawToBinStatuses = new Dictionary<char,BinStatus>();
        private static readonly IDictionary<BinStatus, char> BinStatusesToRaw = new Dictionary<BinStatus, char>();
		
		static BinRecord()
		{
			RawToBinStatuses.Add(RawPassedStatus, BinStatus.Passed);
			RawToBinStatuses.Add(RawFailStatus, BinStatus.Fail);
			RawToBinStatuses.Add(RawUnknownStatus, BinStatus.Unknown);

			BinStatusesToRaw.Add(BinStatus.Passed, RawPassedStatus);
			BinStatusesToRaw.Add(BinStatus.Fail, RawFailStatus);
			BinStatusesToRaw.Add(BinStatus.Unknown, RawUnknownStatus);
		}

        public abstract BinType BinType
        {
            get;
        }

		/// <summary>Indicated whether the status of the bin is passed, failed or unknown</summary> 
		/// <value>
		/// One of the values represented by the BinStatus enumeration
		/// </value>
        public BinStatus Status
        {
            get
            {
				BinStatus status;
				if (RawToBinStatuses.TryGetValue(passFail.Value, out status))
					return status;
				return BinStatus.Unknown;
            }

            set
            {
				PassFail.Value = BinStatusesToRaw[value];
            }
        }

		/// <summary>
		/// Represents the head number
		/// </summary> 
        public IField<byte> HeadNumber
        {
            get { return headNumber; }
        }

		/// <summary>
		/// Represents the site number
		/// </summary> 
        public IField<byte> SiteNumber
        {
            get { return siteNumber; }
        }

		/// <summary>
		/// Represents the bin number
		/// </summary> 
        public IField<ushort> Number
        {
            get { return number; }
        }

		/// <summary>
		/// Represents the parts count
		/// </summary> 
        public IField<uint> PartsCount
        {
            get { return partsCount; }
        }

		/// <summary>
		/// Represents the status of the bin: passed, fail, unknown
		/// </summary> 
        public IField<char> PassFail
        {
            get { return passFail; }
        }

		/// <summary>
		/// Represents the name of the bin
		/// </summary> 
        public IField<string> Name
        {
            get { return name; }
        }
    }
}