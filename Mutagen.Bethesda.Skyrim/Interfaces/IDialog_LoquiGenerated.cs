using Mutagen.Bethesda;

namespace Mutagen.Bethesda.Skyrim
{
    /// <summary>
    /// Implemented by: [DialogResponses, DialogTopic]
    /// </summary>
    public partial interface IDialog :
        ISkyrimMajorRecordInternal,
        IDialogGetter
    {
    }

    /// <summary>
    /// Implemented by: [DialogResponses, DialogTopic]
    /// </summary>
    public partial interface IDialogGetter : ISkyrimMajorRecordGetter
    {
    }
}
