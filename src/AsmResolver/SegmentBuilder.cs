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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AsmResolver
{
    /// <summary>
    /// Represents a collection of segments concatenated (and aligned) after each other.
    /// </summary>
    public class SegmentBuilder : ISegment, IEnumerable<ISegment>
    {
        private readonly IList<AlignedSegment> _items = new List<AlignedSegment>();
        private uint _physicalSize;
        private uint _virtualSize;
        
        /// <summary>
        /// Gets the number of sub segments that are stored into the segment.
        /// </summary>
        public int Count => _items.Count;

        /// <inheritdoc />
        public uint FileOffset
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public uint Rva
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool CanUpdateOffsets => true;

        public void Add(ISegment segment) => Add(segment, 1);

        public void Add(ISegment segment, uint alignment)
        {
            _items.Add(new AlignedSegment(segment, alignment));
        }
        
        /// <inheritdoc />
        public void UpdateOffsets(uint newFileOffset, uint newRva)
        {
            FileOffset = newFileOffset;
            Rva = newRva;
            _physicalSize = 0;
            _virtualSize = 0;
            
            foreach (var item in _items)
            {
                uint physicalPadding = newFileOffset.Align(item.Alignment) - newFileOffset;
                uint virtualPadding = newRva.Align(item.Alignment) - newRva;
                
                newFileOffset += physicalPadding;
                newRva += virtualPadding;
                
                item.Segment.UpdateOffsets(newFileOffset, newRva);

                uint physicalSize = item.Segment.GetPhysicalSize();
                uint virtualSize = item.Segment.GetVirtualSize();
                
                newFileOffset += physicalSize;
                newRva += virtualSize;
                _physicalSize += physicalPadding + physicalSize;
                _virtualSize += virtualPadding + virtualSize;
            }
        }

        /// <inheritdoc />
        public uint GetPhysicalSize()
        {
            return _physicalSize;
        }

        /// <inheritdoc />
        public uint GetVirtualSize()
        {
            return _virtualSize;
        }

        /// <inheritdoc />
        public void Write(IBinaryStreamWriter writer)
        {
            uint start = writer.FileOffset;
            for (int i = 0; i < _items.Count; i++)
            {
                var current = _items[i];
                writer.FileOffset = current.Segment.FileOffset - FileOffset + start;
                current.Segment.Write(writer);
            }
        }

        public IEnumerator<ISegment> GetEnumerator() => _items.Select(s => s.Segment).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly struct AlignedSegment
        {
            public AlignedSegment(ISegment segment, uint alignment)
            {
                Segment = segment;
                Alignment = alignment;
            }

            public ISegment Segment
            {
                get;
            }

            public uint Alignment
            {
                get;
            }
        }
        
    }
}