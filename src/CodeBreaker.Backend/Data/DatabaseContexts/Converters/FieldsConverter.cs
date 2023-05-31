using CodeBreaker.Backend.Data.Models.Fields;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Converters;

internal class FieldsConverter : ValueConverter<IReadOnlyList<Field>, string>
{
    private readonly static JsonSerializerOptions _jsonOptions = new();

    static FieldsConverter()
    {
        _jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        _jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }

    public FieldsConverter() : base
    (
        fields => JsonSerializer.Serialize(fields, _jsonOptions),
        serializedFields => JsonSerializer.Deserialize<Field[]>(serializedFields, _jsonOptions)!
    ) { }
}

internal class ImmutableFieldsConverter : ValueConverter<ImmutableArray<Field>, string>
{
    private readonly static JsonSerializerOptions _jsonOptions = new();
    static ImmutableFieldsConverter()
    {
        _jsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));
        _jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }

    public ImmutableFieldsConverter() : base
    (
        fields => JsonSerializer.Serialize(fields, _jsonOptions),
        serializedFields => JsonSerializer.Deserialize<ImmutableArray<Field>>(serializedFields, _jsonOptions)!
    ) { }
}