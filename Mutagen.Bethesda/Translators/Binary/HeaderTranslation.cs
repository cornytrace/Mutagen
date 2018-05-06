﻿using Mutagen.Bethesda.Internals;
using Noggog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutagen.Bethesda.Binary
{
    public class HeaderTranslation
    {
        public static readonly RecordType GRUP_HEADER = new RecordType("GRUP");

        public static bool TryParse(
            MutagenReader reader,
            RecordType expectedHeader,
            out long contentLength,
            long lengthLength)
        {
            if (reader.Remaining < Constants.HEADER_LENGTH)
            {
                contentLength = -1;
                return false;
            }
            var header = Encoding.ASCII.GetString(reader.ReadBytes(Constants.HEADER_LENGTH));
            if (!expectedHeader.Equals(header))
            {
                contentLength = -1;
                reader.Position -= Constants.HEADER_LENGTH;
                return false;
            }
            switch (lengthLength)
            {
                case 1:
                    contentLength = reader.ReadByte();
                    break;
                case 2:
                    contentLength = reader.ReadInt16();
                    break;
                case 4:
                    contentLength = reader.ReadInt32();
                    break;
                default:
                    throw new NotImplementedException();
            }
            return true;
        }

        public static bool TryGet(
            MutagenReader reader,
            RecordType expectedHeader,
            out long contentLength,
            long lengthLength)
        {
            var ret = TryParse(
                reader,
                expectedHeader,
                out contentLength,
                lengthLength);
            if (ret)
            {
                reader.Position -= Constants.HEADER_LENGTH + lengthLength;
            }
            return ret;
        }

        public static long Parse(
            MutagenReader reader,
            RecordType expectedHeader,
            int lengthLength)
        {
            if (!TryParse(
                reader,
                expectedHeader,
                out var contentLength,
                lengthLength))
            {
                throw new ArgumentException($"Expected header was not read in: {expectedHeader}");
            }
            return contentLength;
        }

        public static long ParseRecord(
            MutagenReader reader,
            RecordType expectedHeader)
        {
            if (!TryParse(
                reader,
                expectedHeader,
                out var contentLength,
                Constants.RECORD_LENGTHLENGTH))
            {
                throw new ArgumentException($"Expected header was not read in: {expectedHeader}");
            }
            return reader.Position + contentLength + Constants.RECORD_META_SKIP;
        }

        public static long ParseSubrecord(
            MutagenReader reader,
            RecordType expectedHeader)
        {
            if (!TryParse(
                reader,
                expectedHeader,
                out var contentLength,
                Constants.SUBRECORD_LENGTHLENGTH))
            {
                throw new ArgumentException($"Expected header was not read in: {expectedHeader}");
            }
            return reader.Position + contentLength;
        }

        public static long ParseGroup(
            MutagenReader reader)
        {
            if (!TryParse(
                reader,
                GRUP_HEADER,
                out var contentLength,
                Constants.RECORD_LENGTHLENGTH))
            {
                throw new ArgumentException($"Expected header was not read in: {GRUP_HEADER}");
            }
            return reader.Position + contentLength - Constants.HEADER_LENGTH - Constants.RECORD_LENGTHLENGTH;
        }

        public static bool TryParseRecordType(
            MutagenReader reader,
            int lengthLength,
            RecordType expectedHeader)
        {
            if (TryParse(
                reader,
                expectedHeader,
                out var contentLength,
                lengthLength))
            {
                return true;
            }
            return false;
        }

        public static bool TryGetRecordType(
            MutagenReader reader,
            int lengthLength,
            RecordType expectedHeader)
        {
            if (TryGet(
                reader,
                expectedHeader,
                out var contentLength,
                lengthLength))
            {
                return true;
            }
            return false;
        }

        public static long GetSubrecord(
            MutagenReader reader,
            RecordType expectedHeader)
        {
            var ret = ParseSubrecord(
                reader,
                expectedHeader);
            reader.Position -= Constants.SUBRECORD_LENGTH;
            return ret;
        }

        public static RecordType ReadNextRecordType(
            MutagenReader reader)
        {
            var header = Encoding.ASCII.GetString(reader.ReadBytes(Constants.HEADER_LENGTH));
            return new RecordType(header, validate: false);
        }

        public static RecordType GetNextRecordType(
            MutagenReader reader,
            RecordTypeConverter recordTypeConverter = null)
        {
            var header = Encoding.ASCII.GetString(reader.ReadBytes(Constants.HEADER_LENGTH));
            var ret = new RecordType(header, validate: false);
            ret = recordTypeConverter.ConvertToStandard(ret);
            reader.Position -= Constants.HEADER_LENGTH;
            return ret;
        }

        public static RecordType GetNextRecordType(
            MutagenReader reader,
            out int contentLength,
            RecordTypeConverter recordTypeConverter = null)
        {
            var ret = ReadNextRecordType(reader, out contentLength);
            ret = recordTypeConverter.ConvertToStandard(ret);
            reader.Position -= Constants.HEADER_LENGTH + Constants.RECORD_LENGTHLENGTH;
            return ret;
        }

        protected static int ReadContentLength(
            MutagenReader reader,
            int lengthLength)
        {
            switch (lengthLength)
            {
                case 1:
                    return reader.ReadByte();
                case 2:
                    return reader.ReadUInt16();
                case 4:
                    return (int)reader.ReadUInt32();
                default:
                    throw new NotImplementedException();
            }
        }

        public static RecordType ReadNextRecordType(
            MutagenReader reader,
            int lengthLength,
            out int contentLength)
        {
            var ret = ReadNextRecordType(reader);
            contentLength = ReadContentLength(reader, lengthLength);
            return ret;
        }

        public static RecordType ReadNextRecordType(
            MutagenReader reader,
            out int contentLength)
        {
            return ReadNextRecordType(
                reader,
                Constants.RECORD_LENGTHLENGTH,
                out contentLength);
        }

        public static RecordType ReadNextSubRecordType(
            MutagenReader reader,
            out int contentLength)
        {
            return ReadNextRecordType(
                reader,
                Constants.SUBRECORD_LENGTHLENGTH,
                out contentLength);
        }

        public static RecordType ReadNextType(
            MutagenReader reader,
            out int contentLength)
        {
            var ret = ReadNextRecordType(reader);
            contentLength = ReadContentLength(reader, Constants.RECORD_LENGTHLENGTH);
            if (ret.Equals(GRUP_HEADER))
            {
                return ReadNextRecordType(reader);
            }
            return ret;
        }

        public static RecordType GetNextType(
            MutagenReader reader,
            out int contentLength,
            RecordTypeConverter recordTypeConverter = null,
            bool hopGroup = true)
        {
            var ret = ReadNextRecordType(reader);
            ret = recordTypeConverter.ConvertToStandard(ret);
            contentLength = ReadContentLength(reader, Constants.RECORD_LENGTHLENGTH);
            if (hopGroup && ret.Equals(GRUP_HEADER))
            {
                ret = GetNextRecordType(reader);
            }
            reader.Position -= Constants.HEADER_LENGTH + Constants.RECORD_LENGTHLENGTH;
            return ret;
        }

        public static RecordType GetNextSubRecordType(
            MutagenReader reader,
            out int contentLength,
            RecordTypeConverter recordTypeConverter = null)
        {
            var ret = ReadNextRecordType(
                reader,
                Constants.SUBRECORD_LENGTHLENGTH,
                out contentLength);
            ret = recordTypeConverter.ConvertToStandard(ret);
            reader.Position -= Constants.SUBRECORD_LENGTH;
            return ret;
        }
    }
}
