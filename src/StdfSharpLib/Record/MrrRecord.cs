/**
 * $Id: MrrRecord.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: MrrRecord.cs
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
    /// Represents the MRR record of STDF
    /// </summary>
    [StdfRecord(1, 20)]
    public sealed class MrrRecord : StdfRecord
    {
        private IField<DateTime>    finishTime          = new StdfDate(); // FINISH_T U*4 Date and time last part tested
        private IField<char[]>      dispositionCode     = new FixedLengthCharacter(1); // DISP_COD C*1 Lot disposition code 
        private IField<string>      userLotDescription  = new StdfString(); // USR_DESC C*n Lot description supplied by user length byte = 0
        private IField<string>      lotDescription      = new StdfString(); // EXC_DESC C*n Lot description supplied by exec length byte = 0        

        public MrrRecord()
        {
            AddField("FINISH_T", finishTime);
            AddField("DISP_COD", dispositionCode);
            AddField("USR_DESC", userLotDescription);
            AddField("EXC_DESC", lotDescription);
        }
        
        public IField<DateTime> FinishTime
        {
            get { return finishTime; }
        }

        public IField<char[]> DispositionCode
        {
            get { return dispositionCode; }
        }

        public IField<string> UserLotDescription
        {
            get { return userLotDescription; }
        }

        public IField<string> LotDescription
        {
            get { return lotDescription; }
        }
    }
}