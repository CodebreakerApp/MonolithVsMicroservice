namespace CodeBreaker.Services.Report.MessageWorker.Options;

internal class MessageServiceOptions
{
    public int StopAfterSecondsOfNoMessage { get; set; } = 60;
}
