using CodeBreaker.Backend.Data.Models.Bots;

namespace CodeBreaker.Backend.Services;

public interface IBotService
{
    Task<Bot> CreateBotAsync(CreateBotArgs args, CancellationToken cancellationToken = default);
}