namespace CodeBreaker.Transfer.Requests;

public class CreateMoveRequest
{
    public required IEnumerable<Field> Fields { get; set; }
}
