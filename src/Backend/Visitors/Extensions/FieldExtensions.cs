using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;

namespace CodeBreaker.Backend.Visitors.Extensions;

public static class FieldExtensions
{
    public static IEnumerable<Field> GetRandomFields(this GameType gameType) =>
        GetRandom(gameType.PossibleFields, gameType.Holes);

    public static IEnumerable<Field> GetRandom(this IReadOnlyList<Field> possibleFields, int count) =>
        Enumerable.Range(0, count)
            .Select(_ => possibleFields.GetRandom());

    public static Field GetRandom(this IReadOnlyList<Field> possibleFields)
    {
        int max = possibleFields.Count;
        int index = Random.Shared.Next(max);
        return possibleFields[index];
    }
}