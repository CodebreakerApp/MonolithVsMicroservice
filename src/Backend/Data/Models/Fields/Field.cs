using System.Text.Json.Serialization;

namespace CodeBreaker.Backend.Data.Models.Fields;

[JsonDerivedType(typeof(ColorField), "color")]
[JsonDerivedType(typeof(ColorShapeField), "color-shape")]
public abstract record class Field : IFieldVisitable
{
    public abstract TResult Accept<TResult>(IFieldVisitor<TResult> visitor);
}