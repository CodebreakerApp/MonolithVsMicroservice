using CodeBreaker.Backend.Data.Models.Bots;

namespace CodeBreaker.Backend.Services;
public interface IBotRunnerService
{
    void ScheduleRun(Bot bot);
}