///**
// * $Id$
// * 
// * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
// *
// * File: 
// * Description:
// * 
// * Copyright (C) 2006 Outburst <outburst@users.sourceforge.net>
// *  
// * This library is free software; you can redistribute it and/or 
// * modify it under the terms of the GNU Lesser General Public License 
// * as published by the Free Software Foundation; either version 2.1 
// * of the License, or (at your option) any later version.
// * 
// * This library is distributed in the hope that it will be useful, 
// * but WITHOUT ANY WARRANTY; without even the implied warranty of 
// * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// * GNU Lesser General Public License for more details.
// * 
// * You should have received a copy of the GNU Lesser General Public License
// * along with this library; if not, write to the Free Software Foundation, 
// * Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// */

using System;
using System.Collections.Generic;
using System.Diagnostics;

using NUnit.Framework;

using KA.StdfSharp.Record;
using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Tests
{
	
	[TestFixture]
	public class TestStdfRecordUtil
	{
		[SetUp]
		public void SetUp()
		{
		}

		[TearDown]
		public void TearDown()
		{
		}
		
		[Test]
		public void GetFields()
		{
			StdfRecordFactory factory = StdfRecordFactory.Instance;
			StdfRecord record = factory.CreateRecord(0, 20);
			Assert.AreEqual(typeof(AtrRecord), record.GetType());
			IList<Type> fieldTypes = StdfRecordUtil.GetIFieldTypes(record);
			Assert.AreEqual(2, fieldTypes.Count);
			foreach (Type t in fieldTypes)
			{
				Debug.WriteLine(t.ToString());
				Assert.AreEqual(typeof(IField), t.GetInterface(typeof(IField).Name));
			}
		}
	}
}
