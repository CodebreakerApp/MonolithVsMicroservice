namespace CodeBreaker.Services.Bot.Runner.Models.GameTypes;

public class GameType6x4 : GameType
{
    public override TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor) =>
        visitor.Visit(this);
}