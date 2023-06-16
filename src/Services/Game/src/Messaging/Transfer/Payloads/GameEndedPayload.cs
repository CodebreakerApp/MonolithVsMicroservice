using MemoryPack;

namespace CodeBreaker.Services.Games.Messaging.Transfer.Payloads;

[MemoryPackable]
public partial class GameEndedPayload
{
    private readonly Guid _id;

    private readonly DateTime _end;

    private readonly GameState _state;

    public required Guid Id
    {
        get => _id;
        init
        {
            if (value == default)
                throw new ArgumentOutOfRangeException(nameof(Id), "Must not be default.");

            _id = value;
        }
    }

    public required DateTime End
    {
        get => _end;
        init
        {
            if (value == default)
                throw new ArgumentOutOfRangeException(nameof(End), "Must not be default.");

            _end = value;
        }
    }

    public required GameState State
    {
        get => _state;
        init
        {
            if (value == GameState.Active)
                throw new ArgumentOutOfRangeException(nameof(State), $"{nameof(State)} must not be {nameof(GameState.Active)}, when the game ended.");

            _state = value;
        }
    }
}