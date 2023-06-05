using System.Diagnostics.CodeAnalysis;

namespace CodeBreaker.Transfer.Requests;

public class GetStatisticsRequest : IParsable<GetStatisticsRequest>
{
    public required DateTime From { get; init; }

    public required DateTime To { get; init; }

    public string? GameType { get; init; }

    public static GetStatisticsRequest Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GetStatisticsRequest result)
    {
        throw new NotImplementedException();
    }
}
