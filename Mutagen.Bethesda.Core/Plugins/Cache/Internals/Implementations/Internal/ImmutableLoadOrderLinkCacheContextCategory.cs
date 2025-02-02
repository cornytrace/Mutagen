﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Loqui;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Internals;
using Noggog;

namespace Mutagen.Bethesda.Plugins.Cache.Internals.Implementations.Internal
{
    internal interface IImmutableLoadOrderLinkCacheContextCategory<TMod, TModGetter, TKey> : IImmutableLoadOrderLinkCacheSimpleContextCategory<TKey>
        where TMod : class, IContextMod<TMod, TModGetter>, TModGetter 
        where TModGetter : class, IContextGetterMod<TMod, TModGetter>
        where TKey : notnull
    {
        bool TryResolveContext(
            TKey key,
            ModKey? modKey,
            Type type,
            [MaybeNullWhen(false)] out IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter> majorRec);

        IEnumerable<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>> ResolveAllContexts(
            TKey key,
            ModKey? modKey,
            Type type);
    }

    internal class ImmutableLoadOrderLinkCacheContextCategory<TMod, TModGetter, TKey> : IImmutableLoadOrderLinkCacheContextCategory<TMod, TModGetter, TKey>
        where TKey : notnull
        where TMod : class, IContextMod<TMod, TModGetter>, TModGetter
        where TModGetter : class, IContextGetterMod<TMod, TModGetter>
    {
        private readonly bool _simple;
        private readonly bool _hasAny;
        private readonly ILinkCache _linkCache;
        private readonly IReadOnlyList<TModGetter> _listedOrder;
        private readonly IReadOnlyDictionary<Type, Type[]> _linkInterfaces;
        private readonly Func<IMajorRecordCommonGetter, TryGet<TKey>> _keyGetter;
        private readonly Func<TKey, bool> _shortCircuit;
        private readonly Dictionary<Type, DepthCache<TKey, IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>> _winningContexts = new();
        private readonly Dictionary<Type, DepthCache<TKey, ImmutableList<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>>> _allContexts = new();

        public ImmutableLoadOrderLinkCacheContextCategory(
            GameCategory category,
            bool simple,
            bool hasAny,
            ILinkCache linkCache,
            IReadOnlyList<TModGetter> listedOrder,
            Func<IMajorRecordCommonGetter, TryGet<TKey>> keyGetter,
            Func<TKey, bool> shortCircuit)
        {
            _simple = simple;
            _hasAny = hasAny;
            _linkCache = linkCache;
            _listedOrder = listedOrder;
            _keyGetter = keyGetter;
            _linkInterfaces = LinkInterfaceMapping.InterfaceToObjectTypes(category);
            _shortCircuit = shortCircuit;
        }

        public bool TryResolveContext(
            TKey key,
            ModKey? modKey,
            Type type,
            [MaybeNullWhen(false)] out IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter> majorRec)
        {
            if (_simple)
            {
                throw new ArgumentException("Queried for record on a simple cache");
            }
            
            if (!_hasAny || _shortCircuit(key))
            {
                majorRec = default;
                return false;
            }

            DepthCache<TKey, IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>? cache;
            lock (_winningContexts)
            {
                // Get cache object by type
                if (!_winningContexts.TryGetValue(type, out cache))
                {
                    cache = new DepthCache<TKey, IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>();
                    if (type.Equals(typeof(IMajorRecordCommon))
                        || type.Equals(typeof(IMajorRecordCommonGetter)))
                    {
                        _winningContexts[typeof(IMajorRecordCommon)] = cache;
                        _winningContexts[typeof(IMajorRecordCommonGetter)] = cache;
                    }
                    else if (LoquiRegistration.TryGetRegister(type, out var registration))
                    {
                        _winningContexts[registration.ClassType] = cache;
                        _winningContexts[registration.GetterType] = cache;
                        _winningContexts[registration.SetterType] = cache;
                        if (registration.InternalGetterType != null)
                        {
                            _winningContexts[registration.InternalGetterType] = cache;
                        }
                        if (registration.InternalSetterType != null)
                        {
                            _winningContexts[registration.InternalSetterType] = cache;
                        }
                    }
                    else
                    {
                        if (!_linkInterfaces.TryGetValue(type, out var objs))
                        {
                            throw new ArgumentException($"A lookup was queried for an unregistered type: {type.Name}");
                        }
                        _winningContexts[type] = cache;
                    }
                }
            }

            // If we're done, we can just query without locking
            if (cache.Done)
            {
                return cache.TryGetValue(key, out majorRec);
            }

            // Potentially more to query, need to lock
            lock (cache)
            {
                // Check for record
                if (cache.TryGetValue(key, out majorRec))
                {
                    return true;
                }
                if (InternalImmutableLoadOrderLinkCache.ShouldStopQuery(modKey, _listedOrder.Count, cache))
                {
                    majorRec = default!;
                    return false;
                }

                while (!InternalImmutableLoadOrderLinkCache.ShouldStopQuery(modKey, _listedOrder.Count, cache))
                {
                    // Get next unprocessed mod
                    var targetIndex = _listedOrder.Count - cache.Depth - 1;
                    var targetMod = _listedOrder[targetIndex];
                    cache.Depth++;
                    cache.PassedMods.Add(targetMod.ModKey);

                    void AddRecords(TModGetter mod, Type type)
                    {
                        foreach (var record in mod.EnumerateMajorRecordContexts(_linkCache, type))
                        {
                            var key = _keyGetter(record.Record);
                            if (key.Failed) continue;
                            cache.AddIfMissing(key.Value, record);
                        }
                    }

                    // Add records from that mod that aren't already cached
                    if (_linkInterfaces.TryGetValue(type, out var objs))
                    {
                        foreach (var objType in objs)
                        {
                            AddRecords(targetMod, LoquiRegistration.GetRegister(objType).GetterType);
                        }
                    }
                    else
                    {
                        AddRecords(targetMod, type);
                    }
                    // Check again
                    if (cache.TryGetValue(key, out majorRec))
                    {
                        return true;
                    }
                }
                // Record doesn't exist
                majorRec = default;
                return false;
            }
        }

        public IEnumerable<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>> ResolveAllContexts(
            TKey key,
            ModKey? modKey,
            Type type)
        {
            if (!_hasAny || _shortCircuit(key))
            {
                yield break;
            }

            // Grab the type cache
            DepthCache<TKey, ImmutableList<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>> cache;
            lock (_allContexts)
            {
                cache = _allContexts.GetOrAdd(type);
            }

            // Grab the formkey's list
            ImmutableList<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>? list;
            int consideredDepth;
            lock (cache)
            {
                if (!cache.TryGetValue(key, out list))
                {
                    list = ImmutableList<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>.Empty;
                    cache.Add(key, list);
                }
                consideredDepth = cache.Depth;
            }

            // Return everyhing we have already
            foreach (var item in list)
            {
                yield return item;
            }

            int iteratedCount = list.Count;
            bool more = !InternalImmutableLoadOrderLinkCache.ShouldStopQuery(modKey, _listedOrder.Count, cache);

            // While there's more depth to consider
            while (more)
            {
                // Process one more mod
                lock (cache)
                {
                    // Only process if no one else has done some work
                    if (consideredDepth == cache.Depth)
                    {
                        // Get next unprocessed mod
                        var targetIndex = _listedOrder.Count - cache.Depth - 1;
                        var targetMod = _listedOrder[targetIndex];
                        cache.Depth++;
                        cache.PassedMods.Add(targetMod.ModKey);

                        void AddRecords(TModGetter mod, Type type)
                        {
                            foreach (var item in mod.EnumerateMajorRecordContexts(_linkCache, type))
                            {
                                var iterKey = _keyGetter(item.Record);
                                if (iterKey.Failed) continue;
                                if (!cache.TryGetValue(iterKey.Value, out var targetList))
                                {
                                    targetList = ImmutableList<IModContext<TMod, TModGetter, IMajorRecordCommon, IMajorRecordCommonGetter>>.Empty;
                                }
                                cache.Set(iterKey.Value, targetList.Add(item));
                            }
                            if (cache.TryGetValue(key, out var requeriedList))
                            {
                                list = requeriedList;
                            }
                        }

                        // Add records from that mod that aren't already cached
                        if (_linkInterfaces.TryGetValue(type, out var objs))
                        {
                            foreach (var objType in objs)
                            {
                                AddRecords(targetMod, LoquiRegistration.GetRegister(objType).GetterType);
                            }
                        }
                        else
                        {
                            AddRecords(targetMod, type);
                        }
                    }
                    consideredDepth = cache.Depth;
                    more = !InternalImmutableLoadOrderLinkCache.ShouldStopQuery(modKey, _listedOrder.Count, cache);
                }

                // Return any new data
                for (int i = iteratedCount; i < list.Count; i++)
                {
                    yield return list[i];
                }
                iteratedCount = list.Count;
            }
        }

        public bool TryResolveSimpleContext(TKey key, ModKey? modKey, Type type, [MaybeNullWhen(false)] out IModContext<IMajorRecordCommonGetter> majorRec)
        {
            if (TryResolveContext(key, modKey, type, out var simple))
            {
                majorRec = simple;
                return true;
            }

            majorRec = default;
            return false;
        }

        public IEnumerable<IModContext<IMajorRecordCommonGetter>> ResolveAllSimpleContexts(TKey key, ModKey? modKey, Type type)
        {
            return ResolveAllContexts(key, modKey, type);
        }
    }
}