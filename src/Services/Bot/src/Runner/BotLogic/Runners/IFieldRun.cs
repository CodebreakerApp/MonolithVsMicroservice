using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Models.Fields;
using System.Runtime.CompilerServices;

namespace CodeBreaker.Services.Bot.Runner.BotLogic.Runners;
public interface IFieldRun
{
    List<Field> HintedFields { get; }

    IAsyncEnumerable<Field> RunAsync(IReadOnlyList<KeyPeg> prevKeyPegs, int remainingAttempts, [EnumeratorCancellation] CancellationToken cancellationToken = default);
}