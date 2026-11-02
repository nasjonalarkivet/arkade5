using System.Globalization;
using Arkivverket.Arkade.Core.Base.Archives;

namespace Arkivverket.Arkade.GUI.Util
{
    public class ComboBoxItemArchiveType(ArchiveType archiveType, CultureInfo culture)
    {
        public ArchiveType ArchiveType { get; } = archiveType;
        public string Token { get; } = archiveType.GetToken();
        public string DisplayName { get; } = archiveType.GetDisplayName(culture);
    }
}
