namespace Cardinal.Controller;
using Cardinal.Service;
public class Worker : AbstractController, IWorker
{
    public Worker() : base()
    { }
    public Worker(Cardinal.Entities.Config conf) : base(conf)
    { }
    public override void init()
    {
        base.init();
        string connectionString = "localhost,abortConnect=false";
        if (this.Conf != null) {
            connectionString = this.Conf.IP_ADDRESS + ":" + this.Conf.PORT + ",abortConnect=false";
        }
        Redis.Connect(connectionString);
    }
}