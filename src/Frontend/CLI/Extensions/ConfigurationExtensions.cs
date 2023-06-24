using Microsoft.Extensions.Configuration;

namespace CodeBreaker.CLI.Extensions;

internal static class ConfigurationExtensions
{
    public static string GetRequired(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new InvalidOperationException($"Could not find the configuration with the key \"{key}\".");
}
