using Noggog;
using System;
using System.Buffers.Binary;
using System.Text;
using Mutagen.Bethesda.Strings;
using Mutagen.Bethesda.Strings.DI;

namespace Mutagen.Bethesda.Plugins.Binary.Translations
{
    /// <summary>
    /// Static class with string-related utility functions
    /// </summary>
    public static class BinaryStringUtility
    {
        /// <summary>
        /// Converts span to a string.
        /// </summary>
        /// <param name="bytes">Bytes to turn into a string</param>
        /// <returns>string containing a character for every byte in the input span</returns>
        public static string ToZString(ReadOnlySpan<byte> bytes)
        {
            return ToZString(bytes, MutagenEncodingProvider.Instance.Default);
        }
        
        /// <summary>
        /// Converts span to a string.
        /// </summary>
        /// <param name="bytes">Bytes to turn into a string</param>
        /// <param name="encoding">Encoding to use</param>
        /// <returns>string containing a character for every byte in the input span</returns>
        public static string ToZString(ReadOnlySpan<byte> bytes, IMutagenEncoding encoding)
        {
            return (encoding ?? MutagenEncodingProvider.Instance.Default).GetString(bytes);
        }

        /// <summary>
        /// Trims the last byte if it is 0.
        /// </summary>
        /// <param name="bytes">Bytes to trim</param>
        /// <returns>Trimmed bytes</returns>
        public static ReadOnlySpan<byte> ProcessNullTermination(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length == 0) return bytes;
            // If null terminated, don't include
            if (bytes[^1] == 0)
            {
                return bytes[0..^1];
            }
            return bytes;
        }

        /// <summary>
        /// Null trims and then processes bytes into a string
        /// </summary>
        /// <param name="bytes">Bytes to convert</param>
        /// <returns>String representation of bytes</returns>
        public static string ProcessWholeToZString(ReadOnlySpan<byte> bytes)
        {
            bytes = ProcessNullTermination(bytes);
            return ToZString(bytes);
        }

        /// <summary>
        /// Reads bytes from a stream until a null termination character occurs.
        /// Converts results to a string.
        /// </summary>
        /// <param name="stream">Stream to read from</param>
        /// <returns>First null terminated string read</returns>
        public static string ParseUnknownLengthString(IBinaryReadStream stream)
        {
            var mem = stream.RemainingMemory;
            var index = mem.Span.IndexOf(default(byte));
            if (index == -1)
            {
                throw new ArgumentException();
            }
            var ret = BinaryStringUtility.ToZString(mem[0..index]);
            stream.Position += index + 1;
            return ret;
        }

        /// <summary>
        /// Reads bytes from a stream until a null termination character occurs.
        /// Converts results to a string.
        /// </summary>
        /// <param name="bytes">Bytes to convert</param>
        /// <returns>First null terminated string read</returns>
        public static string ParseUnknownLengthString(ReadOnlySpan<byte> bytes)
        {
            return ToZString(ExtractUnknownLengthString(bytes));
        }

        /// <summary>
        /// Reads bytes from a stream until a null termination character occurs.
        /// </summary>
        /// <param name="bytes">Bytes to convert</param>
        /// <returns>Initial span of bytes up until the first null byte</returns>
        public static ReadOnlySpan<byte> ExtractUnknownLengthString(ReadOnlySpan<byte> bytes)
        {
            var index = bytes.IndexOf(default(byte));
            if (index == -1)
            {
                throw new ArgumentException();
            }
            return bytes[..index];
        }

        /// <summary>
        /// Read string of known length, which is prepended by bytes denoting its length.
        /// Converts results to a string.
        /// </summary>
        /// <param name="span">Bytes to retrieve string from</param>
        /// <param name="lengthLength">Amount of bytes containing length information</param>
        /// <returns>String of length denoted by initial bytes</returns>
        public static string ParsePrependedString(ReadOnlySpan<byte> span, byte lengthLength)
        {
            return ProcessWholeToZString(ExtractPrependedString(span, lengthLength));
        }

        /// <summary>
        /// Read string of known length, which is prepended by bytes denoting its length.
        /// Converts results to a string.
        /// </summary>
        /// <param name="span">Bytes to retrieve string from</param>
        /// <param name="lengthLength">Amount of bytes containing length information</param>
        /// <returns>String of length denoted by initial bytes</returns>
        public static ReadOnlySpan<byte> ExtractPrependedString(ReadOnlySpan<byte> span, byte lengthLength)
        {
            switch (lengthLength)
            {
                case 2:
                {
                    var length = BinaryPrimitives.ReadUInt16LittleEndian(span);
                    return span.Slice(2, length);
                }
                case 4:
                {
                    var length = BinaryPrimitives.ReadUInt32LittleEndian(span);
                    return span.Slice(4, checked((int)length));
                }
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Read string of known length, which is prepended by bytes denoting its length.
        /// Converts results to a string.
        /// </summary>
        /// <param name="stream">Stream to retrieve string from</param>
        /// <param name="lengthLength">Amount of bytes containing length information</param>
        /// <returns>String of length denoted by initial bytes</returns>
        public static string ReadPrependedString(this IBinaryReadStream stream, byte lengthLength)
        {
            switch (lengthLength)
            {
                case 2:
                    {
                        var length = stream.ReadUInt16();
                        return ToZString(stream.ReadSpan(length));
                    }
                case 4:
                    {
                        var length = checked((int)stream.ReadUInt32());
                        return ToZString(stream.ReadSpan(length));
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public static void Write(this IBinaryWriteStream stream, string str, StringBinaryType binaryType)
        {
            Write(stream, str, binaryType, MutagenEncodingProvider.Instance.Default);
        }

        public static void Write(this IBinaryWriteStream stream, string str, StringBinaryType binaryType, IMutagenEncoding encoding)
        {
            switch (binaryType)
            {
                case StringBinaryType.Plain:
                    Write(stream, str.AsSpan(), encoding);
                    break;
                case StringBinaryType.NullTerminate:
                    Write(stream, str.AsSpan(), encoding);
                    stream.Write((byte)0);
                    break;
                case StringBinaryType.PrependLength:
                {
                    var len = encoding.GetByteCount(str);
                    stream.Write(len);
                    Write(stream, str.AsSpan(), encoding, len);
                    break;
                }
                case StringBinaryType.PrependLengthUShort:
                {
                    var len = encoding.GetByteCount(str);
                    stream.Write(checked((ushort)len));
                    Write(stream, str.AsSpan(), encoding, len);
                    break;
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public static void Write(IBinaryWriteStream stream, ReadOnlySpan<char> str)
        {
            Write(stream, str, MutagenEncodingProvider.Instance.Default);
        }

        public static void Write(IBinaryWriteStream stream, ReadOnlySpan<char> str, IMutagenEncoding encoding)
        {
            Write(stream, str, encoding, encoding.GetByteCount(str));
        }

        public static void Write(IBinaryWriteStream stream, ReadOnlySpan<char> str, IMutagenEncoding encoding, int byteCount)
        {
            Span<byte> bytes = stackalloc byte[byteCount];
            encoding.GetBytes(str, bytes);
            stream.Write(bytes);
        }
    }
}
