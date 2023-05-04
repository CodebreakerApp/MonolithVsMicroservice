namespace CodeBreaker.Backend.Data.Models;

public struct Empty
{
}


//public class DerivedEmptyResult : EmptyResult { }


//public class TestVisitor : IGameTypeVisitor<EmptyResult>
//{
//    public EmptyResult Visit(GameType6x4 gameType)
//    {
//        throw new NotImplementedException();
//    }

//    public EmptyResult Visit(GameType8x5 gameType)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class TestVisitor2 : IGameTypeVisitor<DerivedEmptyResult>
//{
//    public DerivedEmptyResult Visit(GameType6x4 gameType)
//    {
//        throw new NotImplementedException();
//    }

//    public DerivedEmptyResult Visit(GameType8x5 gameType)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class VisitorCovarianceDemo
//{
//    public VisitorCovarianceDemo()
//    {
//        IGameTypeVisitor<EmptyResult> visitor1 = new TestVisitor();
//        IGameTypeVisitor<DerivedEmptyResult> visitor2 = new TestVisitor2();
//        visitor1 = visitor2;
//    }
//}