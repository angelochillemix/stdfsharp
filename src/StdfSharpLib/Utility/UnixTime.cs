/**
 * $Id: UnixTime.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: UnixTime.cs
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

namespace KA.StdfSharp.Utility
{
    /// <summary>
    /// Utility class for Unix time management in .NET framework
    /// </summary>
    public static class UnixTime
    {
        private static readonly DateTime UnixBaseTime = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Converts a unix timestamp to a <code>DateTime</code>.
        /// </summary>
        /// <param name="unixTimeStamp">Represents the unix timestamp</param>
        /// <returns>A <code>DateTime</code> obtained from the unix timestamp</returns>
        public static DateTime ToDateTime(long unixTimeStamp)
        {
            return UnixBaseTime.AddSeconds(unixTimeStamp);
        }

        /// <summary>
        /// Converts a <code>DateTime</code> to a unix timestamp
        /// </summary>
        /// <param name="date">The <code>DateTime</code> to convert</param>
        /// <returns>The unix timestamp obtained from the DateTime</returns>
        public static long FromDateTime(DateTime date)
        {
            return Convert.ToInt64((date - UnixBaseTime).TotalSeconds);
        }
    }
}