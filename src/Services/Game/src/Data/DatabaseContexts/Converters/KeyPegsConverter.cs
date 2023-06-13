using CodeBreaker.Services.Games.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeBreaker.Services.Games.Data.DatabaseContexts.Converters;

public class KeyPegsConverter : ValueConverter<IReadOnlyList<KeyPeg>?, string?>
{
    public KeyPegsConverter() : base
    (
        keyPegs => keyPegs == null
            ? null
            : string.Join('.', keyPegs.Select(keyPeg => Enum.GetName(keyPeg))),
        keyPegsString => keyPegsString == null
            ? null
            : keyPegsString
            .Split('.', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(keyPegName => Enum.Parse<KeyPeg>(keyPegName))
            .ToArray()
    )
    { }
}
