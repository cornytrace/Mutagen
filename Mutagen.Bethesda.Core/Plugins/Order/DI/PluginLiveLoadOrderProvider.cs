﻿using System;
using System.IO.Abstractions;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DynamicData;
using Noggog;

namespace Mutagen.Bethesda.Plugins.Order.DI
{
    public interface IPluginLiveLoadOrderProvider : ISomeLiveLoadOrderProvider
    {
    }

    public class PluginLiveLoadOrderProvider : IPluginLiveLoadOrderProvider
    {
        private readonly IFileSystem _fileSystem;
        private readonly IPluginListingsProvider _listingsProvider;
        private readonly IPluginListingsPathProvider _pluginListingsFilePath;

        public PluginLiveLoadOrderProvider(
            IFileSystem fileSystem,
            IPluginListingsProvider listingsProvider,
            IPluginListingsPathProvider pluginListingsFilePath)
        {
            _fileSystem = fileSystem;
            _listingsProvider = listingsProvider;
            _pluginListingsFilePath = pluginListingsFilePath;
        }
        
        public IObservable<IChangeSet<IModListingGetter>> Get(out IObservable<ErrorResponse> state, IScheduler? scheduler = null)
        {
            var results = ObservableExt.WatchFile(_pluginListingsFilePath.Path, fileWatcherFactory: _fileSystem.FileSystemWatcher)
                .StartWith(Unit.Default)
                .Select(_ =>
                {
                    try
                    {
                        return GetResponse<IObservable<IChangeSet<IModListingGetter>>>.Succeed(
                            _listingsProvider.Get()
                                .AsObservableChangeSet());
                    }
                    catch (Exception ex)
                    {
                        return GetResponse<IObservable<IChangeSet<IModListingGetter>>>.Fail(ex);
                    }
                })
                .Replay(1)
                .RefCount();
            state = results
                .Select(r => (ErrorResponse)r);
            return results
                .Select(r =>
                {
                    return r.Value ?? Observable.Empty<IChangeSet<IModListingGetter>>();
                })
                .Switch()
                .ObserveOnIfApplicable(scheduler);
        }

        public IObservable<Unit> Changed => ObservableExt
            .WatchFile(_pluginListingsFilePath.Path, fileWatcherFactory: _fileSystem.FileSystemWatcher)
            .StartWith(Unit.Default);
    }
}