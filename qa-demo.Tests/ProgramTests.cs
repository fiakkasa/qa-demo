using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace qa_demo.Tests;

public class ProgramTests
{
    internal class Waf(string environment) : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();

                config.AddToConfigBuilder(
"""
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
"""
                );
            });

            builder.UseEnvironment(environment);

            return base.CreateHost(builder);
        }
    }

    [Fact]
    public async Task Program_Should_Run_In_Release_Mode()
    {
        using var app = new Waf("Release");

        var client = app.CreateClient();

        var indexResult = await client.GetAsync("/");

        indexResult.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Program_Should_Run_In_Development_Mode()
    {
        using var app = new Waf("Development");

        var client = app.CreateClient();

        var indexResult = await client.GetAsync("/");

        indexResult.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}