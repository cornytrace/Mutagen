using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [FormList, Npc]
    /// </summary>
    public partial interface IAliasVoiceType :
        ISkyrimMajorRecordInternal,
        IAliasVoiceTypeGetter
    {
    }

    /// <summary>
    /// Implemented by: [FormList, Npc]
    /// </summary>
    public partial interface IAliasVoiceTypeGetter : ISkyrimMajorRecordGetter
    {
    }
}
