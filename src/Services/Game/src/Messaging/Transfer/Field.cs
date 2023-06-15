using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Transfer;

[MemoryPackable]
public partial class Field
{
    public string? Color { get; init; }

    public string? Shape { get; init; }
}