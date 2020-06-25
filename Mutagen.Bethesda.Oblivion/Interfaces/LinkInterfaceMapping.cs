using System;
using System.Collections.Generic;
using Mutagen.Bethesda.Core;

namespace Mutagen.Bethesda.Oblivion.Internals
{
    public class LinkInterfaceMapping : ILinkInterfaceMapping
    {
        public IReadOnlyDictionary<Type, Type[]> InterfaceToObjectTypes { get; }

        public GameMode GameMode => GameMode.Oblivion;

        public LinkInterfaceMapping()
        {
            var dict = new Dictionary<Type, Type[]>();
            dict[typeof(IOwner)] = new Type[]
            {
                typeof(Faction),
                typeof(Npc),
            };
            dict[typeof(IOwnerGetter)] = dict[typeof(IOwner)];
            dict[typeof(IPlaced)] = new Type[]
            {
                typeof(Landscape),
                typeof(PlacedCreature),
                typeof(PlacedNpc),
                typeof(PlacedObject),
            };
            dict[typeof(IPlacedGetter)] = dict[typeof(IPlaced)];
            InterfaceToObjectTypes = dict;
        }
    }
}

