namespace CodeBreaker.Services.Bot.WebApi.Services.Args;

public record class GetBotsArgs(DateTime From, DateTime To, int MaxCount);