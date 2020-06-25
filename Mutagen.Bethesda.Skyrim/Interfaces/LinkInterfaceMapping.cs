using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Core;

namespace Mutagen.Bethesda.Skyrim.Internals
{
    public class LinkInterfaceMapping : ILinkInterfaceMapping
    {
        public IReadOnlyDictionary<Type, Type[]> InterfaceToObjectTypes { get; }

        public GameMode GameMode => GameMode.Skyrim;

        public LinkInterfaceMapping()
        {
            var dict = new Dictionary<Type, Type[]>();
            dict[typeof(IIdleRelation)] = new Type[]
            {
                typeof(ActionRecord),
                typeof(IdleAnimation),
            };
            dict[typeof(IIdleRelationGetter)] = dict[typeof(IIdleRelation)];
            dict[typeof(IObjectId)] = new Type[]
            {
                typeof(Activator),
                typeof(Ammunition),
                typeof(Armor),
                typeof(Book),
                typeof(Container),
                typeof(Door),
                typeof(Faction),
                typeof(FormList),
                typeof(Furniture),
                typeof(IdleMarker),
                typeof(Ingestible),
                typeof(Key),
                typeof(Light),
                typeof(MiscItem),
                typeof(MoveableStatic),
                typeof(Npc),
                typeof(Projectile),
                typeof(Scroll),
                typeof(Shout),
                typeof(SoundMarker),
                typeof(Spell),
                typeof(Static),
                typeof(TextureSet),
                typeof(Weapon),
            };
            dict[typeof(IObjectIdGetter)] = dict[typeof(IObjectId)];
            dict[typeof(IItem)] = new Type[]
            {
                typeof(AlchemicalApparatus),
                typeof(Ammunition),
                typeof(Armor),
                typeof(Book),
                typeof(Ingestible),
                typeof(Ingredient),
                typeof(Key),
                typeof(LeveledItem),
                typeof(Light),
                typeof(MiscItem),
                typeof(Scroll),
                typeof(SoulGem),
                typeof(Weapon),
            };
            dict[typeof(IItemGetter)] = dict[typeof(IItem)];
            dict[typeof(IComplexLocation)] = new Type[]
            {
                typeof(Cell),
                typeof(Worldspace),
            };
            dict[typeof(IComplexLocationGetter)] = dict[typeof(IComplexLocation)];
            dict[typeof(IDialog)] = new Type[]
            {
                typeof(DialogResponses),
                typeof(DialogTopic),
            };
            dict[typeof(IDialogGetter)] = dict[typeof(IDialog)];
            dict[typeof(ILocationTargetable)] = new Type[]
            {
                typeof(Door),
                typeof(PlacedNpc),
                typeof(PlacedObject),
            };
            dict[typeof(ILocationTargetableGetter)] = dict[typeof(ILocationTargetable)];
            dict[typeof(IOwner)] = new Type[]
            {
                typeof(Faction),
                typeof(PlacedNpc),
            };
            dict[typeof(IOwnerGetter)] = dict[typeof(IOwner)];
            dict[typeof(IRelatable)] = new Type[]
            {
                typeof(Faction),
                typeof(Race),
            };
            dict[typeof(IRelatableGetter)] = dict[typeof(IRelatable)];
            dict[typeof(IRegionTarget)] = new Type[]
            {
                typeof(Flora),
                typeof(LandscapeTexture),
                typeof(MoveableStatic),
                typeof(Static),
                typeof(Tree),
            };
            dict[typeof(IRegionTargetGetter)] = dict[typeof(IRegionTarget)];
            dict[typeof(IAliasVoiceType)] = new Type[]
            {
                typeof(FormList),
                typeof(Npc),
            };
            dict[typeof(IAliasVoiceTypeGetter)] = dict[typeof(IAliasVoiceType)];
            dict[typeof(ILockList)] = new Type[]
            {
                typeof(FormList),
                typeof(Npc),
            };
            dict[typeof(ILockListGetter)] = dict[typeof(ILockList)];
            dict[typeof(IPlacedTrapTarget)] = new Type[]
            {
                typeof(Hazard),
                typeof(Projectile),
            };
            dict[typeof(IPlacedTrapTargetGetter)] = dict[typeof(IPlacedTrapTarget)];
            dict[typeof(IHarvestTarget)] = new Type[]
            {
                typeof(Ingestible),
                typeof(Ingredient),
                typeof(LeveledItem),
                typeof(MiscItem),
            };
            dict[typeof(IHarvestTargetGetter)] = dict[typeof(IHarvestTarget)];
            dict[typeof(IKeywordLinkedReference)] = new Type[]
            {
                typeof(Keyword),
            };
            dict[typeof(IKeywordLinkedReferenceGetter)] = dict[typeof(IKeywordLinkedReference)];
            dict[typeof(INpcSpawn)] = new Type[]
            {
                typeof(LeveledNpc),
                typeof(Npc),
            };
            dict[typeof(INpcSpawnGetter)] = dict[typeof(INpcSpawn)];
            dict[typeof(ISpellSpawn)] = new Type[]
            {
                typeof(LeveledSpell),
                typeof(Spell),
            };
            dict[typeof(ISpellSpawnGetter)] = dict[typeof(ISpellSpawn)];
            dict[typeof(IEmittance)] = new Type[]
            {
                typeof(Light),
                typeof(Region),
            };
            dict[typeof(IEmittanceGetter)] = dict[typeof(IEmittance)];
            dict[typeof(ILocationRecord)] = new Type[]
            {
                typeof(Location),
                typeof(LocationReferenceType),
            };
            dict[typeof(ILocationRecordGetter)] = dict[typeof(ILocationRecord)];
            dict[typeof(IEffectRecord)] = new Type[]
            {
                typeof(ObjectEffect),
                typeof(Spell),
            };
            dict[typeof(IEffectRecordGetter)] = dict[typeof(IEffectRecord)];
            dict[typeof(ILinkedReference)] = new Type[]
            {
                typeof(PlacedNpc),
                typeof(PlacedObject),
                typeof(APlacedTrap),
            };
            dict[typeof(ILinkedReferenceGetter)] = dict[typeof(ILinkedReference)];
            dict[typeof(IPlaced)] = new Type[]
            {
                typeof(PlacedNpc),
                typeof(PlacedObject),
                typeof(APlacedTrap),
            };
            dict[typeof(IPlacedGetter)] = dict[typeof(IPlaced)];
            dict[typeof(IPlacedSimple)] = new Type[]
            {
                typeof(PlacedNpc),
                typeof(PlacedObject),
            };
            dict[typeof(IPlacedSimpleGetter)] = dict[typeof(IPlacedSimple)];
            dict[typeof(IPlacedThing)] = new Type[]
            {
                typeof(PlacedObject),
                typeof(APlacedTrap),
            };
            dict[typeof(IPlacedThingGetter)] = dict[typeof(IPlacedThing)];
            dict[typeof(ISound)] = new Type[]
            {
                typeof(SoundDescriptor),
                typeof(SoundMarker),
            };
            dict[typeof(ISoundGetter)] = dict[typeof(ISound)];
            InterfaceToObjectTypes = dict;
        }
    }
}

