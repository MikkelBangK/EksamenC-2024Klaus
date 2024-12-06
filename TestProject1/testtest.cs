using Eksamensprojekt.Context;

namespace TestProject1;

[TestFixture]
public class testtest
{
    [Test]
    public void Test1()
    {

        VirksomhedContext db = new VirksomhedContext();
        
        Assert.Pass();
    }
}