using System.Text.Json.Serialization;
using Transfer = CodeBreaker.Services.Games.Transfer.Api;

namespace GameService.Serialization;

[JsonSerializable(typeof(Transfer.Game))]
[JsonSerializable(typeof(Transfer.GameType))]
[JsonSerializable(typeof(Transfer.Requests.CreateGameRequest))]
[JsonSerializable(typeof(Transfer.Requests.CreateMoveRequest))]
[JsonSerializable(typeof(Transfer.Responses.CreateGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.CreateMoveResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGamesResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameTypeResponse))]
[JsonSerializable(typeof(Transfer.Responses.GetGameTypesResponse))]
internal partial class ApiJsonSerializerContext : JsonSerializerContext { }