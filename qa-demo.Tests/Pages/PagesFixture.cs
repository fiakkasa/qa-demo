using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace qa_demo.Pages.Tests;

public class PagesFixture : IDisposable
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

    internal Waf App { get; }
    internal HttpClient Client { get; }

    private bool _disposedValue;

    public PagesFixture()
    {
        App = new Waf("Development");
        Client = App.CreateClient();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        Client.Dispose();
        App.Dispose();

        _disposedValue = true;
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
