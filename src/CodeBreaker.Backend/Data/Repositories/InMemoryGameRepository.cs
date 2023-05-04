using CodeBreaker.Backend.Data.Models;
using CodeBreaker.Backend.Data.Models.Fields;
using CodeBreaker.Backend.Data.Models.GameTypes;
using CodeBreaker.Backend.Data.Models.KeyPegs;
using System.Runtime.CompilerServices;

namespace CodeBreaker.Backend.Data.Repositories;

public class InMemoryGameRepository : IGameRepository
{
    private readonly IDictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

    public InMemoryGameRepository()
    {
        void Add(Game game) { var id = Guid.NewGuid(); game.Id = id; _games.Add(id, game); }
        Add(new()
        {
            Id = default,
            Code = new List<Field>() { new ColorField(FieldColor.Green) },
            Start = DateTime.Now,
            Username = "Sebastian",
            Moves = new List<Move>()
            {
                new Move(){ Fields = new () { new ColorField(FieldColor.Red) }, KeyPegs = new (){ new BlackKeyPeg() } },
            },
            Type = new GameType6x4()
        });
        Add(new()
        {
            Id = default,
            Code = new List<Field>() { new ColorField(FieldColor.Red) },
            Start = DateTime.Now,
            Username = "Caro",
            Moves = new List<Move>(),
            Type = new GameType8x5()
        });
        Add(new()
        {
            Id = default,
            Code = new List<Field>() { new ColorField(FieldColor.Blue) },
            Start = DateTime.Now,
            Username = "Emanuel",
            Moves = new List<Move>(),
            Type = new GameType8x5()
        });
        Add(new()
        {
            Id = default,
            Code = new List<Field>() { new ColorField(FieldColor.White) },
            Start = DateTime.Now,
            Username = "Felix",
            Moves = new List<Move>(),
            Type = new GameType6x4()
        });
    }

    public Task AddMoveAsync(Guid gameId, Move move, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CancelAsync(Game game, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        Guid id = Guid.NewGuid();
        game.Id = id;
        _games.Add(id, game);
        return Task.FromResult(game);
    }

    public Task DeleteAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<Game> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var game in _games.Values)
            yield return await Task.FromResult(game);
    }

    public Task<Game> GetAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_games[gameId]);
    }

    public Task UpdateAsync(Game game, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
