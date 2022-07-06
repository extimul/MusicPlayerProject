namespace ServerApp.WebApp.Base.Exceptions;

public class NotFoundException : BaseServerException
{
    protected NotFoundException(string message)
        : base("Not Found", message)
    {
    }
}