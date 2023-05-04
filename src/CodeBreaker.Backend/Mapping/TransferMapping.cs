using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Data.Models.KeyPegs;
using CodeBreaker.Backend.Visitors;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Backend.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class TransferMapping
{
    public static partial Transfer.Game ToTransfer(this Game model);

    public static partial Transfer.GameWithCode ToTransferWithCode(this Game model);

    public static partial Transfer.Move ToTransfer(this Move move);

    public static partial Move ToModel(this Transfer.Move transfer);

    public static Transfer.Field ToTransfer(this Field field) => field.Accept(new FieldTransferMappingVisitor());

    public static Transfer.GameType ToTransfer(this GameType gameType) =>
        new()
        {
            Name = gameType.GetName(),
            Holes = gameType.Holes,
            MaxMoves = gameType.MaxMoves,
            PossibleFields = gameType.PossibleFields.Select(x => x.ToTransfer()),
        };

    public static partial IEnumerable<Transfer.GameType> ToTransfer(this IEnumerable<GameType> gameTypes);

    public static string ToName(this GameType gameType) => gameType.GetName();

    public static string ToTransfer(this KeyPeg keyPeg) => keyPeg.GetName();

    public static partial IEnumerable<string> ToTransfer(this IEnumerable<KeyPeg> keyPegs);
}