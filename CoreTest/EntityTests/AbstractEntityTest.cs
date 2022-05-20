namespace CoreTest.Entity;
using Cardinal.Entities;

public class AbstractEntityTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AbstractEntity_Generates_UUID()
    {
        AbstractEntity ae = new AbstractEntity();
        Assert.True(ae.uuid != null);
    }
}