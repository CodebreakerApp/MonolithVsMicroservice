namespace CodeBreaker.Backend.SignalRHubs.Models;

public record class GameEndedPayload(
    int GameId,
    bool Won,
    DateTime End
);