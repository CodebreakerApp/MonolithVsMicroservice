using CodeBreaker.Services.Games.Messaging.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CodeBreaker.Services.Games.HealthChecks;

internal class MessagePublisherReadyCheck(IMessagePublisher messagePublisher) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) =>
        messagePublisher.IsClosed
            ? Task.FromResult(HealthCheckResult.Unhealthy())
            : Task.FromResult(HealthCheckResult.Healthy());
}
