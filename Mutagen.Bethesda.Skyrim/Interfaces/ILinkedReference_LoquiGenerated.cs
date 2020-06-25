using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [PlacedNpc, PlacedObject, APlacedTrap]
    /// </summary>
    public partial interface ILinkedReference :
        ISkyrimMajorRecordInternal,
        ILinkedReferenceGetter,
        IPlaced
    {
    }

    /// <summary>
    /// Implemented by: [PlacedNpc, PlacedObject, APlacedTrap]
    /// </summary>
    public partial interface ILinkedReferenceGetter :
        ISkyrimMajorRecordGetter,
        IPlacedGetter
    {
    }
}
