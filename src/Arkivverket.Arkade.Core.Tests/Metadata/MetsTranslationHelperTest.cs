using Arkivverket.Arkade.Core.Metadata;
using FluentAssertions;
using Xunit;

namespace Arkivverket.Arkade.Core.Tests.Metadata;

public class MetsTranslationHelperTest
{
    [Theory]
    [InlineData("Fagsystem", "Fagsystem")]
    [InlineData("SpecializedSystem", "Fagsystem")]
    [InlineData("noark5", "Noark5")]
    [InlineData("Some other type", "Some other type")]
    public void SystemTypeIsNormalizedToToken(string systemType, string expectedSystemType)
    {
        MetsTranslationHelper.NormalizeSystemType(systemType).Should().Be(expectedSystemType);
    }

    [Fact]
    public void SpecializedSystemTokenIsAValidSystemType()
    {
        MetsTranslationHelper.IsValidSystemType("Fagsystem").Should().BeTrue();
    }
}
