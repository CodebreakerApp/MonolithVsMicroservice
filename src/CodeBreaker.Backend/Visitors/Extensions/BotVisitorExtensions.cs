using CodeBreaker.Backend.Visitors;

namespace CodeBreaker.Backend.Data.Models.Bots;

public static class BotVisitorExtensions
{
    public static string GetName(this Bot bot) => bot.Accept(new BotNameVisitor());
}