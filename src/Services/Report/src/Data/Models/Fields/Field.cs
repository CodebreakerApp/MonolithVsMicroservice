namespace CodeBreaker.Services.Report.Data.Models.Fields;

public abstract record class Field : IFieldVisitable
{
    public int Id { get; set; }

    public abstract TResult Accept<TResult>(IFieldVisitor<TResult> visitor);

    internal int Position { get; set; }
}