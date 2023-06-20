using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Models.Fields;
using CodeBreaker.Services.Bot.Runner.Models.GameTypes;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Bot.Runner.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class ApiTransferMapper
{
    [MapperIgnoreTarget(nameof(Game.Type))]
    private static partial Game ToModelInternal(this Games.Transfer.Api.Game game);

    public static Game ToModel(this Games.Transfer.Api.Game transfer, GameType gameType)
    {
        var game = transfer.ToModelInternal();
        game.Type = gameType;
        return game;
    }

    public static Field ToModel(this Games.Transfer.Api.Field transfer) => FieldMapper.ToModel(transfer);

    public static Games.Transfer.Api.Field ToTransfer(this Field field) => field.Accept(new ApiFieldTransferMappingVisitor());

    public static partial IReadOnlyList<Games.Transfer.Api.Field> ToTransfer(this IReadOnlyList<Field> fields);

    public static partial IReadOnlyList<KeyPeg> ToModel(this IReadOnlyList<string> keyPegStrings);
}