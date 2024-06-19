using System.Net;

namespace qa_demo.Pages.Tests;

public class ErrorPageTests(PagesFixture fixture) : IClassFixture<PagesFixture>
{
    [Fact]
    public async Task ErrorPage_Should_Render()
    {
        var result = await fixture.Client.GetAsync("/error");

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var (html, htmlDoc) = await result.GetHtmlAssets();

        htmlDoc.DocumentNode.SelectSingleNode("//html/head/title").InnerText.Should().Be("Error - QA Demo");
        
        var dangerText = htmlDoc.QuerySelectorAll(".text-danger");
        dangerText.Should().HaveCount(2);
        dangerText[0].InnerText.Should().Be("Error.");
        dangerText[0].Name.Should().Be("h1");
        dangerText[1].InnerText.Should().Be("An error occurred while processing your request.");
        dangerText[1].Name.Should().Be("h2");

        html.MatchSnapshot();
    }
}