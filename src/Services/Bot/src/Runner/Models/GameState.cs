namespace CodeBreaker.Services.Bot.Runner.Models;

public enum GameState : byte
{
    Active = 0,
    Won = 1,
    Lost = 2,
    Cancelled = 3,
    Orphaned = 4,
}