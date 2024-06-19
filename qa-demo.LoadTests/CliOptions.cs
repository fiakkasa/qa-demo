namespace qa_demo.LoadTests;

public record CliOptions
{
    [Option('u', "url", Required = true, HelpText = "Set the base url")]
    public required Uri Url { get; set; }

    [Option("reportdir", Required = false, HelpText = "Set the reports directory. Defaults to ./TestResults")]
    public string ReportDir { get; set; } = "./TestResults";
}
