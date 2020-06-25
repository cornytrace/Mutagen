using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [Hazard, Projectile]
    /// </summary>
    public partial interface IPlacedTrapTarget :
        ISkyrimMajorRecordInternal,
        IPlacedTrapTargetGetter
    {
    }

    /// <summary>
    /// Implemented by: [Hazard, Projectile]
    /// </summary>
    public partial interface IPlacedTrapTargetGetter : ISkyrimMajorRecordGetter
    {
    }
}
