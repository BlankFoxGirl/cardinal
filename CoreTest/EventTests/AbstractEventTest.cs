namespace CoreTest.Event;
using Cardinal;

public class AbstractEvent
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AbstractEvent_Can_Instance()
    {
        Assert.That(() => {
            Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent();
        }, Throws.Nothing);
    }

    [Test]
    public void AbstractEvent_Can_Instance_With_Message()
    {
        Assert.That(() => {
            Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent("0x01:test{|}0x02:test{|}0x03:test");
        }, Throws.Nothing);
    }

    [Test]
    public void AbstractEvent_Can_Instance_With_ID_Target()
    {
        Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent("ID", "Target");
        Assert.That(ae.getTarget(), Is.EqualTo("Target"));
        Assert.That(ae.getIdentifier(), Is.EqualTo("ID"));
    }

    [Test]
    public void AbstractEvent_Can_Instance_With_ID_Target_Payload()
    {
        Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent("ID", "Target", "Payload");
        Assert.That(ae.getTarget(), Is.EqualTo("Target"));
        Assert.That(ae.getIdentifier(), Is.EqualTo("ID"));
        Assert.That(ae.getPayload(), Is.EqualTo("Payload"));
    }

    [Test]
    public void AbstractEvent_Cannot_Instance_With_Invalid_Message()
    {
        Assert.That(() => {
            Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent("");
        }, Throws.InstanceOf<Cardinal.Exceptions.InvalidMessageException>());
    }

    [Test]
    public void AbstractEvent_Can_Compile_Empty()
    {
        Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent();

        Assert.That(() => {
            ae.compile();
        }, Throws.Nothing);
        Assert.That(ae.compile(), Is.Empty);
    }

    [Test]
    public void AbstractEvent_Can_Compile_Id_Target()
    {
        Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent();
        ae.setIdentifier("test");
        ae.setTarget("test");
        Assert.That(() => {
            ae.compile();
        }, Throws.Nothing);
        Assert.That(ae.compile(), Is.EqualTo("0x01:test{|}0x02:test"));
    }

    [Test]
    public void AbstractEvent_Can_Compile_Event()
    {
        Cardinal.Event.AbstractEvent ae = new Cardinal.Event.AbstractEvent();
        ae.setIdentifier("test");
        ae.setTarget("test");
        ae.setPayload("test");
        Assert.That(() => {
            ae.compile();
        }, Throws.Nothing);
        Assert.That(ae.compile(), Is.EqualTo("0x01:test{|}0x02:test{|}0x03:test"));
    }
}