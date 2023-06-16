namespace CodeBreaker.Services.Games.Data.Models;

public enum GameState : byte
{
    Active = 1,
    Won = 2,
    Lost = 3,
    Cancelled = 4,
    Orphaned = 5
}