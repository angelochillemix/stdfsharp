/**
 * $Id: TestChar.cs 21 2008-06-08 07:07:49Z outburst $
 * 
 * STDFSharpTests
 *
 * File: TestChar.cs
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

using KA.StdfSharp.Record.Field;
using NUnit.Framework;

namespace KA.StdfSharp.Tests.Record.Field
{
    [TestFixture]
    public class TestChar
    {
        private IField<char> field = null;

        [SetUp]
        public void Initialize()
        {
            field = new StdfChar();
        }

        [TearDown]
        public void TearDown()
        {
            field = null;
        }

        [Test]
        public void Initialization()
        {
            Assert.AreEqual(' ', field.Value);
            field.Validate();
            Assert.IsTrue(System.Char.IsWhiteSpace(field.Value));
            Assert.IsFalse(field.Valid);
        }

        [Test]
        public void NotValidated()
        {
            field.Value = 'P';
            Assert.IsTrue(field.Valid);
        }
        
        [Test]
        public void Validation()
        {
            field.Value = 'P';
            field.Validate();
            Assert.IsTrue(field.Valid);
        }
        
        [Test]
        public void TestSize()
        {
            Assert.AreEqual(sizeof(char), field.Size);
        }
        
    }
}