namespace GameService.Extensions;

internal static class ConfigurationExtensions
{
    public static string GetRequired(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new InvalidOperationException($"Could not find configuration with key {key}.");
}
