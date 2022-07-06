namespace ServerApp.WebApp.Base.Exceptions;

public class UnauthorizedException : BaseServerException
{
    public UnauthorizedException(string message) : base("Unauthorized", message)
    {
    }
}