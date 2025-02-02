﻿using System.Linq;
using FluentAssertions;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Order;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Testing;
using Mutagen.Bethesda.UnitTests.Plugins.Cache.Linking.Helpers;
using Xunit;

#nullable disable
#pragma warning disable CS0618 // Type or member is obsolete

namespace Mutagen.Bethesda.UnitTests.Plugins.Cache.Linking
{
    public partial class ALinkingTests
    {
        [Fact]
        public void FormLink_LoadOrder_ResolveAll_Empty()
        {
            var package = TypicalLoadOrder().ToImmutableLinkCache();
            FormLink<INpcGetter> formLink = new FormLink<INpcGetter>(UnusedFormKey);
            formLink.ResolveAll(package).Should().BeEmpty();
        }

        [Theory]
        [InlineData(LinkCacheTestTypes.Identifiers)]
        [InlineData(LinkCacheTestTypes.WholeRecord)]
        public void FormLink_LoadOrder_ResolveAll_Linked(LinkCacheTestTypes cacheType)
        {
            var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimLE);
            var npc = mod.Npcs.AddNew();
            var loadOrder = new LoadOrder<ISkyrimModGetter>()
            {
                mod,
                new SkyrimMod(TestConstants.PluginModKey2, SkyrimRelease.SkyrimLE),
            };
            var (style, package) = GetLinkCache(loadOrder, cacheType);
            var formLink = new FormLink<INpcGetter>(npc.FormKey);
            WrapPotentialThrow(cacheType, style, () =>
            {
                var resolved = formLink.ResolveAll(package).ToArray();
                resolved.Should().HaveCount(1);
                resolved.First().Should().BeSameAs(npc);
            });
        }

        [Theory]
        [InlineData(LinkCacheTestTypes.Identifiers)]
        [InlineData(LinkCacheTestTypes.WholeRecord)]
        public void FormLink_LoadOrder_ResolveAll_MultipleLinks(LinkCacheTestTypes cacheType)
        {
            var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimLE);
            var npc = mod.Npcs.AddNew();
            var mod2 = new SkyrimMod(TestConstants.PluginModKey3, SkyrimRelease.SkyrimLE);
            var npcOverride = mod2.Npcs.GetOrAddAsOverride(npc);
            npcOverride.FaceParts = new NpcFaceParts();
            var loadOrder = new LoadOrder<ISkyrimModGetter>()
            {
                mod,
                new SkyrimMod(TestConstants.PluginModKey2, SkyrimRelease.SkyrimLE),
                mod2
            };
            var (style, package) = GetLinkCache(loadOrder, cacheType);
            var formLink = new FormLink<INpcGetter>(npc.FormKey);
            WrapPotentialThrow(cacheType, style, () =>
            {
                var resolved = formLink.ResolveAll(package).ToArray();
                resolved.Should().HaveCount(2);
                resolved.First().Should().BeSameAs(npcOverride);
                resolved.Last().Should().BeSameAs(npc);
            });
        }

        [Theory]
        [InlineData(LinkCacheTestTypes.Identifiers)]
        [InlineData(LinkCacheTestTypes.WholeRecord)]
        public void FormLink_LoadOrder_ResolveAll_DoubleQuery(LinkCacheTestTypes cacheType)
        {
            var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimLE);
            var npc = mod.Npcs.AddNew();
            var mod2 = new SkyrimMod(TestConstants.PluginModKey3, SkyrimRelease.SkyrimLE);
            var npcOverride = mod2.Npcs.GetOrAddAsOverride(npc);
            npcOverride.FaceParts = new NpcFaceParts();
            var loadOrder = new LoadOrder<ISkyrimModGetter>()
            {
                mod,
                new SkyrimMod(TestConstants.PluginModKey2, SkyrimRelease.SkyrimLE),
                mod2
            };
            var (style, package) = GetLinkCache(loadOrder, cacheType);
            var formLink = new FormLink<INpcGetter>(npc.FormKey);
            WrapPotentialThrow(cacheType, style, () =>
            {
                var resolved = formLink.ResolveAll(package).ToArray();
                resolved.Should().HaveCount(2);
                resolved.First().Should().BeSameAs(npcOverride);
                resolved.Last().Should().BeSameAs(npc);
            });
        }

        [Theory]
        [InlineData(LinkCacheTestTypes.Identifiers)]
        [InlineData(LinkCacheTestTypes.WholeRecord)]
        public void FormLink_LoadOrder_ResolveAll_UnrelatedNotIncluded(LinkCacheTestTypes cacheType)
        {
            var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimLE);
            var npc = mod.Npcs.AddNew();
            var unrelatedNpc = mod.Npcs.AddNew();
            var mod2 = new SkyrimMod(TestConstants.PluginModKey3, SkyrimRelease.SkyrimLE);
            var npcOverride = mod2.Npcs.GetOrAddAsOverride(npc);
            npcOverride.FaceParts = new NpcFaceParts();
            var loadOrder = new LoadOrder<ISkyrimModGetter>()
            {
                mod,
                new SkyrimMod(TestConstants.PluginModKey2, SkyrimRelease.SkyrimLE),
                mod2
            };
            var (style, package) = GetLinkCache(loadOrder, cacheType);
            var formLink = new FormLink<INpcGetter>(npc.FormKey);
            WrapPotentialThrow(cacheType, style, () =>
            {
                var resolved = formLink.ResolveAll(package).ToArray();
                resolved.Should().HaveCount(2);
                resolved.First().Should().BeSameAs(npcOverride);
                resolved.Last().Should().BeSameAs(npc);
            });
        }

        [Theory]
        [InlineData(LinkCacheTestTypes.Identifiers)]
        [InlineData(LinkCacheTestTypes.WholeRecord)]
        public void FormLink_LoadOrder_ResolveAll_SeparateQueries(LinkCacheTestTypes cacheType)
        {
            var mod = new SkyrimMod(TestConstants.PluginModKey, SkyrimRelease.SkyrimLE);
            var npc = mod.Npcs.AddNew();
            var unrelatedNpc = mod.Npcs.AddNew();
            var mod2 = new SkyrimMod(TestConstants.PluginModKey3, SkyrimRelease.SkyrimLE);
            var npcOverride = mod2.Npcs.GetOrAddAsOverride(npc);
            npcOverride.FaceParts = new NpcFaceParts();
            var loadOrder = new LoadOrder<ISkyrimModGetter>()
            {
                mod,
                new SkyrimMod(TestConstants.PluginModKey2, SkyrimRelease.SkyrimLE),
                mod2
            };
            var (style, package) = GetLinkCache(loadOrder, cacheType);
            var formLink = new FormLink<INpcGetter>(npc.FormKey);
            WrapPotentialThrow(cacheType, style, () =>
            {
                var resolved = formLink.ResolveAll(package).ToArray();
                resolved.Should().HaveCount(2);
                resolved.First().Should().BeSameAs(npcOverride);
                resolved.Last().Should().BeSameAs(npc);
            });
            formLink = new FormLink<INpcGetter>(unrelatedNpc.FormKey);
            WrapPotentialThrow(cacheType, style, () =>
            {
                var resolved = formLink.ResolveAll(package).ToArray();
                resolved.Should().HaveCount(1);
                resolved.First().Should().BeSameAs(unrelatedNpc);
            });
        }
    }
}