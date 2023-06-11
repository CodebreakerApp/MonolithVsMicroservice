using CodeBreaker.Transfer.Requests;
using CodeBreaker.Transfer.Responses;
using System.Net.Http.Json;
using System.Text;

namespace CodeBreaker.Frontend.Services;

public class ReportService(HttpClient httpClient)
{
    public async Task<GetStatisticsResponse> GetStatisticsAsync(GetStatisticsRequest req, CancellationToken cancellationToken = default)
    {
        string query = QueryBuilder
            .Create("from", req.From.ToString("yyyy-MM-dd"))
            .Add("to", req.To.ToString("yyyy-MM-dd"))
            .Add("gameType", req.GameType)
            .Query;
        var responseBody = await httpClient.GetFromJsonAsync<GetStatisticsResponse>($"/reports/statistics{query}", cancellationToken);
        return responseBody ?? throw new InvalidOperationException();
    }

    public async Task<GetReportGameResponse> GetGameAsync(int id, CancellationToken cancellationToken = default)
    {
        var responseBody = await httpClient.GetFromJsonAsync<GetReportGameResponse>($"/reports/games/{id}", cancellationToken);
        return responseBody ?? throw new InvalidOperationException();
    }

    public async Task<GetReportGamesResponse> GetGamesAsync(GetReportGamesRequest req, CancellationToken cancellationToken = default)
    {
        string query = QueryBuilder
            .Create("from", req.From.ToString("s"))
            .Add("to", req.To.ToString("s"))
            .Query;
        var responseBody = await httpClient.GetFromJsonAsync<GetReportGamesResponse>($"/reports/games{query}", cancellationToken);
        return responseBody ?? throw new InvalidOperationException();
    }
}

public class QueryBuilder
{
    private readonly StringBuilder stringBuilder = new();

    public string Query => stringBuilder.ToString();

    public static QueryBuilder Create(string name, string? value) =>
        new QueryBuilder().Add(name, value);

    public QueryBuilder Add(string name, string? value)
    {
        if (stringBuilder.Length == 0)
            stringBuilder.Append('?');
        else
            stringBuilder.Append('&');

        stringBuilder.Append($"{name}={value ?? string.Empty}");
        return this;
    }
}