namespace qa_demo.Tests.TestingExtensions;

public static class HtmlExtensions
{
    public static async ValueTask<(string html, HtmlDocument htmlDoc)> GetHtmlAssets(
        this HttpResponseMessage responseMessage, 
        CancellationToken cancellationToken = default
    )
    {
        var html = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        return (html, htmlDoc);
    }
}