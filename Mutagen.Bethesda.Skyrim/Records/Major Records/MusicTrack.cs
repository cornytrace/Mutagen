using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Overlay;
using System.Collections.Generic;

namespace Mutagen.Bethesda.Skyrim
{
    public partial class MusicTrack
    {
        public enum TypeEnum : uint
        {
            Palette = 0x23F678C3,
            SingleTrack = 0x6ED7E048,
            SilentTrack = 0xA1A9C4D5,
        }
    }

    namespace Internals
    {
        public partial class MusicTrackBinaryOverlay
        {
            public IReadOnlyList<IConditionGetter>? Conditions { get; private set; }

            partial void ConditionsCustomParse(OverlayStream stream, long finalPos, int offset, RecordType type, int? lastParsed)
            {
                Conditions = ConditionBinaryOverlay.ConstructBinayOverlayCountedList(stream, _package);
            }
        }
    }
}
