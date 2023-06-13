namespace CodeBreaker.Services.Games.Data.Models.Fields;

public interface IFieldVisitor<TResult>
{
    TResult Visit(ColorField field);

    TResult Visit(ColorShapeField field);
}

public interface IFieldVisitor : IFieldVisitor<Empty> { }

public interface IFieldVisitable
{
    TResult Accept<TResult>(IFieldVisitor<TResult> visitor);
}