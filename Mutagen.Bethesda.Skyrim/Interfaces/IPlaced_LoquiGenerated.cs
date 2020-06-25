using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [PlacedNpc, PlacedObject, APlacedTrap]
    /// </summary>
    public partial interface IPlaced :
        ISkyrimMajorRecordInternal,
        IPlacedGetter,
        IPlacedThing,
        IPlacedSimple
    {
    }

    /// <summary>
    /// Implemented by: [PlacedNpc, PlacedObject, APlacedTrap]
    /// </summary>
    public partial interface IPlacedGetter :
        ISkyrimMajorRecordGetter,
        IPlacedThingGetter,
        IPlacedSimpleGetter
    {
    }
}
