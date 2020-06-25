using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [PlacedObject, APlacedTrap]
    /// </summary>
    public partial interface IPlacedThing :
        ISkyrimMajorRecordInternal,
        IPlacedThingGetter
    {
    }

    /// <summary>
    /// Implemented by: [PlacedObject, APlacedTrap]
    /// </summary>
    public partial interface IPlacedThingGetter : ISkyrimMajorRecordGetter
    {
    }
}
