using CodeBreaker.Services.Bot.Transfer.Api.Requests;
using CodeBreaker.Services.Bot.Transfer.Api.Responses;
using System.Text.Json.Serialization;

namespace CodeBreaker.Services.Bot.WebApi.Serialization;

[JsonSerializable(typeof(GetBotsRequest))]
[JsonSerializable(typeof(GetBotResponse))]
[JsonSerializable(typeof(CreateBotRequest))]
[JsonSerializable(typeof(GetBotsResponse))]
[JsonSerializable(typeof(CreateBotResponse))]
internal partial class ApiJsonSerializerContext : JsonSerializerContext
{
}
