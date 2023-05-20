using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBreaker.Backend.Data.DatabaseContexts.Converters;

public class GameTypeNameConverter : ValueConverter<GameType, string>
{
    public GameTypeNameConverter() : base
    (
        gameType => gameType.GetName(),
        name => new GameTypeRepository().GetGameType(name)
    ) { }
}
