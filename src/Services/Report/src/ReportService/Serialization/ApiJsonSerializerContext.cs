using System.Text.Json.Serialization;

namespace CodeBreaker.Services.Report.Serialization;

[JsonSerializable(typeof(string))] // dummy
internal partial class ApiJsonSerializerContext : JsonSerializerContext { }