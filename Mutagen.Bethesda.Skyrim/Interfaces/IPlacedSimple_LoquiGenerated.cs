using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [PlacedNpc, PlacedObject]
    /// </summary>
    public partial interface IPlacedSimple :
        ISkyrimMajorRecordInternal,
        IPlacedSimpleGetter
    {
    }

    /// <summary>
    /// Implemented by: [PlacedNpc, PlacedObject]
    /// </summary>
    public partial interface IPlacedSimpleGetter : ISkyrimMajorRecordGetter
    {
    }
}
