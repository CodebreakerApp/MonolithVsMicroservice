using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;
using CodeBreaker.Services.Games.Data.Models.GameTypes;
using CodeBreaker.Services.Games.Data.Models.Visitors;
using CodeBreaker.Services.Games.Messaging.Transfer.Payloads;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Games.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class MessagingTransferMapping
{
    public static partial GameCreatedPayload ToGameCreatedPayload(this Game game);

    public static partial GameEndedPayload ToGameEndedPayload(this Game game);

    public static MoveCreatedPayload ToMoveCreatedPayload(this Move move, Guid gameId) =>
        new()
        {
            GameId = gameId,
            MoveId = move.Id,
            Fields = move.Fields.Select(ToTransfer).ToArray(),
            KeyPegs = move.KeyPegs?.Select(ToTransfer).ToArray() ?? Array.Empty<string>(),
            CreatedAt = move.CreatedAt,
        };

    private static string ToTransfer(this GameType gameType) => gameType.GetName();

    private static Messaging.Transfer.Field ToTransfer(this Field field) => field.Accept(new MessagingFieldTransferMappingVisitor());

    private static string ToTransfer(KeyPeg keyPeg) => Enum.GetName(keyPeg) ?? throw new InvalidOperationException("The given keypeg value does not exist");
}
