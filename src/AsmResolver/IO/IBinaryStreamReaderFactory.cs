using System;
using System.IO;

namespace AsmResolver.IO
{
    /// <summary>
    /// Provides members for creating new binary streams.
    /// </summary>
    public interface IBinaryStreamReaderFactory : IDisposable
    {
        /// <summary>
        /// Gets the maximum length a single binary stream reader produced by this factory can have.
        /// </summary>
        uint MaxLength
        {
            get;
        }

        /// <summary>
        /// Creates a new binary reader at the provided address.
        /// </summary>
        /// <param name="address">The raw address to start reading from.</param>
        /// <param name="rva">The virtual address (relative to the image base) that is associated to the raw address.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The created reader.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Occurs if <paramref name="address"/> is not a valid address.</exception>
        /// <exception cref="EndOfStreamException">Occurs if <paramref name="length"/> is too long.</exception>
        BinaryStreamReader CreateReader(ulong address, uint rva, uint length);
    }

    public static partial class IOExtensions
    {
        /// <summary>
        /// Creates a binary reader for the entire address space.
        /// </summary>
        /// <param name="factory">The factory to use.</param>
        /// <returns>The constructed reader.</returns>
        public static BinaryStreamReader CreateReader(this IBinaryStreamReaderFactory factory)
            => factory.CreateReader(0, 0, factory.MaxLength);
    }
}
