using System;

namespace KA.StdfSharp.Record
{
    public interface IStdfRecordFactory
    {
        /// <summary>
        /// Creates a STDF record instance of the specified <code>type</code> and <code>subtype</code>.
        /// </summary>
        /// <param name="type">The type of record to create.</param>
        /// <param name="subtype">The subtype of record to create.</param>
        /// <returns>A record of specified type and subtype.</returns>
        /// <exception cref="RecordNotFoundException">If the record was not found.</exception>
        /// <exception cref="StdfException">If the record cannot be created.</exception>
        StdfRecord CreateRecord(byte type, byte subtype);

        /// <summary>
        /// Creates a record from its type.
        /// </summary>
        /// <param name="recordType">The type of the record to create.</param>
        /// <returns>The created record.</returns>
        StdfRecord CreateRecord(Type recordType);

        /// <summary>
        /// Registers a record to be created.
        /// </summary>
        /// <param name="type">The type of the record</param>
        /// <param name="subtype">The subtype of the record</param>
        /// <param name="record">The <code>Type</code> of the record</param>
        void RegisterRecord(byte type, byte subtype, Type record);
    }
}