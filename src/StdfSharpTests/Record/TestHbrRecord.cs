/**
 * $Id: TestHbrRecord.cs 21 2008-06-08 07:07:49Z outburst $
 * 
 * STDFSharpTests
 *
 * File: TestHbrRecord.cs
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

using KA.StdfSharp.Record;
using KA.StdfSharp.Tests.Mock;
using NUnit.Framework;

namespace KA.StdfSharp.Tests.Record
{
    [TestFixture]
    public class TestHbrRecord
    {
        private HbrRecord hbr = null;

        [SetUp]
        public void Initialize()
        {
            hbr = new HbrRecord();
        }

        [TearDown]
        public void TearDown()
        {
            hbr.Clear();
            hbr = null;
        }
        
        [Test]
        public void TestType()
        {
            Assert.IsTrue(hbr.BinType.Equals(BinType.Hardware));
        }
        
        [Test]
        public void TestWriting()
        {
            InitializeTestRecord();
            MockStdfFileWriter writer = new MockStdfFileWriter(CpuType.Sun386);
            writer.WriteRecord(hbr);
            writer.Reset();
            StdfFileReader reader = new StdfFileReader(writer.Stream);
            StdfRecord record = reader.ReadRecord();
            Assert.IsInstanceOf(typeof(FarRecord), record);
            record = reader.ReadRecord();
            Assert.IsInstanceOf(typeof(HbrRecord), record);
            HbrRecord readRecord = record as HbrRecord;
            Assert.IsNotNull(readRecord);
            Assert.AreEqual(hbr.HeadNumber, readRecord.HeadNumber);
            Assert.AreEqual(hbr.SiteNumber, readRecord.SiteNumber);
            Assert.AreEqual(hbr.Name, readRecord.Name);
            Assert.AreEqual(hbr.Number, readRecord.Number);
            Assert.AreEqual(hbr.PartsCount, readRecord.PartsCount);
            Assert.AreEqual(hbr.PassFail, readRecord.PassFail);
        }

        private void InitializeTestRecord()
        {
            hbr.HeadNumber.Value = 1;
            hbr.SiteNumber.Value = 1;
            hbr.Name.Value = "Bin name";
            hbr.PartsCount.Value = 10;
            hbr.PassFail.Value = HbrRecord.RawPassedStatus;
            hbr.Number.Value = 1;
        }

        [Test]
        public void TestStatus()
        {
            hbr.PassFail.Value = HbrRecord.RawPassedStatus;
            Assert.AreEqual(hbr.Status, BinStatus.Passed);

            hbr.PassFail.Value = HbrRecord.RawFailStatus;
            Assert.AreEqual(hbr.Status, BinStatus.Fail);

            hbr.PassFail.Value = HbrRecord.RawUnknownStatus;
            Assert.AreEqual(hbr.Status, BinStatus.Unknown);

            hbr.Status = BinStatus.Passed;
            Assert.AreEqual(hbr.PassFail.Value, HbrRecord.RawPassedStatus);

            hbr.Status = BinStatus.Fail;
            Assert.AreEqual(hbr.PassFail.Value, HbrRecord.RawFailStatus);

            hbr.Status = BinStatus.Unknown;
            Assert.AreEqual(hbr.PassFail.Value, HbrRecord.RawUnknownStatus);
        }
    }
}