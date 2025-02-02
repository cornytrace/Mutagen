using Mutagen.Bethesda.Binary;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Overlay;
using Mutagen.Bethesda.Plugins.Binary.Streams;
using Noggog;
using System.Collections.Generic;
using Mutagen.Bethesda.Plugins.Binary.Translations;

namespace Mutagen.Bethesda.Skyrim
{
    namespace Internals
    {
        public partial class PackageEventBinaryCreateTranslation
        {
            public static partial void FillBinaryTopicsCustom(MutagenFrame frame, IPackageEvent item)
            {
                item.Topics.SetTo(ATopicReferenceBinaryCreateTranslation.Factory(frame));
            }
        }

        public partial class PackageEventBinaryWriteTranslation
        {
            public static partial void WriteBinaryTopicsCustom(MutagenWriter writer, IPackageEventGetter item)
            {
                ATopicReferenceBinaryWriteTranslation.Write(writer, item.Topics);
            }
        }

        public partial class PackageEventBinaryOverlay
        {
            public IReadOnlyList<IATopicReferenceGetter> Topics { get; private set; } = ListExt.Empty<IATopicReferenceGetter>();

            partial void TopicsCustomParse(
                OverlayStream stream,
                long finalPos,
                int offset,
                RecordType type,
                PreviousParse lastParsed)
            {
                Topics = new List<IATopicReferenceGetter>(
                    ATopicReferenceBinaryCreateTranslation.Factory(
                        new MutagenFrame(stream)));
            }
        }
    }
}
