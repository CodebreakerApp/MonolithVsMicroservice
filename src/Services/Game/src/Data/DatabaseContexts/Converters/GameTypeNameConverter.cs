using CodeBreaker.Services.Games.Data.Models.GameTypes;
using CodeBreaker.Services.Games.Data.Models.Visitors;
using CodeBreaker.Services.Games.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBreaker.Services.Games.Data.DatabaseContexts.Converters;

public class GameTypeNameConverter : ValueConverter<GameType, string>
{
    public GameTypeNameConverter() : base
    (
        gameType => gameType.GetName(),
        name => new GameTypeRepository().GetGameType(name)
    )
    { }
}
