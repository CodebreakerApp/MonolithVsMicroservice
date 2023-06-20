namespace CodeBreaker.Services.Bot.Runner.Options;

internal class BotScheduleWorkerOptions
{
    public int StopAfterSecondsOfNoMessage { get; init; } = 60;
}
