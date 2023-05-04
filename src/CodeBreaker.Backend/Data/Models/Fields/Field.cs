namespace CodeBreaker.Backend.Data.Models.Fields;

public abstract record class Field : IFieldVisitable
{
    public abstract TResult Accept<TResult>(IFieldVisitor<TResult> visitor);
}