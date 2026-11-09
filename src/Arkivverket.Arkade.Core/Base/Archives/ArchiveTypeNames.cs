using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Arkivverket.Arkade.Core.Resources;

namespace Arkivverket.Arkade.Core.Base.Archives;

public static class ArchiveTypeNames
{
    // Archive types are named by token in archive metadata (METS), test reports, the test session log
    // and on the command line. Tokens are fixed: information packages already delivered carry them.
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

    public static string GetDisplayName(this ArchiveType archiveType, CultureInfo culture)
    {
        return archiveType switch
        {
            ArchiveType.Noark3 => "Noark-3",
            ArchiveType.Noark4 => "Noark-4",
            ArchiveType.Noark5 => "Noark 5",
            ArchiveType.SpecializedSystem =>
                Messages.ResourceManager.GetString(nameof(Messages.ArchiveTypeSpecializedSystem), culture),
            ArchiveType.Siard => "SIARD",
            _ => throw new ArgumentOutOfRangeException(nameof(archiveType), archiveType, null),
        };
    }
}
