using CodeBreaker.Backend.Data.Models.Bots;

namespace CodeBreaker.Backend.Data.Repositories;
public interface IBotRepository
{
    Task<Bot> CreateAsync(Bot bot, CancellationToken cancellationToken = default);
}