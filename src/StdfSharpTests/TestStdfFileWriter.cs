/**
 * $Id: TestStdfFileWriter.cs 11 2006-10-20 21:11:16Z outburst $
 * 
 * STDFSharpTests
 *
 * File: MockStdfFileWriter.cs
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
using System.Collections.Generic;
using System.IO;
using KA.StdfSharp.Record;
using NUnit.Framework;

namespace KA.StdfSharp.Tests
{
    [TestFixture]
    public class TestStdfFileWriter
    {
        private const string filePath = @"std.tmp";

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        
        [Test]
        public void WriteFarRecord()
        {
            FarRecord far = CreateTestFarRecord();
            MemoryStream stream = new MemoryStream(4);
            StdfFileWriter writer = new StdfFileWriter(stream);
            writer.WriteRecord(far);
            stream.Position = 0;
            ReadRecord(typeof(FarRecord), stream);
            writer.Dispose();
        }

        [Test]
        public void WriteMoreRecords()
        {
            MemoryStream stream = new MemoryStream(4);
            StdfFileWriter writer = new StdfFileWriter(stream);
            writer.WriteRecord(CreateTestFarRecord());
            writer.WriteRecord(CreateTestAtrRecord());
            writer.WriteRecord(CreateTestMrrRecord());
            stream.Position = 0;
            List<Type> recordTypeList = new List<Type>(3);
            recordTypeList.Add(typeof (FarRecord));
            recordTypeList.Add(typeof(AtrRecord));
            recordTypeList.Add(typeof (MrrRecord));
            ReadRecords(recordTypeList, stream);
            writer.Dispose();
        }
        
        private struct RecordInfo
        {
            private byte type;
            private byte subtype;
            
            public RecordInfo(byte type, byte  subtype)
            {
                this.type = type;
                this.subtype = subtype;
            }

            public byte Type
            {
                get { return type; }
            }

            public byte Subtype
            {
                get { return subtype; }
            }
        }

        private void ReadRecord(Type recordType, Stream stream)
        {
            StdfRecordAttribute attribute = StdfRecord.GetStdfRecordAttribute(recordType);
            ReadRecords(new RecordInfo[] { new RecordInfo(attribute.Type, attribute.Subtype) }, stream);
        }

        private void ReadRecords(List<Type> records, Stream stream)
        {

            StdfFileReader reader = null;
            try
            {
                reader = new StdfFileReader(stream);
                foreach (Type recordType in records)
                {
                    StdfRecord record = reader.ReadRecord();
                    Assert.IsInstanceOf(recordType, record);
                }
            }
            catch (StdfException e)
            {
                Console.WriteLine(e.StackTrace);
                Assert.Fail(e.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        private void ReadRecords(RecordInfo[] info, Stream stream)
        {
            StdfFileReader reader = null;
            try
            {
                reader = new StdfFileReader(stream);
                foreach (RecordInfo stdfInfo in info)
                {
                    reader.RegisterDelegate(stdfInfo.Type, stdfInfo.Subtype, delegate(object o, RecordReadEventArgs e)
                    {
                        StdfRecord record = e.Record;
                        Assert.AreEqual(stdfInfo.Type, record.Type);
                        Assert.AreEqual(stdfInfo.Subtype, record.Subtype);
                    });
                }
                reader.Read();
            }
            catch (StdfException e)
            {
                Console.WriteLine(e.StackTrace);
                Assert.Fail(e.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        private FarRecord CreateTestFarRecord()
        {
            FarRecord far = (FarRecord)StdfRecordFactory.Instance.CreateRecord(typeof(FarRecord));
            far.CpuType.Value = 2;
            far.Version.Value = 4;
            return far;
        }

        private AtrRecord CreateTestAtrRecord()
        {
            AtrRecord atr = (AtrRecord)StdfRecordFactory.Instance.CreateRecord(typeof(AtrRecord));
            atr.ModificationDate.Value = DateTime.Now;
            atr.ProgramCommandLine.Value = "test_command_line";
            return atr;
        }

        private MrrRecord CreateTestMrrRecord()
        {
            MrrRecord mrr = (MrrRecord)StdfRecordFactory.Instance.CreateRecord(typeof(MrrRecord));
            mrr.FinishTime.Value = DateTime.Now;
            mrr.DispositionCode.Value = new char[] { ' ' };
            mrr.LotDescription.Value = "lot_description";
            mrr.UserLotDescription.Value = "user_lot_description";
            return mrr;
        }
    }
}