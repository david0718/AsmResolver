﻿// AsmResolver - Executable file format inspection library 
// Copyright (C) 2016-2019 Washi
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3.0 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

using System;
using System.Collections;
using System.Collections.Generic;

namespace AsmResolver.PE.DotNet.Metadata.Tables.Rows
{
    /// <summary>
    /// Represents a single row in the event pointer metadata table.
    /// </summary>
    public readonly struct EventPointerRow : IMetadataRow
    {
        /// <summary>
        /// Reads a single event pointer row from an input stream.
        /// </summary>
        /// <param name="reader">The input stream.</param>
        /// <param name="layout">The layout of the event pointer table.</param>
        /// <returns>The row.</returns>
        public static EventPointerRow FromReader(IBinaryStreamReader reader, TableLayout layout)
        {
            return new EventPointerRow(reader.ReadIndex((IndexSize) layout.Columns[0].Size));
        }
        
        /// <summary>
        /// Creates a new row for the event pointer metadata table.
        /// </summary>
        /// <param name="event">The index into the Event table that this pointer references.</param>
        public EventPointerRow(uint @event)
        {
            Event = @event;
        }

        /// <inheritdoc />
        public TableIndex TableIndex => TableIndex.EventPtr;

        /// <inheritdoc />
        public int Count => 1;

        /// <inheritdoc />
        public uint this[int index] => index switch
        {
            0 => Event,
            _ => throw new IndexOutOfRangeException()
        };

        /// <summary>
        /// Gets an index into the Event table that this pointer references.
        /// </summary>
        public uint Event
        {
            get;
        }
        
        /// <inheritdoc />
        public void Write(IBinaryStreamWriter writer, TableLayout layout)
        {
            writer.WriteIndex(Event,(IndexSize) layout.Columns[0].Size);
        }

        /// <summary>
        /// Determines whether this row is considered equal to the provided event pointer row.
        /// </summary>
        /// <param name="other">The other row.</param>
        /// <returns><c>true</c> if the rows are equal, <c>false</c> otherwise.</returns>
        public bool Equals(EventPointerRow other)
        {
            return Event == other.Event;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is EventPointerRow other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int) Event;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"({Event:X8})";
        }

        /// <inheritdoc />
        public IEnumerator<uint> GetEnumerator()
        {
            return new MetadataRowColumnEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}