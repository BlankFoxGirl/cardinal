namespace CoreTest.Controller;
using Cardinal.Controller;

public class AbstractControllerTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AbstractController_Is_Init()
    {
        AbstractController ac = new AbstractController();
        Assert.That(ac.IsInit(), Is.EqualTo(true));
    }

    [Test]
    public void AbstractController_Can_Set_Conf()
    {
        Cardinal.Entities.Config conf = new Cardinal.Entities.Config();
        AbstractController ac = new AbstractController(conf);
        Assert.That(ac.IsInit(), Is.EqualTo(true));
        Assert.That(ac.Conf, Is.Not.Null);
        Assert.That(ac.Conf.IS_LISTENER, Is.EqualTo(true));
    }
}