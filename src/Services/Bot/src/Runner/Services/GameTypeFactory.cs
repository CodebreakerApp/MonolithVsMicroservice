using CodeBreaker.Services.Bot.Runner.Mapping;
using CodeBreaker.Services.Bot.Runner.Models.Fields;
using CodeBreaker.Services.Bot.Runner.Models.GameTypes;
using CodeBreaker.Services.Games.Transfer.Api.Responses;
using Riok.Mapperly.Abstractions;
using System.Collections.Frozen;
using System.Net.Http.Json;

namespace CodeBreaker.Services.Bot.Runner.Services;

internal class GameTypeService(HttpClient httpClient) : IGameTypeService
{
    private static readonly FrozenDictionary<string, Func<Games.Transfer.Api.GameType, GameType>> s_factoryMapping = new Dictionary<string, Func<Games.Transfer.Api.GameType, GameType>>()
    {
        { "6x4", GameTypeMapper.ToGameType6x4 },
        { "8x5", GameTypeMapper.ToGameType8x5 },
    }
    .ToFrozenDictionary();

    public async Task<GameType> FetchGameTypeAsync(string gameTypeName, CancellationToken cancellationToken)
    {
        var result = await httpClient.GetFromJsonAsync<GetGameTypeResponse>($"/gametypes/{gameTypeName}", cancellationToken);
        var apiGameType = result?.GameType ?? throw new InvalidOperationException("Could not deserialize gametype received from the api.");
        return s_factoryMapping[apiGameType.Name]?.Invoke(apiGameType) ?? throw new ArgumentOutOfRangeException("GameType not found");
    }
}


[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class GameTypeMapper
{
    public static partial GameType6x4 ToGameType6x4(Games.Transfer.Api.GameType transfer);

    public static partial GameType8x5 ToGameType8x5(Games.Transfer.Api.GameType transfer);

    private static Field ToModel(Games.Transfer.Api.Field transfer) => FieldMapper.ToModel(transfer);
}