/**
 * $Id: PtrRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: PtrRecord.cs
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
    /// Represents the PTR record of STDF.
    /// </summary>
    [StdfRecordAttribute(15, 10)]
    public class PtrRecord : StdfRecord
    {
        private IField<uint>    testNumber              = new StdfUInt(); // TEST_NUM U*4 Test number
        private IField<byte>    headNumber              = new StdfUByte(); // HEAD_NUM U*1 Test head number
        private IField<byte>    siteNumber              = new StdfUByte(); // SITE_NUM U*1 Test site number
        private IBitField       testFlags               = null; // TEST_FLG B*1 Test flags (fail, alarm, etc.)
        private IBitField       parametricFlags         = null; // PARM_FLG B*1 Parametric test flags (drift, etc.)
        private IField<float>   result                  = new StdfFloat(); // RESULT R*4 Test result
        private IField<string>  testLabel               = new StdfString(); // TEST_TXT C*n Test description text or label
        private IField<string>  alarmName               = new StdfString(); // ALARM_ID C*n Name of alarm
        private IBitField       optionalFlags            = null; // OPT_FLAG B*1 Optional data flag
        private IField<byte>    resultScale             = new StdfUByte(); // RES_SCAL I*1 Test results scaling exponent
        private IField<byte>    lowLimitScale           = new StdfUByte(); // LLM_SCAL I*1 Low limit scaling exponent
        private IField<byte>    highLimitScale          = new StdfUByte(); // HLM_SCAL I*1 High limit scaling exponent
        private IField<float>   lowLimit                = new StdfFloat(); // LO_LIMIT R*4 Low test limit value
        private IField<float>   highLimit               = new StdfFloat(); // HI_LIMIT R*4 High test limit value
        private IField<string>  units                   = new StdfString(); // UNITS C*n Test units
        private IField<string>  resultFormat            = new StdfString(); // C_RESFMT C*n ANSI C result format string
        private IField<string>  lowLimitFormat          = new StdfString(); // C_LLMFMT C*n ANSI C low limit format string
        private IField<string>  highLimitFormat         = new StdfString(); // C_HLMFMT C*n ANSI C high limit format string
        private IField<float>   lowSpecificationLimit   = new StdfFloat(); // LO_SPEC R*4 Low specification limit value
        private IField<float>   highSpecificationLimit  = new StdfFloat(); // HI_SPEC R*4 High specification limit value
        
        public enum FieldName
        {
            /// <summary>
            /// Test number
            /// </summary>
            TEST_NUM,
            /// <summary>
            /// Test head number
            /// </summary>
            HEAD_NUM,
            /// <summary>
            /// Test site number
            /// </summary>
            SITE_NUM,
            /// <summary>
            /// Test flags (fail, alarm, etc.)
            /// </summary>
            TEST_FLG,
            /// <summary>
            /// Parametric test flags (drift, etc.)
            /// </summary>
            PARM_FLG,
            /// <summary>
            /// Test result
            /// </summary>
            RESULT,
            /// <summary>
            /// Test description text or label
            /// </summary>
            TEST_TXT,
            /// <summary>
            /// Name of alarm
            /// </summary>
            ALARM_ID,
            /// <summary>
            /// Optional data flag
            /// </summary>
            OPT_FLAG,
            /// <summary>
            /// Test results scaling exponent
            /// </summary>
            RES_SCAL,
            /// <summary>
            /// Low limit scaling exponent
            /// </summary>
            LLM_SCAL,
            /// <summary>
            /// High limit scaling exponent
            /// </summary>
            HLM_SCAL,
            /// <summary>
            /// Low test limit value
            /// </summary>
            LO_LIMIT,
            /// <summary>
            /// High test limit value
            /// </summary>
            HI_LIMIT,
            /// <summary>
            /// Test units
            /// </summary>
            UNITS,
            /// <summary>
            /// ANSI C result format string
            /// </summary>
            C_RESFMT,
            /// <summary>
            /// ANSI C low limit format string
            /// </summary>
            C_LLMFMT,
            /// <summary>
            /// ANSI C high limit format string
            /// </summary>
            C_HLMFMT,
            /// <summary>
            /// Low specification limit value
            /// </summary>
            LO_SPEC,
            /// <summary>
            /// High specification limit value
            /// </summary>
            HI_SPEC
        }
        
        public PtrRecord()
        {
            testFlags = new TestFlagsField(this);
            parametricFlags = new ParametricFlagsField(this);
            optionalFlags = new OptionalFlagsField(this);

            AddField(FieldName.TEST_NUM.ToString(), testNumber);
            AddField(FieldName.HEAD_NUM.ToString(), headNumber);
            AddField(FieldName.SITE_NUM.ToString(), siteNumber);
            AddField(FieldName.TEST_FLG.ToString(), testFlags);
            AddField(FieldName.PARM_FLG.ToString(), parametricFlags);
            AddField(FieldName.RESULT.ToString(), result);
            AddField(FieldName.TEST_TXT.ToString(), testLabel);
            AddField(FieldName.ALARM_ID.ToString(), alarmName);
            AddField(FieldName.OPT_FLAG.ToString(), optionalFlags);
            AddField(FieldName.RES_SCAL.ToString(), resultScale);
            AddField(FieldName.LLM_SCAL.ToString(), lowLimitScale);
            AddField(FieldName.HLM_SCAL.ToString(), highLimitScale);
            AddField(FieldName.LO_LIMIT.ToString(), lowLimit);
            AddField(FieldName.HI_LIMIT.ToString(), highLimit);
            AddField(FieldName.UNITS.ToString(), units);
            AddField(FieldName.C_RESFMT.ToString(), resultFormat);
            AddField(FieldName.C_LLMFMT.ToString(), lowLimitFormat);
            AddField(FieldName.C_HLMFMT.ToString(), highLimitFormat);
            AddField(FieldName.LO_SPEC.ToString(), lowSpecificationLimit);
            AddField(FieldName.HI_SPEC.ToString(), highSpecificationLimit);
        }

        public class TestFlagsField : BitEncoded<PtrRecord>
        {
            public TestFlagsField(PtrRecord record) : base(record)
            {
            }

            protected override void DoValidate()
            {
                if (!EvaluateAnd((byte)TestFlagFieldBit.TestResult))
                    ParentRecord[FieldName.RESULT.ToString()].Valid = false;
            }

            public bool AlarmDetected
            {
                get
                {
                    return !EvaluateAnd((byte)TestFlagFieldBit.AlarmDetected);
                }
            }

            public bool TestResultUnreliable
            {
                get
                {
                    return !EvaluateAnd((byte)TestFlagFieldBit.TestResultUnreliable);
                }
            }

            public bool TimeoutOccurred
            {
                get
                {
                    return !EvaluateAnd((byte)TestFlagFieldBit.TimeoutOccurred);
                }
            }

            public bool TestExecuted
            {
                get
                {
                    return EvaluateAnd((byte)TestFlagFieldBit.TestNotExecuted);
                }
            }

            public bool TestAborted
            {
                get
                {
                    return !EvaluateAnd((byte)TestFlagFieldBit.TestAborted);
                }
            }

            public bool PassFailFlagValid
            {
                get
                {
                    return EvaluateAnd((byte)TestFlagFieldBit.NoPassFailFlag);
                }
            }

            public bool TestFailed
            {
                get
                {
                    return !EvaluateAnd((byte)TestFlagFieldBit.TestFailed);
                }
            }

            [Flags]
            private enum TestFlagFieldBit
            {
                Unknown = 0x00,
                AlarmDetected = 0x01,
                TestResult = 0x02,
                TestResultUnreliable = 0x04,
                TimeoutOccurred = 0x08,
                TestNotExecuted = 0x10,
                TestAborted = 0x20,
                NoPassFailFlag= 0x40,
                TestFailed = 0x80
            }
        }

        public class ParametricFlagsField : BitEncoded<PtrRecord>
        {
            public ParametricFlagsField(PtrRecord record) : base(record)
            {
            }
        }

        public class OptionalFlagsField : BitEncoded<PtrRecord>
        {
            public OptionalFlagsField(PtrRecord record) : base(record)
            {
            }

            protected override void DoValidate()
            {
                if (!EvaluateAnd((byte)OptionalFlagFieldBit.ResultScale))
                    ParentRecord[FieldName.RES_SCAL.ToString()].Valid = false;
                if (!EvaluateAnd((byte)OptionalFlagFieldBit.NoLowSpecificationLimit))
                    ParentRecord[FieldName.LO_SPEC.ToString()].Valid = false;
                if (!EvaluateAnd((byte)OptionalFlagFieldBit.NoHighSpecificationLimit))
                    ParentRecord[FieldName.HI_SPEC.ToString()].Valid = false;
                if ((!EvaluateAnd((byte)OptionalFlagFieldBit.LowLimit)) || (!EvaluateAnd((byte)OptionalFlagFieldBit.NoLowLimit)))
                {
                    ParentRecord[FieldName.LO_LIMIT.ToString()].Valid = false;
                    ParentRecord[FieldName.LLM_SCAL.ToString()].Valid = false;
                }
                if ((!EvaluateAnd((byte)OptionalFlagFieldBit.HighLimit)) || (!EvaluateAnd((byte)OptionalFlagFieldBit.NoHighLimit)))
                {
                    ParentRecord[FieldName.HI_LIMIT.ToString()].Valid = false;
                    ParentRecord[FieldName.HLM_SCAL.ToString()].Valid = false;
                }
            }

            [Flags]
            public enum OptionalFlagFieldBit
            {
                Unknown = 0x00,
                ResultScale = 0x01,
                Reserved = 0x02,
                NoLowSpecificationLimit = 0x04,
                NoHighSpecificationLimit = 0x08,
                LowLimit = 0x10,
                HighLimit = 0x20,
                NoLowLimit = 0x40,
                NoHighLimit = 0x80
            }
        }

        public OptionalFlagsField OptionalFlag
        {
            get { return optionalFlags as OptionalFlagsField; }
        }

        public TestFlagsField TestFlags
        {
            get { return testFlags as TestFlagsField; }
        }
    }
}