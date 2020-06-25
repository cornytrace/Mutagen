using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [Location, LocationReferenceType]
    /// </summary>
    public partial interface ILocationRecord :
        ISkyrimMajorRecordInternal,
        ILocationRecordGetter
    {
    }

    /// <summary>
    /// Implemented by: [Location, LocationReferenceType]
    /// </summary>
    public partial interface ILocationRecordGetter : ISkyrimMajorRecordGetter
    {
    }
}
