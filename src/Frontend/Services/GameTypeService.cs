using CodeBreaker.Services.Games.Transfer.Api.Responses;
using System.Net.Http.Json;

namespace CodeBreaker.Frontend.Services;

public class GameTypeService(HttpClient httpClient)
{
    public async Task<GetGameTypeResponse> GetGameTypeAsync(string gameTypeName, CancellationToken cancellationToken = default)
    {
        var res = await httpClient.GetFromJsonAsync<GetGameTypeResponse>($"/gametypes/{gameTypeName}", cancellationToken);
        return res!;
    }
}
