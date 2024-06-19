using System.Net;

namespace qa_demo.Pages.Tests;

public class PrivacyPageTests(PagesFixture fixture) : IClassFixture<PagesFixture>
{
    [Fact]
    public async Task PrivacyPage_Should_Render()
    {
        var result = await fixture.Client.GetAsync("/privacy");

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var (html, htmlDoc) = await result.GetHtmlAssets();

        htmlDoc.DocumentNode.SelectSingleNode("//html/head/title").InnerText.Should().Be("Privacy Policy - QA Demo");

        html.MatchSnapshot();
    }
}
