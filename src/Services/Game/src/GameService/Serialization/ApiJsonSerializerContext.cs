using System.Text.Json.Serialization;

namespace GameService.Serialization;

[JsonSerializable(typeof(string))] // TODO remove dummy
internal partial class ApiJsonSerializerContext : JsonSerializerContext { }