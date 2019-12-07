/**
 * $Id: BitEncoded.cs 20 2008-06-08 07:05:10Z outburst $
 * 
 * STDFSharp
 *
 * File: BitEncoded.cs
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
    public class BitEncoded<R> : StdfUByte, IBitField where R : StdfRecord
    {
        private R record;

        public BitEncoded()
        {
            record = default(R);
        }
        
        public BitEncoded(R record)
        {
            this.record = record;
        }

        /// <summary>
        /// Returns the parent record of this field.
        /// </summary>
        protected R ParentRecord
        {
            get { return record; }
        }
        
        /// <summary>
        /// Evaluate the bit AND of the passed byte with the value of the field.
        /// </summary>
        /// <param name="value">The value with which to evaluate.</param>
        /// <returns>true if bit AND evaluate to zero (It means the bit are set to 0), otherwise false.</returns>
        public bool EvaluateAnd(byte value)
        {
            return (Value & value) == 0;
        }

        /// <summary>
        /// Evaluate the bit OR of the passed byte with the value of the field.
        /// </summary>
        /// <param name="value">The value with which to evaluate.</param>
        /// <returns>true if bit OR not evaluate to zero (It means the bit are set to 1), otherwise false.</returns>
        public bool EvaluateOr(byte value)
        {
            return (Value | value) != 0;
        }
    }
}