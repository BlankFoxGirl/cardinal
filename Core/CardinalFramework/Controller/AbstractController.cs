namespace Cardinal.Controller;
using Cardinal.Entities;

public class AbstractController
{
    public Config? Conf;

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

    }
}