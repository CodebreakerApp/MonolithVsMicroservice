using CodeBreaker.Services.Bot.Runner.Models.Fields;

namespace CodeBreaker.Services.Bot.Runner.Models;

public class Move
{
    public required IReadOnlyList<Field> Fields { get; init; }

    public required IReadOnlyList<KeyPeg> KeyPegs { get; set; }
}