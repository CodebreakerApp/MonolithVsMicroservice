using CodeBreaker.Services.Games.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeBreaker.Services.Games.Data.DatabaseContexts.Converters;

public class KeyPegsConverter : ValueConverter<IReadOnlyList<KeyPeg>?, string?>
{
    private static readonly JsonSerializerOptions _jsonOptions = new();

    static KeyPegsConverter() =>
        _jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));

    public KeyPegsConverter() : base
    (
        keyPegs => keyPegs == null
            ? null
            : JsonSerializer.Serialize(keyPegs, _jsonOptions),
        keyPegsString => keyPegsString == null
            ? null
            : JsonSerializer.Deserialize<KeyPeg[]>(keyPegsString, _jsonOptions)
    )
    { }
}
