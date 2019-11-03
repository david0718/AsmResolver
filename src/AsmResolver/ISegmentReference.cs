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

namespace AsmResolver
{
    /// <summary>
    /// Represents a reference to a segment in a binary file, such as the beginning of a function or method body, or
    /// a reference to a chunk of initialization data of a field. 
    /// </summary>
    public interface ISegmentReference : IOffsetProvider
    {
        /// <summary>
        /// Gets a value indicating whether the referenced segment is bounded to a fixed size.
        /// </summary>
        bool IsBounded
        {
            get;
        }
        
        /// <summary>
        /// Creates a binary reader starting at the beginning of the segment.
        /// </summary>
        /// <returns>The binary reader.</returns>
        IBinaryStreamReader CreateReader();
    }
}