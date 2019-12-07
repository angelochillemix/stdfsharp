/**
 * $Id: TestStdfFile.cs 21 2008-06-08 07:07:49Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: TestStdfFile.cs
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

using System.IO;
using NUnit.Framework;

namespace KA.StdfSharp.Tests
{
    [TestFixture]
    public class TestStdfFile
    {
        private static readonly string filePath = Path.GetFullPath(@"../../../../../../stdf/stdf_test.std");
		
		private StdfFile file;
        
        [SetUp]
        public void Init()
        {
            file = new StdfFile(filePath);
        }
        
        [Test]
        public void ReadAndWriteOpening()
        {
            int i = 0;
            while (i < 10)
            {
                using (file.OpenForRead())
                {
                }
                using (file.OpenForWrite())
                {
                }
                i++;
            }

            i = 0;
            while (i < 10)
            {
                using (file.OpenForWrite())
                {
                }
                using (file.OpenForRead())
                {
                }
                i++;
            }
        }
        
        [Test]
        public void OpenFileForWritingAfterOpenedForReading()
        {
            file.OpenForRead();
            Assert.Throws<IOException>(() => file.OpenForWrite());
        }

        [Test]
        public void OpenFileForReadingAfterOpenedForWriting()
        {
            file.OpenForWrite();
            file.OpenForRead();
            Assert.Throws<IOException>(() => file.OpenForRead());
        }

        [Test]
        public void OpenFileSeveralTimesDisposingReader()
        {
            for (int i = 0; i < 100; i++)
            {
                StdfFileReader reader = file.OpenForRead();
                reader.Dispose();
            }
        }

        /// <summary>
        /// Verify that the two properties are correctly read.
        /// </summary>
        [Test]
        public void ReadFarRecord()
        {
            Assert.AreEqual(StdfVersion.V4, file.Version);
            Assert.AreEqual(CpuType.Sun, file.Cpu.Type);
        }
    }
}
