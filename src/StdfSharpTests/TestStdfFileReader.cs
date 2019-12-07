/**
 * $Id: TestStdfFileReader.cs 21 2008-06-08 07:07:49Z outburst $
 * 
 * STDFSharpTests
 *
 * File: TestStdfFileReader.cs
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

namespace KA.StdfSharp.Tests
{
    [TestFixture]
    public class TestStdfFileReader
    {
        private static readonly string filePath = Path.GetFullPath(@"../../../../../../stdf/stdf_test.std");
        private static readonly string tmpFilePath = Path.GetFullPath(@"../../../../../stdf/tmp.std");
        
        [SetUp]
        public void Init()
        {
            if (File.Exists(tmpFilePath))
                File.Delete(tmpFilePath);
            Assert.IsFalse(File.Exists(tmpFilePath));
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(tmpFilePath))
                File.Delete(tmpFilePath);
        }
        
        [Test]
        public void RegisterDelegate()
        {
            try
            {
                using (FileStream stream = File.OpenRead(filePath))
                {
                    using (StdfFileReader reader = new StdfFileReader(stream))
                    {
                        reader.RegisterDelegate(0, 10, delegate(object o, RecordReadEventArgs e)
                                                           {
                                                               Assert.IsInstanceOf(typeof(FarRecord), (e.Record));
                                                           });
                        reader.RegisterDelegate(0, 20, delegate(object o, RecordReadEventArgs e)
                                                           {
                                                               Assert.IsInstanceOf(typeof(AtrRecord), e.Record);
                                                           });
                        reader.RegisterDelegate(1, 20, delegate(object o, RecordReadEventArgs e)
                                                           {
                                                               Assert.IsInstanceOf(typeof(MrrRecord), e.Record);
                                                           });
                        reader.Read();
                    }
                }
            }
            catch (StdfException e)
            {
                Assert.Fail(e.ToString());
            }
        }
        
        [Test]
        public void ReadFileWithStreamOpenedForWriting()
        {
            using (FileStream stream = File.OpenWrite(tmpFilePath))
            {
                using (new StdfFileReader(stream))
                {
                    Assert.Fail("A StdfException should be thrown because the passed stream is not readable.");
                }
            }
            //Assert.That(() => new StdfFileReader(File.OpenWrite(tmpFilePath)), Throws.TypeOf<StdfException>);
        }

        [Test]
        public void ReadFileThroughNullStream()
        {
            using (StdfFileReader reader = new StdfFileReader(null))
            {
                Assert.Fail("A StdfException should be thrown because the passed stream is not readable.");
            }
            //Assert.That(() => new StdfFileReader(null), Throws.TypeOf<StdfException>);
        }

        [Test]
        [Category("Profile")]
		[Ignore("Not to be run")]
        public void SkippingUnknownRecords()
        {
            StdfFile file = new StdfFile(filePath);
            using (StdfFileReader reader = file.OpenForRead())
            {
                reader.RecordRead += new RecordReadEventHandler(delegate(object o, RecordReadEventArgs e)
                                                           {
                                                               Assert.IsNotNull(e.Record);
                                                           });
                try
                {
                    reader.Read();
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }
            }
        }
    }
}