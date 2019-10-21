// AsmResolver - Executable file format inspection library 
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

namespace AsmResolver.PE.DotNet.Metadata.Tables.Rows
{
    /// <summary>
    /// Represents a single row in the parameter definition metadata table.
    /// </summary>
    public readonly struct ParameterDefinitionRow : IMetadataRow
    {
        /// <summary>
        /// Reads a single parameter definition row from an input stream.
        /// </summary>
        /// <param name="reader">The input stream.</param>
        /// <param name="layout">The layout of the parameter definition table.</param>
        /// <returns>The row.</returns>
        public static ParameterDefinitionRow FromReader(IBinaryStreamReader reader, TableLayout layout)
        {
            return new ParameterDefinitionRow(
                (ParameterAttributes) reader.ReadUInt16(),
                reader.ReadUInt16(),
                reader.ReadIndex((IndexSize) layout.Columns[2].Size));
        }

        public ParameterDefinitionRow(ParameterAttributes attributes, ushort sequence, uint name)
        {
            Attributes = attributes;
            Sequence = sequence;
            Name = name;
        }

        /// <inheritdoc />
        public TableIndex TableIndex => TableIndex.Param;

        /// <summary>
        /// Gets the attributes associated to the parameter.
        /// </summary>
        public ParameterAttributes Attributes
        {
            get;
        }

        /// <summary>
        /// Gets the index of the parameter definition.
        /// </summary>
        public ushort Sequence
        {
            get;
        }

        /// <summary>
        /// Gets an index into the #Strings heap containing the name of the type reference.
        /// </summary>
        /// <remarks>
        /// If this value is zero, the parameter name is considered <c>null</c>.
        /// </remarks>
        public uint Name
        {
            get;
        }

        /// <inheritdoc />
        public void Write(IBinaryStreamWriter writer, TableLayout layout)
        {
            writer.WriteUInt16((ushort) Attributes);
            writer.WriteUInt16(Sequence);
            writer.WriteIndex(Name, (IndexSize) layout.Columns[2].Size);
        }

        /// <summary>
        /// Determines whether this row is considered equal to the provided parameter definition row.
        /// </summary>
        /// <param name="other">The other row.</param>
        /// <returns><c>true</c> if the rows are equal, <c>false</c> otherwise.</returns>
        public bool Equals(ParameterDefinitionRow other)
        {
            return Attributes == other.Attributes && Sequence == other.Sequence && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is ParameterDefinitionRow other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int) Attributes;
                hashCode = (hashCode * 397) ^ Sequence.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Name;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"({(int) Attributes:X4}, {Sequence:X4}, {Name:X8})";
        }
        
    }
}