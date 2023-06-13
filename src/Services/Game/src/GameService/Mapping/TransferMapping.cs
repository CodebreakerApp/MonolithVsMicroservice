using CodeBreaker.Services.Games.Data.Models;
using CodeBreaker.Services.Games.Data.Models.Fields;
using CodeBreaker.Services.Games.Data.Models.GameTypes;
using CodeBreaker.Services.Games.Data.Models.Visitors;
using Riok.Mapperly.Abstractions;

namespace CodeBreaker.Services.Games.Mapping;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName, EnabledConversions = MappingConversionType.All & ~MappingConversionType.ToStringMethod)]
internal static partial class TransferMapping
{
    public static partial Transfer.Api.Game ToTransfer(this Game model);

    public static partial Transfer.Api.GameWithCode ToTransferWithCode(this Game model);

    public static partial Transfer.Api.Move ToTransfer(this Move move);

    public static partial Move ToModel(this Transfer.Api.Move transfer);

    public static Field ToModel(this Transfer.Api.Field transfer)
    {
        if (transfer.Shape != null && transfer.Color != null)
            return new ColorShapeField(Enum.Parse<FieldColor>(transfer.Color), Enum.Parse<FieldShape>(transfer.Shape));

        if (transfer.Color != null)
            return new ColorField(Enum.Parse<FieldColor>(transfer.Color));

        throw new InvalidOperationException("Invalid field");
    }

    public static IReadOnlyList<Field> ToModel(this IReadOnlyList<Transfer.Api.Field> transfer) =>
        transfer.Select(field => field.ToModel()).ToList();

    public static Transfer.Api.Field ToTransfer(this Field field) => field.Accept(new FieldTransferMappingVisitor());

    public static Transfer.Api.GameType ToTransfer(this GameType gameType) =>
        new()
        {
            Name = gameType.GetName(),
            Holes = gameType.Holes,
            MaxMoves = gameType.MaxMoves,
            PossibleFields = gameType.PossibleFields.Select(x => x.ToTransfer()),
        };

    public static partial IReadOnlyList<Transfer.Api.GameType> ToTransfer(this IReadOnlyList<GameType> gameTypes);

    public static string ToName(this GameType gameType) => gameType.GetName();

    public static string ToTransfer(this KeyPeg keyPeg) => Enum.GetName(keyPeg) ?? throw new InvalidOperationException("The given keypeg value does not exist");

    public static partial IReadOnlyList<string> ToTransfer(this IReadOnlyList<KeyPeg> keyPegs);
}