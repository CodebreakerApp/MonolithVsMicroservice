namespace CodeBreaker.Services.Bot.Data.Models.Extensions;

internal static class BotStateExtensions
{
    public static bool HasEnded(this BotState botState) =>
        botState == BotState.Ended ||
        botState == BotState.Failed;
}
