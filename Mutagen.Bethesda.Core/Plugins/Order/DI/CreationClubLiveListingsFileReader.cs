﻿using System;
using System.IO.Abstractions;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using Noggog;

namespace Mutagen.Bethesda.Plugins.Order.DI
{
    public interface ICreationClubLiveListingsFileReader
    {
        IObservable<IChangeSet<IModListingGetter>> Get(out IObservable<ErrorResponse> state);
    }

    public class CreationClubLiveListingsFileReader : ICreationClubLiveListingsFileReader
    {
        private readonly IFileSystem _fileSystem;
        public ICreationClubRawListingsReader ListingsReader { get; }
        public ICreationClubListingsPathProvider ListingsPathProvider { get; }

        public CreationClubLiveListingsFileReader(
            IFileSystem fileSystem,
            ICreationClubRawListingsReader listingsReader,
            ICreationClubListingsPathProvider listingsPathProvider)
        {
            _fileSystem = fileSystem;
            ListingsReader = listingsReader;
            ListingsPathProvider = listingsPathProvider;
        }
        
        public IObservable<IChangeSet<IModListingGetter>> Get(out IObservable<ErrorResponse> state)
        {
            var path = ListingsPathProvider.Path;
            if (path == null)
            {
                state = Observable.Return(ErrorResponse.Success);
                return Observable.Empty<IChangeSet<IModListingGetter>>();
            }
            var raw = ObservableExt.WatchFile(path.Value, fileWatcherFactory: _fileSystem.FileSystemWatcher, throwIfInvalidPath: true)
                .StartWith(Unit.Default)
                .Select(_ =>
                {
                    try
                    {
                        return GetResponse<IObservable<IChangeSet<IModListingGetter>>>.Succeed(
                            ListingsReader.Read(_fileSystem.File.OpenRead(path.Value))
                                .AsObservableChangeSet());
                    }
                    catch (Exception ex)
                    {
                        return GetResponse<IObservable<IChangeSet<IModListingGetter>>>.Fail(ex);
                    }
                });
            state = raw
                .Select(r => (ErrorResponse)r);
            return raw
                .Select(r =>
                {
                    return r.Value ?? Observable.Empty<IChangeSet<IModListingGetter>>();
                })
                .Switch();
        }
    }
}