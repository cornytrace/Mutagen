using Mutagen.Bethesda.Plugins.Binary.Overlay;
using Mutagen.Bethesda.Plugins.Binary.Streams;
using Mutagen.Bethesda.Plugins.Binary.Translations;
using Noggog;
using System;

namespace Mutagen.Bethesda.Fallout4
{
    public partial class GlobalInt
    {
        public const char TRIGGER_CHAR = 'l';
        public override char TypeChar => TRIGGER_CHAR;

        public override float? RawFloat
        {
            get => this.Data.HasValue ? (float)this.Data : default;
            set
            {
                if (value.HasValue)
                {
                    this.Data = (int)value.Value;
                }
                else
                {
                    this.Data = default;
                }
            }
        }
    }

    namespace Internals
    {
        public partial class GlobalIntBinaryCreateTranslation
        {
            public static partial void FillBinaryDataCustom(MutagenFrame frame, IGlobalIntInternal item)
            {
            }
        }

        public partial class GlobalIntBinaryWriteTranslation
        {
            public static partial void WriteBinaryDataCustom(MutagenWriter writer, IGlobalIntGetter item)
            {
                if (item.Data is not { } data) return;
                using (HeaderExport.Subrecord(writer, RecordTypes.FLTV))
                {
                    writer.Write((float)data);
                }
            }
        }

        public partial class GlobalIntBinaryOverlay
        {
            public override char TypeChar { get { return GlobalInt.TRIGGER_CHAR; } set { } }
            public override float? RawFloat => this.Data is { } data ? (float)data : default;

            private int? _DataLocation;
            public bool GetDataIsSetCustom() => _DataLocation.HasValue;
            public int GetDataCustom()
            {
                return (int)HeaderTranslation.ExtractSubrecordMemory(_data, _DataLocation!.Value, _package.MetaData.Constants).Float();
            }
            partial void DataCustomParse(OverlayStream stream, long finalPos, int offset)
            {
                _DataLocation = (ushort)(stream.Position - offset);
            }
        }
    }
}
