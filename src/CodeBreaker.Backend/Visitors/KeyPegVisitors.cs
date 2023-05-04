namespace CodeBreaker.Backend.Data.Models.KeyPegs;

public class KeyPegNameVisitor : IKeyPegVisitor<string>
{
    public string Visit(BlackKeyPeg keyPeg) => "black";

    public string Visit(WhiteKeyPeg keyPeg) => "white";
}

public static class KeyPegVisitorExtensions
{
    public static string GetName(this KeyPeg keyPeg) => keyPeg.Accept(new KeyPegNameVisitor());
}