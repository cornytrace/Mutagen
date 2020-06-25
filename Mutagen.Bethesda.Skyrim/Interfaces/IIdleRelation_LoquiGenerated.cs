using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [ActionRecord, IdleAnimation]
    /// </summary>
    public partial interface IIdleRelation :
        ISkyrimMajorRecordInternal,
        IIdleRelationGetter
    {
    }

    /// <summary>
    /// Implemented by: [ActionRecord, IdleAnimation]
    /// </summary>
    public partial interface IIdleRelationGetter : ISkyrimMajorRecordGetter
    {
    }
}
