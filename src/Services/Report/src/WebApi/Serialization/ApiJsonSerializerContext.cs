using System.Text.Json.Serialization;

namespace CodeBreaker.Services.Report.Serialization;

[JsonSerializable(typeof(Transfer.Api.Game))]
[JsonSerializable(typeof(Transfer.Api.Requests.GetStatisticsRequest))]
[JsonSerializable(typeof(Transfer.Api.Requests.GetGamesRequest))]
[JsonSerializable(typeof(Transfer.Api.Responses.GetGameResponse))]
[JsonSerializable(typeof(Transfer.Api.Responses.GetGamesResponse))]
[JsonSerializable(typeof(Transfer.Api.Responses.GetStatisticsResponse))]
internal partial class ApiJsonSerializerContext : JsonSerializerContext { }