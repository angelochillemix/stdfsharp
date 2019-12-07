/**
 * $Id: TestString.cs 21 2008-06-08 07:07:49Z outburst $
 * 
 * STDFSharpTests
 *
 * File: TestString.cs
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
using KA.StdfSharp.Record.Field;
using NUnit.Framework;

namespace KA.StdfSharp.Tests.Record.Field
{
    [TestFixture]
    public class TestString
    {
        private const string TestValue = "value";

        protected IField<string> field = null;

        [SetUp]
        public void Initialize()
        {
            field = new StdfString();
        }

        [TearDown]
        public void TearDown()
        {
            field = null;
        }
        
        [Test]
        public void Initialization()
        {
            Assert.AreEqual(string.Empty, field.Value);
            Assert.AreEqual(0, field.Value.Length);
            field.Validate();
            Assert.IsFalse(field.Valid);
        }
        
        [Test]
        public void Validation()
        {
            field.Value = TestValue;
            field.Validate();
            Assert.IsTrue(field.Valid);
        }
        
        [Test]
        public void WritingReading()
        {
            Stream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            field.Value = TestValue;
            field.Write(writer);
            writer.Flush();
            stream.Position = 0;
            BinaryReader reader = new BinaryReader(stream);
            IField<string> newField = new StdfString();
            newField.Read(reader);
            Assert.AreEqual(field, newField);
        }

        [Test]
        public void WritingEmptyValue()
        {
            Stream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            field.Value = string.Empty;
            field.Write(writer);
            writer.Flush();
            stream.Position = 0;
            BinaryReader reader = new BinaryReader(stream);
            IField<string> newField = new StdfString();
            newField.Read(reader);
            Assert.AreEqual(field, newField);
        }

        [Test]
        public void SetValue()
        {
            field.Value = TestValue;
            Assert.AreEqual(TestValue, field.Value);
        }
        
        [Test]
        public void TestSize()
        {
            Assert.AreEqual(1, field.Size);
            field.Value = TestValue;
            Assert.AreEqual(TestValue.Length + 1, field.Size);
        }

        [Test]
        public void SetNullValue()
        {
            field.Value = null;
            Assert.That(field.Value == null, Throws.TypeOf<ArgumentNullException>());
        }
    }
}