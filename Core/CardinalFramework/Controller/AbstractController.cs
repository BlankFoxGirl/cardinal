namespace Cardinal.Controller;
using Cardinal.Entities;

public class AbstractController
{
    public Config? Conf;
    private bool isInit = false;

    public AbstractController()
    {
        // Constructor.
        this.init(); // Inits the controller
    }
    public AbstractController(Config? conf)
    {
        // Constructor.
        this.Conf = conf;
        this.init(); // Inits the controller
    }

    virtual public void init()
    {
        this.isInit = true;
    }
    public bool IsInit()
    {
        return this.isInit;
    }
}