using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkivverket.Arkade.Core.Base.Archives;

public static class ArchiveTypeNames
{
    // Archive types are named by token wherever they are shown or written: user interfaces, archive
    // metadata (METS), test reports, the test session log and the command line. Tokens are fixed:
    // information packages already delivered carry them, and receiving archives match on them.
    public const string SpecializedSystemToken = "Fagsystem";

    // Arkade 2.13.0 and 2.13.1 wrote this to METS in place of SpecializedSystemToken
    private const string ObsoleteSpecializedSystemToken = "SpecializedSystem";

    private static readonly Dictionary<string, ArchiveType> ArchiveTypesByToken = new(
        Enum.GetValues<ArchiveType>().Select(archiveType => KeyValuePair.Create(archiveType.GetToken(), archiveType))
            .Append(KeyValuePair.Create(ObsoleteSpecializedSystemToken, ArchiveType.SpecializedSystem)),
        StringComparer.OrdinalIgnoreCase);

    public static string GetToken(this ArchiveType archiveType)
    {
        return archiveType == ArchiveType.SpecializedSystem ? SpecializedSystemToken : archiveType.ToString();
    }

    public static bool TryParseToken(string token, out ArchiveType archiveType)
    {
        archiveType = default;
        return token != null && ArchiveTypesByToken.TryGetValue(token, out archiveType);
    }
}
