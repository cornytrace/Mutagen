using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [Door, PlacedNpc, PlacedObject]
    /// </summary>
    public partial interface ILocationTargetable :
        ISkyrimMajorRecordInternal,
        ILocationTargetableGetter
    {
    }

    /// <summary>
    /// Implemented by: [Door, PlacedNpc, PlacedObject]
    /// </summary>
    public partial interface ILocationTargetableGetter : ISkyrimMajorRecordGetter
    {
    }
}
