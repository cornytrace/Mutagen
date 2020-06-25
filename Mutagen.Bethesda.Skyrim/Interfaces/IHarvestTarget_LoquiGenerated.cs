using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [Ingestible, Ingredient, LeveledItem, MiscItem]
    /// </summary>
    public partial interface IHarvestTarget :
        ISkyrimMajorRecordInternal,
        IHarvestTargetGetter
    {
    }

    /// <summary>
    /// Implemented by: [Ingestible, Ingredient, LeveledItem, MiscItem]
    /// </summary>
    public partial interface IHarvestTargetGetter : ISkyrimMajorRecordGetter
    {
    }
}
