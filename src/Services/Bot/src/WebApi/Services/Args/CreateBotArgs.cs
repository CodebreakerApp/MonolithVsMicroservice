namespace CodeBreaker.Services.Bot.WebApi.Services.Args;

internal record class CreateBotArgs(string Name, string BotType, string GameType, int ThinkTime);