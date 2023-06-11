using System.Text.Json.Serialization;

namespace CodeBreaker.Backend.Serialization;

[JsonSerializable(typeof(Transfer.Game))]
[JsonSerializable(typeof(Transfer.Bot))]
[JsonSerializable(typeof(Transfer.GameType))]
[JsonSerializable(typeof(Transfer.Requests.CreateGameRequest))]
[JsonSerializable(typeof(Transfer.Requests.CreateMoveRequest))]
[JsonSerializable(typeof(Transfer.Requests.CreateBotRequest))]
[JsonSerializable(typeof(Transfer.Requests.GetStatisticsRequest))]
[JsonSerializable(typeof(Transfer.Requests.GetReportGamesRequest))]
[JsonSerializable(typeof(Transfer.Responses.CreateGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.CreateMoveResponse))]
[JsonSerializable(typeof(Transfer.Responses.CreateBotResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetBotTypesResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGamesResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameTypeResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameTypesResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetStatisticsResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetReportGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetReportGamesResponse))]
internal partial class CodebreakerJsonSerializerContext : JsonSerializerContext { }
