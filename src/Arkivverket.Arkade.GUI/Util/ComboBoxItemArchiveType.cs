using Arkivverket.Arkade.Core.Base.Archives;

namespace Arkivverket.Arkade.GUI.Util
{
    public class ComboBoxItemArchiveType(ArchiveType archiveType)
    {
        public ArchiveType ArchiveType { get; } = archiveType;
        public string Token { get; } = archiveType.GetToken();
    }
}
