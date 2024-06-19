using System.Net;

namespace qa_demo.Pages.Tests;

public class IndexPageTests(PagesFixture fixture) : IClassFixture<PagesFixture>
{
    [Fact]
    public async Task IndexPage_Should_Render()
    {
        var result = await fixture.Client.GetAsync("/");

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var (html, htmlDoc) = await result.GetHtmlAssets();

        htmlDoc.DocumentNode.SelectSingleNode("//html/head/title").InnerText.Should().Be("Home page - QA Demo");

        html.MatchSnapshot();
    }
}
