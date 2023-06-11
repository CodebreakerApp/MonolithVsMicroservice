namespace CodeBreaker.Transfer.LivePayloads;

public record class GameEndedPayload(
    int GameId,
    bool Won,
    DateTime End
);
