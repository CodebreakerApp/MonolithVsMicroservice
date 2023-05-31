using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using System.Runtime.CompilerServices;

namespace CodeBreaker.Backend.BotLogic.Runners;
public interface IFieldRun
{
    List<Field> HintedFields { get; }

    IAsyncEnumerable<Field> RunAsync(IReadOnlyList<KeyPeg> prevKeyPegs, [EnumeratorCancellation] CancellationToken cancellationToken = default);
}