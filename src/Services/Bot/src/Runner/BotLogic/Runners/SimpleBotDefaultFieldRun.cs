using CodeBreaker.Services.Bot.Runner.Models.Bots;
using CodeBreaker.Services.Bot.Runner.Models;
using CodeBreaker.Services.Bot.Runner.Services;
using System.Runtime.CompilerServices;
using CodeBreaker.Services.Bot.Runner.Models.Fields;
using CodeBreaker.Services.Bot.Runner.BotLogic.Exceptions;
using CodeBreaker.Services.Bot.Runner.Models.Extensions;

namespace CodeBreaker.Services.Bot.Runner.BotLogic.Runners;

/// <summary>
/// This cluss represents a run for determining the fields.
/// </summary>
/// <param name="moveService">The moveservice, used to make moves.</param>
/// <param name="bot">The bot for which the run is made.</param>
/// <param name="slots">The slots/fields to work with. At the end, the slots represent the result.</param>
/// <param name="index">The index of this run. 0 ... n</param>
/// <param name="nextRun">The run for the next field in the <paramref name="slots"/>.</param>
internal class SimpleBotDefaultFieldRun(IMoveService moveService, SimpleBot bot, Field[] slots, int index, IFieldRun? nextRun) : IFieldRun
{
    /// <summary>
    /// Gets the hinted fields.
    /// </summary>
    /// <value>
    /// The fields, likely to occur in subsequent runs.
    /// </value>
    public List<Field> HintedFields { get; } = new();

    /// <summary>
    /// Gets the game.
    /// </summary>
    /// <value>
    /// The game to play.
    /// </value>
    protected Game Game => bot.Game!;

    /// <summary>
    /// Gets the tried fields.
    /// </summary>
    /// <value>
    /// The fields already tried by this run.
    /// </value>
    protected List<Field> TriedFields { get; } = new();

    /// <summary>
    /// Gets the possible fields.
    /// </summary>
    /// <value>
    /// The possible fields to pick.
    /// </value>
    /// <remarks>
    /// Hinted fields are priorised.<br/>
    /// Already tried fields are not used.
    /// </remarks>
    protected IEnumerable<Field> PossibleFields =>
        Game.Type.PossibleFields
        .OrderByDescending(HintedFields.Contains)   // Use the hinted fields as first
        .Except(TriedFields)
        .Distinct();

    /// <summary>
    /// Determines the field for this run and recursively starts the next run.
    /// </summary>
    /// <param name="prevKeyPegs">The previous key pegs.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The fields determined by this run and all subsequent runs.</returns>
    public async IAsyncEnumerable<Field> RunAsync(IReadOnlyList<KeyPeg> prevKeyPegs, int remainingAttempts, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        IReadOnlyList<KeyPeg> currentKeyPegs = prevKeyPegs;
        bool exit = false;
        Field previousField;

        foreach (var field in PossibleFields)
        {
            remainingAttempts--;
            previousField = slots[index];
            slots[index] = field;

            await moveService.ApplyMoveAsync(Game, slots, cancellationToken);

            if (remainingAttempts == 0)
            {
                Game.State = GameState.Lost;
                throw new GameEndedException(Game);
            }

            currentKeyPegs = Game.Moves.LastOrDefault()?.KeyPegs ?? throw new InvalidOperationException("The move was made, but no keypegs were returned.");
            (int blackDiff, int whiteDiff) = GetDiff(prevKeyPegs, currentKeyPegs);

            if (blackDiff > 0)
            {
                // the found field was no hinted field -> put the hints to the next run
                if (!HintedFields.Contains(field))
                    nextRun?.HintedFields.AddRange(HintedFields);

                // black key peg found = goal for this run
                yield return field;
                exit = true;
            }
            else if (blackDiff < 0)
            {
                // black key peg lost
                // ... the previous key peg was correct for this run
                currentKeyPegs = prevKeyPegs;
                slots[index] = previousField;
                yield return previousField;
                exit = true;
            }

            if (whiteDiff > 0)
            {
                // white key peg found
                // "field" will occur in one of the subsequent fields/slots
                nextRun?.HintedFields.Add(field);
            }

            TriedFields.Add(field);
            prevKeyPegs = currentKeyPegs;

            if (exit)
                break;
            else
                await Task.Delay(bot.ThinkTime);
        }

        await foreach (var item in nextRun?.RunAsync(currentKeyPegs, remainingAttempts, cancellationToken) ?? AsyncEnumerable.Empty<Field>())
            yield return item;
    }

    private (int blackDiff, int whiteDiff) GetDiff(IReadOnlyList<KeyPeg> a, IReadOnlyList<KeyPeg> b) =>
        (
            b.Count(x => x == KeyPeg.Black) - a.Count(x => x == KeyPeg.Black),
            b.Count(x => x == KeyPeg.White) - a.Count(x => x == KeyPeg.White)
        );
}
