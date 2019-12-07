/**
 * $Id: PrrRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: PrrRecord.cs
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
using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{
    /// <summary>
    /// Represents the PRR record of STDF
    /// </summary>
    [StdfRecord(5, 20)]
    public class PrrRecord : StdfRecord
    {
        private IField<byte>        headNumber              = new StdfUByte(); // HEAD_NUM U*1 Test head number
        private IField<byte>        siteNumber              = new StdfUByte(); // SITE_NUM U*1 Test site number
        private IBitField           partFlag                = new PartFlagField(); // PART_FLG B*1 Part information flag
        private IField<ushort>      testExecutedCount       = new StdfUShort(); // NUM_TEST U*2 Number of tests executed
        private IField<ushort>      hardwareBin             = new StdfUShort(); // HARD_BIN U*2 Hardware bin number
        private IField<ushort>      softwareBin             = new StdfUShort(); // SOFT_BIN U*2 Software bin number
        private IField<short>       xCoordinate             = new Short(); // X_COORD I*2 (Wafer) X coordinate
        private IField<short>       yCoordinate             = new Short(); // Y_COORD I*2 (Wafer) Y coordinate
        private IField<DateTime>    elapsedTestTime         = new StdfDate(); // TEST_T U*4 Elapsed test time in milliseconds
        private IField<string>      partIdentification      = new StdfString(); // PART_ID C*n Part identification
        private IField<string>      partDescription         = new StdfString(); // PART_TXT C*n Part identification
        private IField<byte[]>      partRepairInformation; // PART_FIX B*n Part repair information


        public enum FieldName
        {
            /// <summary>
            /// Test head number
            /// </summary>
            HEAD_NUM,
            /// <summary>
            /// Test site number
            /// </summary>
            SITE_NUM,
            /// <summary>
            /// Part information flag
            /// </summary>
            PART_FLG,
            /// <summary>
            /// Number of tests executed
            /// </summary>
            NUM_TEST,
            /// <summary>
            /// Hardware bin number
            /// </summary>
            HARD_BIN,
            /// <summary>
            /// Software bin number
            /// </summary>
            SOFT_BIN,
            /// <summary>
            /// X coordinate
            /// </summary>
            X_COORD,
            /// <summary>
            /// Y coordinate
            /// </summary>
            Y_COORD,
            /// <summary>
            /// Elapsed test time in milliseconds
            /// </summary>
            TEST_T,
            /// <summary>
            /// Part identification
            /// </summary>
            PART_ID,
            /// <summary>
            /// Part identification
            /// </summary>
            PART_TXT,
            /// <summary>
            /// Part repair information
            /// </summary>
            PART_FIX
        }
        
        public PrrRecord()
        {
            partRepairInformation = new VariableBitEncoded<PrrRecord>(this);
            
            AddField(FieldName.HEAD_NUM.ToString(), headNumber);
            AddField(FieldName.SITE_NUM.ToString(), siteNumber);
            AddField(FieldName.PART_FLG.ToString(), partFlag);
            AddField(FieldName.NUM_TEST.ToString(), testExecutedCount);
            AddField(FieldName.HARD_BIN.ToString(), hardwareBin);
            AddField(FieldName.SOFT_BIN.ToString(), softwareBin);
            AddField(FieldName.X_COORD.ToString(), xCoordinate);
            AddField(FieldName.Y_COORD.ToString(), yCoordinate);
            AddField(FieldName.TEST_T.ToString(), elapsedTestTime);
            AddField(FieldName.PART_ID.ToString(), partIdentification);
            AddField(FieldName.PART_TXT.ToString(), partDescription);
            AddField(FieldName.PART_FIX.ToString(), partRepairInformation);
        }
        
        public bool SupersedePartIdSequence
        {
            get
            {
                return !(partFlag.EvaluateAnd((byte)PartFlagBit.SupersedePartIdSequence));
            }
        }

        public bool SupersedeCoordinateSequence
        {
            get
            {
                return !(partFlag.EvaluateAnd((byte)PartFlagBit.SupersedeCoordinateSequence));
            }
        }
        
        public bool TestNormallyCompleted
        {
            get
            {
                return partFlag.EvaluateAnd((byte)PartFlagBit.TestNormallyCompleted);
            }
        }
        
        public bool PartPassed
        {
            get
            {
                return partFlag.EvaluateAnd((byte)PartFlagBit.TestFailed);
            }
        }

        public bool PassFailFlagValid
        {
            get
            {
                return partFlag.EvaluateAnd((byte)PartFlagBit.NoPassFailFlag);
            }
        }

        public IField<byte> HeadNumber
        {
            get { return headNumber; }
        }

        public IField<byte> SiteNumber
        {
            get { return siteNumber; }
        }

        public IField<ushort> TestExecutedCount
        {
            get { return testExecutedCount; }
        }

        public IField<ushort> HardwareBin
        {
            get { return hardwareBin; }
        }

        public IField<ushort> SoftwareBin
        {
            get { return softwareBin; }
        }

        public IField<short> XCoordinate
        {
            get { return xCoordinate; }
        }

        public IField<short> YCoordinate
        {
            get { return yCoordinate; }
        }

        public IField<DateTime> ElapsedTestTime
        {
            get { return elapsedTestTime; }
        }

        public IField<string> PartIdentification
        {
            get { return partIdentification; }
        }

        public IField<string> PartDescription
        {
            get { return partDescription; }
        }

        public IField<byte[]> PartRepairInformation
        {
            get { return partRepairInformation; }
        }

        /// <summary>
        /// Represents the field PART_FLG of PRR record.
        /// </summary>
        /// <remarks>From STDF specification the first two bit must not be set both to 1. 
        /// So it possibile to check if it happens checking the validity of this field through <see cref="IField.Valid"/></remarks>
        public IField<byte> PartFlag
        {
            get { return partFlag; }
        }

        private class PartFlagField : BitEncoded<PrrRecord>
        {
            /// <summary>
            /// Validate the field's value.
            /// </summary>
            /// <remarks>Each subclasses should override to validate field's value.</remarks>
            protected override void DoValidate()
            {
                if (!(EvaluateAnd((byte)PartFlagBit.SupersedePartIdSequence)) && !(EvaluateAnd((byte)PartFlagBit.SupersedeCoordinateSequence)))
                    Valid = false;
            }
        }

        [Flags]
        private enum PartFlagBit : byte
        {
            Unknown = 0x00,
            SupersedePartIdSequence = 0x01,
            SupersedeCoordinateSequence = 0x02,
            TestNormallyCompleted = 0x04,
            TestFailed = 0x08,
            NoPassFailFlag = 0x10,
            All = Unknown | SupersedePartIdSequence | SupersedeCoordinateSequence | 
                  TestNormallyCompleted | TestFailed | NoPassFailFlag
        }
    }
}