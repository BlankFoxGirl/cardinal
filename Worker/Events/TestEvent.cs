namespace Cardinal.Event;
using Cardinal.Entities;
using Cardinal.Exceptions;
using Cardinal.Consts;
using Cardinal.Service;

public class TestEvent : AbstractEvent
{
    public TestEvent(): base() {}
    public TestEvent(string message) : base(message) {
        // This is my entry point... Everything I wan't to do, I do here.

        this.init();
    }

    private void init()
    {
        string val = Redis.Read(this.getPayload());
        TestEvent t = new TestEvent();
        t.setPayload(val);
        t.setTarget("AbstractEvent");
        t.setIdentifier("Resp");

        Redis.Publish("Response", t.compile());
    }
}