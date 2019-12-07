/**
 * $Id: TestPrrRecord.cs 9 2006-09-21 22:13:20Z outburst $
 * 
 * STDFSharpTests
 *
 * File: TestPrrRecord.cs
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
using System.IO;
using KA.StdfSharp.Record;
using NUnit.Framework;

namespace KA.StdfSharp.Tests.Record
{
    [TestFixture]
    public class TestPrrRecord
    {
        private PrrRecord prr = null;
        
        [SetUp]
        public void Initialize()
        {
            prr = new PrrRecord();
        }
        
        [TearDown]
        public void TearDown()
        {
            prr = null;
        }

        [Ignore("To complete")]
        public void TestConstructor()
        {
            Stream recordStream = new MemoryStream();
            prr.ElapsedTestTime.Value = DateTime.Now;
            prr.HardwareBin.Value = 0;
            prr.HeadNumber.Value = 1;
            prr.SiteNumber.Value = 1;
            prr.PartDescription.Value = "description";
            prr.PartFlag.Value = Convert.ToByte("00000000", 2);
            prr.PartIdentification.Value = "identification";
            prr.PartRepairInformation.Value = new byte[0];
            prr.SoftwareBin.Value = 10;
            prr.TestExecutedCount.Value = 100;
            prr.XCoordinate.Value = 10;
            prr.YCoordinate.Value = 10;
            StdfFileWriter writer = new StdfFileWriter(recordStream);
            writer.WriteRecord(prr);
            // TODO To complete.
        }
        
        [Test]
        public void FlagsValidation()
        {
            prr.PartFlag.Value = Convert.ToByte("00011111", 2);
            Assert.IsTrue(prr.SupersedePartIdSequence);
            Assert.IsTrue(prr.SupersedeCoordinateSequence);
            Assert.IsFalse(prr.TestNormallyCompleted);
            Assert.IsFalse(prr.PartPassed);
            Assert.IsFalse(prr.PassFailFlagValid);

            prr.PartFlag.Value = Convert.ToByte("00000000", 2);
            Assert.IsFalse(prr.SupersedePartIdSequence);
            Assert.IsFalse(prr.SupersedeCoordinateSequence);
            Assert.IsTrue(prr.TestNormallyCompleted);
            Assert.IsTrue(prr.PartPassed);
            Assert.IsTrue(prr.PassFailFlagValid);
        }
        
        [Test]
        public void PartFlagFieldValid1()
        {
            prr.PartFlag.Value = Convert.ToByte("00000000", 2);
            prr.PartFlag.Validate();
            Assert.IsTrue(prr.PartFlag.Valid);
        }

        [Test]
        public void PartFlagFieldValid2()
        {
            prr.PartFlag.Value = Convert.ToByte("00000001", 2);
            prr.PartFlag.Validate();
            Assert.IsTrue(prr.PartFlag.Valid);
        }

        [Test]
        public void PartFlagFieldValid3()
        {
            prr.PartFlag.Value = Convert.ToByte("00000010", 2);
            prr.PartFlag.Validate();
            Assert.IsTrue(prr.PartFlag.Valid);
        }

        [Test]
        public void PartFlagFieldValid4()
        {
            prr.PartFlag.Value = Convert.ToByte("00000011", 2);
            prr.PartFlag.Validate();
            Assert.IsTrue(prr.SupersedePartIdSequence);
            Assert.IsTrue(prr.SupersedeCoordinateSequence);
            Assert.IsFalse(prr.PartFlag.Valid);
        }
    }
}