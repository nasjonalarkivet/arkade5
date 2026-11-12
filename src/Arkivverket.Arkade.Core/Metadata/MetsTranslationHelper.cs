using Arkivverket.Arkade.Core.Base;
using Arkivverket.Arkade.Core.Base.Archives;

namespace Arkivverket.Arkade.Core.Metadata
{
    public static class MetsTranslationHelper
    {
        public static bool IsValidSystemType(string systemType)
        {
            return ArchiveTypeNames.TryParseToken(systemType, out _);
        }

        public static bool IsSystemTypeNoark5(string systemType)
        {
            return ArchiveTypeNames.TryParseToken(systemType, out ArchiveType archiveType)
                   && archiveType == ArchiveType.Noark5;
        }

        public static string NormalizeSystemType(string systemType)
        {
            return ArchiveTypeNames.TryParseToken(systemType, out ArchiveType archiveType)
                ? archiveType.GetToken()
                : systemType;
        }
    }
}
