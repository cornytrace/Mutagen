using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [Activator, Ammunition, Armor, Book, Container, Door, Faction, FormList, Furniture, IdleMarker, Ingestible, Key, Light, MiscItem, MoveableStatic, Npc, Projectile, Scroll, Shout, SoundMarker, Spell, Static, TextureSet, Weapon]
    /// </summary>
    public partial interface IObjectId :
        ISkyrimMajorRecordInternal,
        IObjectIdGetter
    {
    }

    /// <summary>
    /// Implemented by: [Activator, Ammunition, Armor, Book, Container, Door, Faction, FormList, Furniture, IdleMarker, Ingestible, Key, Light, MiscItem, MoveableStatic, Npc, Projectile, Scroll, Shout, SoundMarker, Spell, Static, TextureSet, Weapon]
    /// </summary>
    public partial interface IObjectIdGetter : ISkyrimMajorRecordGetter
    {
    }
}
