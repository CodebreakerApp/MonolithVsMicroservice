using System.Text.Json.Serialization;

namespace CodeBreaker.Services.Games.Data.Models.Fields;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$t")]
[JsonDerivedType(typeof(ColorField), "color")]
[JsonDerivedType(typeof(ColorShapeField), "color-shape")]
public abstract record class Field : IFieldVisitable
{
    public abstract TResult Accept<TResult>(IFieldVisitor<TResult> visitor);
}