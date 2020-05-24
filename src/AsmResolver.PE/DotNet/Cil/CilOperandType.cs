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

namespace AsmResolver.PE.DotNet.Cil
{
    /// <summary>
    /// Provides members defining all categories of operands that each CIL instruction can have.
    /// </summary>
    public enum CilOperandType
    {
        /// <summary>
        /// Indicates the CIL instruction has a branch target (a signed 32-bit offset relative to the next instruction's
        /// offset) as operand.
        /// </summary>
        InlineBrTarget,
        
        /// <summary>
        /// Indicates the CIL instruction has a metadata token referencing a field as operand.
        /// </summary>
        InlineField,
        
        /// <summary>
        /// Indicates the CIL instruction has a single 32-bit integer as operand. 
        /// </summary>
        InlineI,
        
        /// <summary>
        /// Indicates the CIL instruction has a single 64-bit integer as operand. 
        /// </summary>
        InlineI8,
        
        /// <summary>
        /// Indicates the CIL instruction has a metadata token referencing a method as operand.
        /// </summary>
        InlineMethod,
        
        /// <summary>
        /// Indicates the CIL instruction has no operand.
        /// </summary>
        InlineNone,
        
        /// <summary>
        /// Indicates the CIL instruction has a list of phi variables as operand.
        /// </summary>
        /// <remarks>
        /// This operand type is not used in the default CIL instruction set, and is only meant to be used by the
        /// runtime itself. 
        /// </remarks>
        InlinePhi,
        
        /// <summary>
        /// Indicates the CIL instruction has a 64-bit floating point number as operand.
        /// </summary>
        InlineR,
        
        /// <summary>
        /// Indicates the CIL instruction has a metadata token referencing a standalone signature as operand.
        /// </summary>
        InlineSig = 9,
        
        /// <summary>
        /// Indicates the CIL instruction has a metadata token referencing a string in the #US stream as operand.
        /// </summary>
        InlineString,
        
        /// <summary>
        /// Indicates the CIL instruction has a jump table (an array of 32-bit offsets relative to the next instruction's
        /// offset) as operand.  
        /// </summary>
        InlineSwitch,
        
        /// <summary>
        /// Indicates the CIL instruction has a metadata token referencing a type or member as operand.
        /// </summary>
        InlineTok,
        
        /// <summary>
        /// Indicates the CIL instruction has a metadata token referencing a type as operand.
        /// </summary>
        InlineType,
        
        /// <summary>
        /// Indicates the CIL instruction has a 16-bit variable index as operand. 
        /// </summary>
        InlineVar,
        
        /// <summary>
        /// Indicates the CIL instruction has a short branch target (a signed 8-bit offset relative to the next
        /// instruction's offset) as operand. 
        /// </summary>
        ShortInlineBrTarget,
        
        /// <summary>
        /// Indicates the CIL instruction has a signed 8-bit integer as operand.
        /// </summary>
        ShortInlineI,
        
        /// <summary>
        /// Indicates the CIL instruction has a 32-bit floating point number as operand.
        /// </summary>
        ShortInlineR,
        
        /// <summary>
        /// Indicates the CIL instruction has an 8-bit variable index as operand. 
        /// </summary>
        ShortInlineVar,
        
        /// <summary>
        /// Indicates the CIL instruction has a 16-bit parameter index as operand.
        /// </summary>
        InlineArgument,
        
        /// <summary>
        /// Indicates the CIL instruction has an 8-bit parameter index as operand.
        /// </summary>
        ShortInlineArgument,
    }
}