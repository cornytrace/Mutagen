using Loqui;
using Mutagen.Bethesda.Plugins.Allocators;
using Noggog;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Mutagen.Bethesda.Plugins.Binary.Parameters;

namespace Mutagen.Bethesda.Plugins.Records
{
    /// <summary>
    /// An interface that Mod objects implement to hook into the common getter systems
    /// </summary>
    public interface IModGetter : 
        IMajorRecordGetterEnumerable,
        IMajorRecordSimpleContextEnumerable,
        IFormLinkContainerGetter, 
        IModKeyed, 
        IEqualsMask
    {
        /// <summary>
        /// The associated game release
        /// </summary>
        GameRelease GameRelease { get; }

        /// <summary>
        /// Read only list of master reference getters.
        /// </summary>
        /// <returns>Read only list of master reference getters</returns>
        IReadOnlyList<IMasterReferenceGetter> MasterReferences { get; }

        /// <summary>
        /// Returns the top-level Group getter object associated with the given Major Record Type.
        /// </summary>
        /// <returns>Group getter object associated with the given Major Record Type</returns>
        /// <typeparam name="TMajor">The type of Major Record to get the Group for</typeparam>
        /// <exception cref="ArgumentException">
        /// An unexpected TMajor type will throw an exception.<br />
        /// Unexpected types include: <br />
        ///   - Major Record Types that are not part of this game type.  (Querying for Oblivion records on a Skyrim mod) <br />
        ///   - Nested types, where there is not just one top level group that contains given type (Placed Objects) <br />
        ///   - A setter type is requested from a getter only object. <br />
        /// </exception>
        IGroupCommonGetter<TMajor> GetTopLevelGroup<TMajor>() where TMajor : IMajorRecordCommonGetter;

        /// <summary>
        /// Returns the top-level Group getter object associated with the given Major Record Type.
        /// </summary>
        /// <returns>Group getter object associated with the given Major Record Type</returns>
        /// <param name="type">The type of Major Record to get the Group for</param>
        /// <exception cref="ArgumentException">
        /// An unexpected TMajor type will throw an exception.<br />
        /// Unexpected types include: <br />
        ///   - Major Record Types that are not part of this game type.  (Querying for Oblivion records on a Skyrim mod) <br />
        ///   - Nested types, where there is not just one top level group that contains given type (Placed Objects) <br />
        ///   - A setter type is requested from a getter only object. <br />
        /// </exception>
        IGroupCommonGetter<IMajorRecordCommonGetter> GetTopLevelGroup(Type type);

        /// <summary>
        /// Exports to disk in Bethesda binary format.
        /// Access and iterates through the mod object's contents in a single thread.
        /// </summary>
        /// <param name="path">Path to export to</param>
        /// <param name="param">Optional customization parameters</param>
        /// <param name="fileSystem">Optional filesystem substitution</param>
        void WriteToBinary(FilePath path, BinaryWriteParameters? param = null, IFileSystem? fileSystem = null);

        /// <summary>
        /// Exports to disk in Bethesda binary format.
        /// Access and iterates through the mod groups in separate threads.  All provided mod objects
        /// are thread safe to use with this function.
        /// </summary>
        /// <param name="path">Path to export to</param>
        /// <param name="param">Optional customization parameters</param>
        /// <param name="fileSystem">Optional filesystem substitution</param>
        void WriteToBinaryParallel(FilePath path, BinaryWriteParameters? param = null, IFileSystem? fileSystem = null);

        /// <summary>
        /// Whether a mod supports localization features
        /// </summary>
        bool CanUseLocalization { get; }

        /// <summary>
        /// Whether a mod has localization enabled
        /// </summary>
        bool UsingLocalization { get; }

        /// <summary>
        /// The next FormID to be allocated
        /// </summary>
        uint NextFormID { get; }
    }

    /// <summary>
    /// An interface that Mod objects implement to hook into the common systems
    /// </summary>
    public interface IMod : IModGetter, IMajorRecordEnumerable, IFormKeyAllocator, IFormLinkContainer
    {
        /// <summary>
        /// List of master references.
        /// </summary>
        /// <returns>List of master references</returns>
        new IList<MasterReference> MasterReferences { get; }

        /// <summary>
        /// Returns the Group object associated with the given Major Record Type.
        /// </summary>
        /// <returns>Group object associated with the given Major Record Type</returns>
        /// <typeparam name="TMajor">The type of Major Record to get the Group for</typeparam>
        /// <exception cref="ArgumentException">
        /// An unexpected TMajor type will throw an exception.
        /// Unexpected types include:
        ///   - Major Record Types that are not part of this game type.  (Querying for Oblivion records on a Skyrim mod)
        ///   - A setter type is requested from a getter only object.
        /// </exception>
        new IGroupCommon<TMajor> GetTopLevelGroup<TMajor>() where TMajor : IMajorRecordCommon;

        /// <summary>
        /// The next FormID to be allocated
        /// </summary>
        new uint NextFormID { get; set; }

        /// <summary>
        /// Whether a mod has localization enabled
        /// </summary>
        new bool UsingLocalization { get; set; }

        /// <summary>
        /// Assigns a new allocator to the mod.  This will be used whenever a new FormKey is requested from the mod.
        /// </summary>
        /// <param name="allocator">Allocator to use</param>
        /// <typeparam name="TAlloc">Allocator type</typeparam>
        /// <returns>The same Allocator given is returned</returns>
        TAlloc SetAllocator<TAlloc>(TAlloc allocator)
            where TAlloc : IFormKeyAllocator;
    }

    /// <summary>
    /// An interface for Overlay mod systems
    /// </summary>
    public interface IModDisposeGetter : IModGetter, IDisposable
    {
    }
}
