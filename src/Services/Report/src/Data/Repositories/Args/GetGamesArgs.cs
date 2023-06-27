namespace CodeBreaker.Services.Report.Data.Repositories.Args;

public class GetGamesArgs
{
    private DateTime _to = DateTime.Today.AddDays(1);
    private int _maxCount = 1000;

    public GetGamesArgs()
    {
    }

    public GetGamesArgs(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public DateTime From { get; protected init; } = DateTime.Today.AddDays(-10);

    public DateTime To
    {
        get => _to;
        init
        {
            if (value < From)
                throw new ArgumentOutOfRangeException(nameof(To), $"Must not be smaller than {nameof(From)}");

            _to = value;
        }
    }

    public int MaxCount
    {
        get => _maxCount;
        init
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(MaxCount), "Must not be smaller than 0");

            _maxCount = value;
        }
    }
}
