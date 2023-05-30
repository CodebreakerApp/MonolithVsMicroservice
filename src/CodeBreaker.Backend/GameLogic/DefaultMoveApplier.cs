using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;

namespace CodeBreaker.Backend.GameLogic;

public class DefaultMoveApplier : MoveApplier
{
    public DefaultMoveApplier(Game game) : base(game)
    {
    }

    public override Move ApplyMove(in List<Field> guessPegs)
    {
        if (Game.Type.Holes != guessPegs.Count)
            throw new InvalidOperationException($"Invalid number of guesses. Given: {guessPegs.Count}; Required: {Game.Type.Holes}");

        if (Game.Code.Length != guessPegs.Count)
            throw new InvalidOperationException($"The number of pegs in the code ({Game.Code.Length}) does not match the number of guesspegs ({guessPegs.Count})");

        if (guessPegs.Any(guessPeg => !Game.Type.PossibleFields.Contains(guessPeg)))
            throw new InvalidOperationException("The guess contains an invalid value/invalid values");

        List<Field> codeToCheck = Game.Code.ToList(); // copy
        List<Field> guessPegsToCheck = guessPegs.ToList(); // copy
        List<KeyPeg> keyPegs = new();

        // check black
        for (int i = 0; i < guessPegsToCheck.Count; i++)
            if (guessPegsToCheck[i] == codeToCheck[i])
            {
                keyPegs.Add(KeyPeg.Black);
                codeToCheck.RemoveAt(i);
                guessPegsToCheck.RemoveAt(i);
                i--;
            }

        // check white
        foreach (Field field in guessPegsToCheck)
        {
            // value not in the code
            if (!codeToCheck.Contains(field))
                continue;

            // peg was already added to the white pegs often enough
            // (max. the number in the codeToCheck)
            if (keyPegs.Count(x => x == KeyPeg.White) == codeToCheck.Count(x => x == field))
                continue;

            keyPegs.Add(KeyPeg.White);
        }

        if (keyPegs.Count > Game.Type.Holes)
            throw new InvalidOperationException("There are more keyPegs than holes for the given gameType"); // Should not be the case

        Move move = new() { Fields = guessPegs };
        move.KeyPegs = keyPegs;
        Game.Moves.Add(move);

        // all holes correct  OR  maxmoves reached
        if (keyPegs.Count(x => x == KeyPeg.Black) == Game.Type.Holes || Game.Moves.Count >= Game.Type.MaxMoves)
            Game.End = DateTime.Now;

        return move;
    }
}