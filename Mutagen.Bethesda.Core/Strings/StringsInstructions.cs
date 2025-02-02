using Noggog;
using System.Collections.Generic;
using Mutagen.Bethesda.Strings.DI;

namespace Mutagen.Bethesda.Strings
{
    /// <summary>
    /// Class to specify Strings file usage when importing a mod
    /// </summary>
    public class StringsReadParameters
    {
        /// <summary>
        /// If specified, normal string folder path locations will be ignored in favor of the path provided.
        /// </summary>
        public DirectoryPath? StringsFolderOverride;

        /// <summary>
        /// How to order BSAs when searching for strings.<br/>
        /// Null is the default, which will fall back on typical ini files for the bsa ordering.<br/>
        /// Otherwise, given order will be treated like other load order concepts.  Later entries override earlier entries.
        /// </summary>
        public IEnumerable<FileName>? BsaOrdering;

        /// <summary>
        /// The object to retrieve the encodings to be used
        /// </summary>
        public IMutagenEncodingProvider? EncodingProvider;
    }
}
