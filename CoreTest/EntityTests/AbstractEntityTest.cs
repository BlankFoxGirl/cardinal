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

    [Test]
    public void AbstractEntity_Can_Get_UUID()
    {
        AbstractEntity ae = new AbstractEntity();
        Assert.That((string)ae.getProperty("uuid"), Is.Not.Null);
    }

    [Test]
    public void AbstractEntity_Cannot_Get_Invalid_Property()
    {
        AbstractEntity ae = new AbstractEntity();
        Assert.That(() => {
            ae.getProperty("asodkaoskdasod");
        }, Throws.InstanceOf<Cardinal.Exceptions.EntityPropertyNotFoundException>());
    }

    [Test]
    public void AbstractEntity_Cannot_Set_Invalid_Property()
    {
        AbstractEntity ae = new AbstractEntity();
        Assert.That(() => {
            ae.setProperty("asodkaoskdasod", new object());
        }, Throws.InstanceOf<Cardinal.Exceptions.EntityPropertyNotFoundException>());
    }
}