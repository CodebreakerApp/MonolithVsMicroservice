using CodeBreaker.Services.Bot.Data.Models;

namespace CodeBreaker.Services.Bot.Visitors;

internal class BotNameVisitor : IBotVisitor<string>
{
    public string Visit(SimpleBot bot) => "simple";
}

internal static class BotVisitorExtensions
{
    public static string GetName(this Data.Models.Bot bot) => bot.Accept(new BotNameVisitor());
}