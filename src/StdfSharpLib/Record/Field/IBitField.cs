/**
 * $Id: IBitField.cs 6 2006-09-01 17:49:05Z Angelo $
 * 
 * STDFSharp
 *
 * File: IBitField.cs
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

namespace KA.StdfSharp.Record.Field
{
    public interface IBitField : IField<byte>
    {
        /// <summary>
        /// Evaluate the bit AND of the passed byte with the value of the field.
        /// </summary>
        /// <param name="value">The value with which to evaluate.</param>
        /// <returns>true if bit AND evaluate to zero (It means the bit are set to 0), otherwise false.</returns>
        bool EvaluateAnd(byte value);

        /// <summary>
        /// Evaluate the bit AND of the passed byte with the value of the field.
        /// </summary>
        /// <param name="value">The value with which to evaluate.</param>
        /// <returns>true if bit OR not evaluate to zero (It means the bit are set to 1), otherwise false.</returns>
        bool EvaluateOr(byte value);
    }
}