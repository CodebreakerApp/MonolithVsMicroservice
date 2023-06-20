namespace CodeBreaker.Services.Bot.Runner.Models.GameTypes;

public class GameType8x5 : GameType
{
    public override TResult Accept<TResult>(IGameTypeVisitor<TResult> visitor) =>
        visitor.Visit(this);
}