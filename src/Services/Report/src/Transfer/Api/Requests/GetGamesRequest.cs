namespace CodeBreaker.Services.Report.Transfer.Api.Requests;

public class GetGamesRequest
{
    public DateTime From { get; init; } = DateTime.Now.AddDays(-1);

    public DateTime To { get; init; } = DateTime.Now;

    public int MaxCount { get; init; } = 100;
}
