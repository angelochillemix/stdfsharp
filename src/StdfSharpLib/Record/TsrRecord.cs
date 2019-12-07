/**
 * $Id: TsrRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: TsrRecord.cs
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
    /// Represents the TSR record of STDF.
    /// </summary>
    [StdfRecord(10, 30)]
    public class TsrRecord : StdfRecord
    {
        private IField<byte>        headNumber              = new StdfUByte(); // HEAD_NUM U*1 Test head number
        private IField<byte>        siteNumber              = new StdfUByte(); // SITE_NUM U*1 Test site number
        private IField<char>        testType                = new StdfChar(); // TEST_TYP C*1 Test type
        private IField<uint>        testNumber              = new StdfUInt(); // TEST_NUM U*4 Test number
        private IField<uint>        executionCount          = new StdfUInt(); // EXEC_CNT U*4 Number of test executions
        private IField<uint>        failCount               = new StdfUInt(); // FAIL_CNT U*4 Number of test failures
        private IField<uint>        alarmCount              = new StdfUInt(); // ALRM_CNT U*4 Number of alarmed tests
        private IField<string>      testName                = new StdfString(); // TEST_NAM C*n Test name
        private IField<string>      sequencerName           = new StdfString(); // SEQ_NAME C*n Sequencer (program segment/flow) name 
        private IField<string>      testLabel               = new StdfString(); // TEST_LBL C*n Test label or text
        private IBitField           optionalDataFlag        = null; // OPT_FLAG B*1 Optional data flag 
        private IField<DateTime>    executionTime           = new StdfDate(); // TEST_TIM R*4 Average test execution time in seconds
        private IField<float>       lowestResultValue       = new StdfFloat(); // TEST_MIN R*4 Lowest test result value
        private IField<float>       highestResultValue      = new StdfFloat(); // TEST_MAX R*4 Highest test result value
        private IField<float>       resultValuesSum         = new StdfFloat(); // TST_SUMS R*4 Sumof test result values
        private IField<float>       resultValuesSquareSum   = new StdfFloat(); // TST_SQRS R*4 Sum of squares of test result values

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
            /// Test type
            /// </summary>
            TEST_TYP,
            /// <summary>
            /// Test number
            /// </summary>
            TEST_NUM,
            /// <summary>
            /// Number of test executions
            /// </summary>
            EXEC_CNT,
            /// <summary>
            /// Number of test failures
            /// </summary>
            FAIL_CNT,
            /// <summary>
            /// Number of alarmed tests
            /// </summary>
            ALRM_CNT,
            /// <summary>
            /// Test name
            /// </summary>
            TEST_NAM,
            /// <summary>
            /// Sequencer (program segment/flow) name
            /// </summary>
            SEQ_NAME,
            /// <summary>
            /// Test label or text
            /// </summary>
            TEST_LBL,
            /// <summary>
            /// Optional data flag
            /// </summary>
            OPT_FLAG,
            /// <summary>
            /// Average test execution time in seconds
            /// </summary>
            TEST_TIM,
            /// <summary>
            /// Lowest test result value
            /// </summary>
            TEST_MIN,
            /// <summary>
            /// Highest test result value
            /// </summary>
            TEST_MAX,
            /// <summary>
            /// Sum of test result values
            /// </summary>
            TST_SUMS,
            /// <summary>
            /// Sum of squares of test result values
            /// </summary>
            TST_SQRS,
        }

        public TsrRecord()
        {
            optionalDataFlag = new OptionalDataFlagField(this); 

            AddField(FieldName.HEAD_NUM.ToString(), headNumber);
            AddField(FieldName.SITE_NUM.ToString(), siteNumber);
            AddField(FieldName.TEST_TYP.ToString(), testType);
            AddField(FieldName.TEST_NUM.ToString(), testNumber);
            AddField(FieldName.EXEC_CNT.ToString(), executionCount);
            AddField(FieldName.FAIL_CNT.ToString(), failCount);
            AddField(FieldName.ALRM_CNT.ToString(), alarmCount);
            AddField(FieldName.TEST_NAM.ToString(), testName);
            AddField(FieldName.SEQ_NAME.ToString(), sequencerName);
            AddField(FieldName.TEST_LBL.ToString(), testLabel);
            AddField(FieldName.OPT_FLAG.ToString(), optionalDataFlag);
            AddField(FieldName.TEST_TIM.ToString(), executionTime);
            AddField(FieldName.TEST_MIN.ToString(), lowestResultValue);
            AddField(FieldName.TEST_MAX.ToString(), highestResultValue);
            AddField(FieldName.TST_SUMS.ToString(), resultValuesSum);
            AddField(FieldName.TST_SQRS.ToString(), resultValuesSquareSum);
        }
        
        public IField<byte> HeadNumber
        {
            get { return headNumber; }
        }

        public IField<byte> SiteNumber
        {
            get { return siteNumber; }
        }

        public IField<char> TestType
        {
            get { return testType; }
        }

        public IField<uint> TestNumber
        {
            get { return testNumber; }
        }

        public IField<uint> ExecutionCount
        {
            get { return executionCount; }
        }

        public IField<uint> FailCount
        {
            get { return failCount; }
        }

        public IField<uint> AlarmCount
        {
            get { return alarmCount; }
        }

        public IField<string> TestName
        {
            get { return testName; }
        }

        public IField<string> SequencerName
        {
            get { return sequencerName; }
        }

        public IField<string> TestLabel
        {
            get { return testLabel; }
        }

        public IField<DateTime> ExecutionTime
        {
            get { return executionTime; }
        }

        public IField<float> LowestResultValue
        {
            get { return lowestResultValue; }
        }

        public IField<float> HighestResultValue
        {
            get { return highestResultValue; }
        }

        public IField<float> ResultValuesSum
        {
            get { return resultValuesSum; }
        }

        public IField<float> ResultValuesSquareSum
        {
            get { return resultValuesSquareSum; }
        }

        private class OptionalDataFlagField : BitEncoded<TsrRecord>
        {
            public OptionalDataFlagField(TsrRecord record) : base(record)
            {
            }


            /// <summary>
            /// Validate the field's value.
            /// </summary>
            /// <remarks>Each subclasses should override to validate field's value.</remarks>
            protected override void DoValidate()
            {
                if (!EvaluateAnd((byte)OptionalDataFlagBit.LowestResult))
                    ParentRecord.LowestResultValue.Valid = false;
                if (!EvaluateAnd((byte)OptionalDataFlagBit.HighestResult))
                    ParentRecord.HighestResultValue.Valid = false;
                if (!EvaluateAnd((byte)OptionalDataFlagBit.ExecutionTime))
                    ParentRecord.ExecutionTime.Valid = false;
                if (!EvaluateAnd((byte)OptionalDataFlagBit.Sum))
                    ParentRecord.ResultValuesSum.Valid = false;
                if (!EvaluateAnd((byte)OptionalDataFlagBit.SquareSum))
                    ParentRecord.ResultValuesSquareSum.Valid = false;
            }

            [Flags]
            private enum OptionalDataFlagBit : byte
            {
                Unknown = 0x00,
                LowestResult = 0x01, // bit 0
                HighestResult = 0x02, // bit 1
                ExecutionTime = 0x04, // bit 2
                Reserved1 = 0x08, // bit 3
                Sum = 0x10, // bit 4
                SquareSum = 0x20, // bit 5
                Reserved2 = 0x40, // bit 6
                Reserved3 = 0x80, // bit 7
                All = LowestResult | HighestResult | ExecutionTime | Reserved1 | Sum | SquareSum | Reserved2 | Reserved3
            }
        }
    }
}