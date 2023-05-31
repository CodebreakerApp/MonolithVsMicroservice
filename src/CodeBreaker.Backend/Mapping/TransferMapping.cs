using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Bots;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
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

    public static Field ToModel(this Transfer.Field transfer)
    {
        if (transfer.Shape != null && transfer.Color != null)
            return new ColorShapeField(Enum.Parse<FieldColor>(transfer.Color), Enum.Parse<FieldShape>(transfer.Shape));

        if (transfer.Color != null)
            return new ColorField(Enum.Parse<FieldColor>(transfer.Color));

        throw new InvalidOperationException("Invalid field");
    }

    public static Transfer.Field ToTransfer(this Field field) => field.Accept(new FieldTransferMappingVisitor());

    public static Transfer.GameType ToTransfer(this GameType gameType) =>
        new()
        {
            Name = gameType.GetName(),
            Holes = gameType.Holes,
            MaxMoves = gameType.MaxMoves,
            PossibleFields = gameType.PossibleFields.Select(x => x.ToTransfer()),
        };

    public static partial IReadOnlyList<Transfer.GameType> ToTransfer(this IReadOnlyList<GameType> gameTypes);

    public static string ToName(this GameType gameType) => gameType.GetName();

    public static string ToTransfer(this KeyPeg keyPeg) => Enum.GetName(keyPeg) ?? throw new InvalidOperationException("The given keypeg value does not exist");

    public static partial IReadOnlyList<string> ToTransfer(this IReadOnlyList<KeyPeg> keyPegs);

    public static partial Transfer.Bot ToTransfer(this Bot bot);
}