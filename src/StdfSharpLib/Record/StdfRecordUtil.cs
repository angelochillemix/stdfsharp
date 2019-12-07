/**
 * $Id$
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: StdfRecordUtil
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
using System.Reflection;
using KA.StdfSharp.Record.Field;

namespace KA.StdfSharp.Record
{
  public static class StdfRecordUtil 
  {
        public static IList<Type> GetIFieldTypes(StdfRecord record)
        {
            FieldInfo[] fields = record.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            IList<Type> fieldList = new List<Type>();
            foreach (FieldInfo info in fields)
            {
                Type[] interfacesTypes = info.FieldType.GetInterfaces();
                IList<Type> interfacesList = new List<Type>(interfacesTypes);
                if (interfacesList.Contains(typeof(IField)))
                    fieldList.Add(info.FieldType);
            }
            return fieldList;
        }
  }
  
}