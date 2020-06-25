using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [LeveledSpell, Spell]
    /// </summary>
    public partial interface ISpellSpawn :
        ISkyrimMajorRecordInternal,
        ISpellSpawnGetter
    {
    }

    /// <summary>
    /// Implemented by: [LeveledSpell, Spell]
    /// </summary>
    public partial interface ISpellSpawnGetter : ISkyrimMajorRecordGetter
    {
    }
}
