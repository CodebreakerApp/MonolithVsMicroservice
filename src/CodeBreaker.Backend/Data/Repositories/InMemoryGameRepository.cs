using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using System.Runtime.CompilerServices;

namespace CodeBreaker.Backend.Data.Repositories;

public class InMemoryGameRepository : IGameRepository
{
    private readonly IDictionary<int, Game> _games;

    private int _id;

    public InMemoryGameRepository()
    {
        _games = new Game[]
        {
            new()
            {
                Id = _id++,
                Code = new List<Field>() { new ColorField(FieldColor.Green) },
                Start = DateTime.Now,
                Username = "Sebastian",
                Moves = new List<Move>()
                {
                    new Move(){ Fields = new List<Field> () { new ColorField(FieldColor.Red) }, KeyPegs = new List<KeyPeg> (){ KeyPeg.Black } },
                },
                Type = new GameType6x4()
            },
            new()
            {
                Id = _id++,
                Code = new List<Field>() { new ColorField(FieldColor.Red) },
                Start = DateTime.Now,
                Username = "Caro",
                Moves = new List<Move>(),
                Type = new GameType8x5()
            },
            new()
            {
                Id = _id++,
                Code = new List<Field>() { new ColorField(FieldColor.Blue) },
                Start = DateTime.Now,
                Username = "Emanuel",
                Moves = new List<Move>(),
                Type = new GameType8x5()
            },
            new()
            {
                Id = _id++,
                Code = new List<Field>() { new ColorField(FieldColor.White) },
                Start = DateTime.Now,
                Username = "Felix",
                Moves = new List<Move>(),
                Type = new GameType6x4()
            }
        }.ToDictionary(x => x.Id);
    }

    public Task AddMoveAsync(int gameId, Move move, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CancelAsync(int gameId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        _games.Add(_id++, game);
        return Task.FromResult(game);
    }

    public Task DeleteAsync(int gameId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<Game> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var game in _games.Values)
            yield return await Task.FromResult(game);
    }

    public Task<Game> GetAsync(int gameId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_games[gameId]);
    }

    public Task UpdateAsync(int gameId, Game game, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
