using System.Text;

using Microsoft.Extensions.Configuration;

namespace qa_demo.Tests.TestingExtensions;

public static class ConfigExtensions
{
    public static T AddToConfigBuilder<T>(this T builder, string config) where T : IConfigurationBuilder
    {
        builder.AddJsonStream(
            new MemoryStream(
                Encoding.UTF8.GetBytes(config)
            )
        );

        return builder;
    }
}
