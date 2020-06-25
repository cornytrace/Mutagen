using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [Cell, Worldspace]
    /// </summary>
    public partial interface IComplexLocation :
        ISkyrimMajorRecordInternal,
        IComplexLocationGetter
    {
    }

    /// <summary>
    /// Implemented by: [Cell, Worldspace]
    /// </summary>
    public partial interface IComplexLocationGetter : ISkyrimMajorRecordGetter
    {
    }
}
