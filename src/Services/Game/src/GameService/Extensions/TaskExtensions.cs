namespace System.Threading.Tasks;

internal static class TaskExtensions
{
    public static void FireAndForgetButLogExceptions(this Task task, ILogger logger)
    {
        task.ContinueWith(t =>
        {
            logger.LogError(t.Exception, "An exception occured when running a fire-and-forget task.");
        }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
    }
}