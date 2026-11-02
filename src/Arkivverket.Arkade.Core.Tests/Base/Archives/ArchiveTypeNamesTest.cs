using System.Globalization;
using System.IO;
using System.Text;
using Arkivverket.Arkade.Core.Base.Archives;
using Arkivverket.Arkade.Core.Report;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Core.Tests.Base.Archives;

public class ArchiveTypeNamesTest
{
    [Theory]
    [InlineData(ArchiveType.Noark3, "Noark3")]
    [InlineData(ArchiveType.Noark4, "Noark4")]
    [InlineData(ArchiveType.Noark5, "Noark5")]
    [InlineData(ArchiveType.SpecializedSystem, "Fagsystem")]
    [InlineData(ArchiveType.Siard, "Siard")]
    public void TokenIsTheFixedNameOfTheArchiveType(ArchiveType archiveType, string expectedToken)
    {
        archiveType.GetToken().Should().Be(expectedToken);
        ArchiveTypeNames.TryParseToken(expectedToken, out ArchiveType parsedArchiveType).Should().BeTrue();
        parsedArchiveType.Should().Be(archiveType);
    }

    [Theory]
    [InlineData("fagsystem", ArchiveType.SpecializedSystem)]
    [InlineData("SpecializedSystem", ArchiveType.SpecializedSystem)]
    [InlineData("noark5", ArchiveType.Noark5)]
    [InlineData("SIARD", ArchiveType.Siard)]
    public void TokenIsParsedCaseInsensitivelyAndObsoleteTokenIsAccepted(string token, ArchiveType expectedArchiveType)
    {
        ArchiveTypeNames.TryParseToken(token, out ArchiveType archiveType).Should().BeTrue();
        archiveType.Should().Be(expectedArchiveType);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("3")]
    [InlineData("Noark 5")]
    public void UnknownTokenIsNotParsed(string token)
    {
        ArchiveTypeNames.TryParseToken(token, out _).Should().BeFalse();
    }

    [Theory]
    [InlineData(ArchiveType.Noark3, "en", "Noark 3")]
    [InlineData(ArchiveType.Noark4, "nb-NO", "Noark 4")]
    [InlineData(ArchiveType.Noark5, "nb-NO", "Noark 5")]
    [InlineData(ArchiveType.SpecializedSystem, "en", "Specialized system")]
    [InlineData(ArchiveType.SpecializedSystem, "nb-NO", "Fagsystem")]
    [InlineData(ArchiveType.Siard, "en", "SIARD")]
    public void DisplayNameIsTheOfficialNameInTheGivenLanguage(ArchiveType archiveType, string culture,
        string expectedDisplayName)
    {
        archiveType.GetDisplayName(CultureInfo.GetCultureInfo(culture)).Should().Be(expectedDisplayName);
    }

    [Fact]
    public void SerializedTestReportsNameTheArchiveTypeByToken()
    {
        var testReport = new TestReport
        {
            Summary = new TestReportSummary { ArchiveType = ArchiveType.SpecializedSystem },
            TestsResults = [],
        };

        GenerateReport(new XmlReportGenerator(), testReport).Should().Contain("<ArchiveType>Fagsystem</ArchiveType>");
        GenerateReport(new JsonReportGenerator(), testReport).Should().Contain("\"ArchiveType\": \"Fagsystem\"");
    }

    private static string GenerateReport(IReportGenerator reportGenerator, TestReport testReport)
    {
        using var stream = new MemoryStream();
        reportGenerator.Generate(testReport, stream);
        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
