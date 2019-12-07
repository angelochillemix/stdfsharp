/**
 * $Id: Swapper.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
 *
 * File: Swapper.cs
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
	/// Utility class for Endianess bytes swapping. 
	/// This class consider bytes in big endian and convert to little endian.
	/// </summary>
    public static class EndianSwapper
    {
		/// <summary>
		/// Swap a <code>ushort</code>.
		/// </summary>
		/// <param name="us">
		/// A <see cref="System.UInt16"/> which represents the ushort to swap.
		/// </param>
		/// <returns>
		/// A <see cref="System.UInt16"/> which represents the swapped ushort.
		/// </returns>
        public static ushort SwapShort(ushort us)
        {
            return (ushort)(((us & 0x00ff) << 8) + ((us & 0xff00) >> 8));
        }

		/// <summary>
		/// Swaps a <code>uint</code>
		/// </summary>
		/// <param name="ui">
		/// A <see cref="System.UInt32"/> which represents the uint to swap
		/// </param>
		/// <returns>
		/// A <see cref="System.UInt32"/> which represents the swapped uint
		/// </returns>
        public static uint SwapInt32(uint ui)
        {
            return (((ui & 0x000000ff) << 24) + ((ui & 0x0000ff00) << 8) +
                    ((ui & 0x00ff0000) >> 8) + ((ui & 0xff000000) >> 24));
        }

		/// <summary>
		/// Swaps a <code>ulong</code>
		/// </summary>
		/// <param name="ul">
		/// A <see cref="System.UInt64"/> which represents the ulong to swap.
		/// </param>
		/// <returns>
		/// A <see cref="UInt64"/> which represents the swapped ulong.
		/// </returns>
        public static UInt64 SwapLong(ulong ul)
        {
            return ((((ul) & 0xff00000000000000) >> 56) |
                    (((ul) & 0x00ff000000000000) >> 40) |
                    (((ul) & 0x0000ff0000000000) >> 24) |
                    (((ul) & 0x000000ff00000000) >> 8) |
                    (((ul) & 0x00000000ff000000) << 8) |
                    (((ul) & 0x0000000000ff0000) << 24) |
                    (((ul) & 0x000000000000ff00) << 40) |
                    (((ul) & 0x00000000000000ff) << 56));
        }

		/// <summary>
		/// Swaps a float
		/// </summary>
		/// <param name="ul">
		/// A <see cref="System.UInt64"/> which represents the float to swap.
		/// </param>
		/// <returns>
		/// A <see cref="System.Single"/> which represents the float swapped.
		/// </returns>
        public static float SwapFloat(ulong ul)
        {
            return SwapLong(ul);
        }
    }
}