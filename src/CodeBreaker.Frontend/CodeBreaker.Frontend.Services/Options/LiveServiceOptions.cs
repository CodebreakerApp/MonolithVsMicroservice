namespace CodeBreaker.Frontend.Services.Options;

public class LiveServiceOptions
{
    public required string Url { get; set; }

    public bool AutomaticReconnect { get; set; } = true;
}
