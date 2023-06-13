using CodeBreaker.Services.Games.Transfer.Api;

namespace CodeBreaker.Services.Games.Transfer.Api.Requests;

public class CreateMoveRequest
{
    public required IEnumerable<Field> Fields { get; set; }
}
