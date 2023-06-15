using MemoryPack;

namespace CodeBreaker.Services.Games.Transfer.Messaging;

[MemoryPackable]
public class Field
{
    public string? Color { get; set; }

    public string? Shape { get; set; }
}